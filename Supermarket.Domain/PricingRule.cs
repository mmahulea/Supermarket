namespace Supermarket.Domain
{
	using System.Collections.Generic;

	public interface IPricingRule
	{
		decimal GetDiscount(IEnumerable<Product> products);
	}
}
