namespace Sitecore.Ecommerce.WebApiStore.Domain
{
  using System.Collections.Generic;
  using Sitecore.Ecommerce.WebApiStore.Models;

  public class InMemoryProductRepository : ProductRepositoryBase
  {
    private readonly Product[] products = new[] 
      { 
          new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 }, 
          new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M }, 
          new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M } 
      };

    public override IEnumerable<Product> GetProducts()
    {
      return this.products;
    }
  }
}