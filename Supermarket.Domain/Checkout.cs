namespace Supermarket.Domain
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Supermarket.Domain.Repositories;

	public class Checkout
	{
		private readonly ProductRepository productRepository;
		
		private readonly List<Product> products = new List<Product>();

		private readonly List<IPricingRule> pricingRules; 

		public Checkout(IEnumerable<IPricingRule> pricingRules, ProductRepository productRepository)
		{
			if (pricingRules == null)
			{
				throw new ArgumentNullException("pricingRules");
			}
			if (productRepository == null)
			{
				throw new ArgumentNullException("productRepository");
			}
			this.productRepository = productRepository;
			this.pricingRules = pricingRules.ToList();
		}
		
		public bool Scan(string productCode)
		{
			var productToAdd = this.productRepository.Get(productCode);
			if (productToAdd != null)
			{
				this.products.Add(productToAdd);
				return true;
			}
			return false;
		}
	
		public decimal TotalPrice
		{
			get
			{
				return this.products.Sum(p => p.Price) - this.pricingRules.Sum(r => r.GetDiscount(this.products));
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(string.Format("{0, -4} {1, -15}  {2, -3} {3, 10}", "Code", "Name", "Qty", "Price"));
			sb.AppendLine("------------------------------------");
			foreach (var grp in this.products.GroupBy(p => p.ProductCode))
			{
				sb.AppendLine(string.Format("{0, -4} {1, -15}  {2, -3} {3, 10}", grp.Key, grp.FirstOrDefault().Name, grp.Count(), grp.Sum(p => p.Price).ToString("£0.00")));
			}				
			foreach (var pricingRule in this.pricingRules)
			{
				var discount = pricingRule.GetDiscount(this.products);
				if (discount > 0)
				{					
					string discountCode = pricingRule.GetType().Name;
					string txt = discountCode;
					string price = (0 - discount).ToString("£0.00");
					for (int i = 0; i < 36 - discountCode.Length - price.Length; i++)
					{
						txt += " ";
					}
					txt += price;
					sb.AppendLine(txt);					
				}
			}
			sb.AppendLine("------------------------------------");
			sb.AppendLine(String.Format("Total: {0, 29}", this.TotalPrice.ToString("£0.00")));
			
			return sb.ToString();
		}
	}
}
