namespace Supermarket.Console
{
	using System;
	using System.Collections.Generic;
	using Supermarket.Domain;
	using Supermarket.Domain.Repositories;

	static class Program
	{
		static void Main()
		{						
			ProductRepository productRepository = new ProductRepository();
			PricingRuleRepository pricingRuleRepository = new PricingRuleRepository();

			IEnumerable<IPricingRule> pricingRules = pricingRuleRepository.GetAll();
			IEnumerable<Product> products = productRepository.GetAll();

			Checkout checkout = new Checkout(pricingRules, productRepository);

			foreach (var product in products)
			{
				Console.WriteLine("{0, -5}   {1, -20}   {2, 5}", product.ProductCode, product.Name, product.Price);
			}

			Console.Write("Please enter produc code: ");
			string key = Console.ReadLine();
			while (key != null && key.ToLower() != "checkout")
			{				
				if (!checkout.Scan(key))
				{
					Console.WriteLine("Product not found!");
				}
				else
				{
					Console.WriteLine(checkout.ToString());
				}
				key = Console.ReadLine();
			}

			Console.WriteLine("Total price: " + checkout.TotalPrice);

			Console.WriteLine();
			Console.WriteLine("Press any key to exit");
			Console.ReadLine();
		}
	}
}
