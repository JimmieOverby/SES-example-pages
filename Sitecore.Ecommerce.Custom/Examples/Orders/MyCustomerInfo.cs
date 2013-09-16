namespace Sitecore.Ecommerce.Examples.Orders
{
  using Data;
  using DomainModel.Data;

  public class MyCustomerInfo : IEntity
  {
    [Entity(FieldName = "Hobby")]
    public string Hobby { get; set; }

    public string Alias { get; set; }
  }
}