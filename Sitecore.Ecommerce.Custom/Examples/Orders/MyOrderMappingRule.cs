namespace Sitecore.Ecommerce.Examples.Orders
{
  using Data;

  public class MyOrderMappingRule : OrderMappingRule
  {
    /// <summary>
    /// Gets or sets the hobby.
    /// </summary>
    /// <value>
    /// The hobby.
    /// </value>
    [Entity(FieldName = "Hobby")]
    public virtual string Hobby
    {
      get { return ((MyOrder)this.MappingObject).MyCustomerInfo.Hobby; }
      set { ((MyOrder)this.MappingObject).MyCustomerInfo.Hobby = value; }
    }
  }
}