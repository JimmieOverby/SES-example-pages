namespace Sitecore.Ecommerce.WebApiStore.Domain
{
  using System.Collections.Generic;
  using Sitecore.Ecommerce.WebApiStore.Models;

  public abstract class ProductRepositoryBase
  {
    public abstract IEnumerable<Product> GetProducts();
  }
}