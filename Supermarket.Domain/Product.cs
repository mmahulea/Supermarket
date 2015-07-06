namespace Supermarket.Domain
{
	using System;

	public class Product
	{
		private readonly string productCode;

		private readonly string name;

		private readonly decimal price;		

		public Product(string productCode, string name, decimal price)
		{
			if (productCode == null)
			{
				throw new ArgumentNullException("productCode");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.productCode = productCode;
			this.name = name;
			this.price = price;
		}

		public string ProductCode { get { return this.productCode; } }
		public string Name { get { return this.name; } }
		public decimal Price { get { return this.price; } }
	}
}
