using System;

namespace Supermarket.Domain.Repositories
{
	using System.Collections.Generic;

	public class ProductRepository
	{
		public IEnumerable<Product> GetAll()
		{
			return new List<Product>() { this.Get("FR1"), this.Get("SR1"), this.Get("CF1"), };
		}

		public Product Get(string productCode)
		{
			switch (productCode.ToUpper())
			{
				case "FR1": return new Product("FR1", "Fruit tea", 3.11m);
				case "SR1": return new Product("SR1", "Strawberries ", 5);
				case "CF1": return new Product("CF1", "Coffee ", 11.23m);
				default: return null;
			}
		}
	}
}
