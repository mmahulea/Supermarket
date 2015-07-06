namespace Supermarket.Domain.Rules
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class StrawberryBulkPurchaseRule : IPricingRule
	{
		private const decimal NewBulkPrice = 4.5m;
		private IEnumerable<Product> allProducts;

		public decimal GetDiscount(IEnumerable<Product> products)
		{
			this.allProducts = products;
			decimal noDiscount = Decimal.Zero;

			if (ThreeOrMoreStrawberriesAreBeingSold)
			{
				return BulkPriceDiscount;
			}
			
			return noDiscount;
		}

		private bool ThreeOrMoreStrawberriesAreBeingSold
		{
			get { return this.allProducts.Count(p => p.ProductCode == "SR1") >= 3; }
		}

		private decimal BulkPriceDiscount
		{
			get
			{
				int howManyStroberiesAreBeingBought = this.allProducts.Count(p => p.ProductCode == "SR1");
				decimal totalStroberyPrice = this.allProducts.Where(p => p.ProductCode == "SR1").Sum(p => p.Price);
				return totalStroberyPrice - (howManyStroberiesAreBeingBought * NewBulkPrice);
			}
			
		}
	}
}
