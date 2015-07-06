namespace Supermarket.Domain.Rules
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class FruitTeaBuyOneGetOneFreeRule : IPricingRule
	{
		private IEnumerable<Product> allProducts;

		public decimal GetDiscount(IEnumerable<Product> products)
		{
			this.allProducts = products;
			decimal noDiscount = Decimal.Zero;

			if (AtLeastTwoFruitTeaItemsAreBeingSold)
			{
				return this.BuyOneGetOneFreeDiscount;
			}

			return noDiscount;
		}

		private bool AtLeastTwoFruitTeaItemsAreBeingSold
		{
			get { return this.allProducts.Count(p => p.ProductCode == "FR1") > 1; }
		}

		private decimal BuyOneGetOneFreeDiscount
		{
			get
			{
				int numberOffFruitTeaProducts = this.allProducts.Count(p => p.ProductCode == "FR1");
				Product fruitTea = this.allProducts.FirstOrDefault(p => p.ProductCode == "FR1");
				if (fruitTea != null)
				{
					return (numberOffFruitTeaProducts / 2) * fruitTea.Price;
				}
				return Decimal.Zero;

			}
		}
	}
}
