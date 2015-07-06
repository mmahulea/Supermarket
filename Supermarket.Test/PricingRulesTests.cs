namespace Supermarket.Test
{
	using Supermarket.Domain;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System.Collections.Generic;
	using Supermarket.Domain.Repositories;
	using Supermarket.Domain.Rules;

	[TestClass]
	public class PricingRulesTests
	{		
		private Checkout checkout;

		[TestInitialize]
		public void TestInitialize()
		{
			this.checkout = new Checkout(new List<IPricingRule>
			{
				new FruitTeaBuyOneGetOneFreeRule(),
				new StrawberryBulkPurchaseRule()
			},
			new ProductRepository());
		}

		[TestMethod]
		public void Only_fruit_tea_buy_one_get_one_free_rule_should_be_applied()
		{
			this.checkout.Scan("FR1");
			this.checkout.Scan("SR1");
			this.checkout.Scan("FR1");
			this.checkout.Scan("FR1");
			this.checkout.Scan("FR1");
			this.checkout.Scan("CF1");
			Assert.AreEqual(this.checkout.TotalPrice, 22.45m);
		}

		[TestMethod]
		public void Fruit_tea_buy_one_get_one_free_rule_should_be_applied()
		{
			this.checkout.Scan("FR1");			
			this.checkout.Scan("FR1");
			Assert.AreEqual(this.checkout.TotalPrice, 3.11m);			
		}

		[TestMethod]
		public void Strawberries_discount_for_bulk_purchase_should_be_applied()
		{
			this.checkout.Scan("SR1");
			this.checkout.Scan("SR1");
			this.checkout.Scan("FR1");
			this.checkout.Scan("SR1");
			Assert.AreEqual(this.checkout.TotalPrice, 16.61m);						
		}
	}
}
