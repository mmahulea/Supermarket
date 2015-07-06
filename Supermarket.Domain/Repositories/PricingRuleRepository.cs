namespace Supermarket.Domain.Repositories
{
	using System.Collections.Generic;
	using Supermarket.Domain.Rules;

	public class PricingRuleRepository
	{
		public IEnumerable<IPricingRule> GetAll()
		{
			return new List<IPricingRule>
			{
				new FruitTeaBuyOneGetOneFreeRule(),
				new StrawberryBulkPurchaseRule()
			};
		}
	}
}
