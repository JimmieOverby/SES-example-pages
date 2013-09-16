namespace Sitecore.Ecommerce.Examples.Orders
{
  using Data;
  using DomainModel.Orders;

  /// <summary>
  /// Defines my order class.
  /// </summary>
  public class MyOrder : Ecommerce.Orders.Order
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MyOrder"/> class.
    /// </summary>
    /// <param name="status">The status.</param>
    public MyOrder(OrderStatus status)
      : base(status)
    {
      this.MyCustomerInfo = new MyCustomerInfo();
    }

    /// <summary>
    /// Gets or sets my customer info.
    /// </summary>
    /// <value>
    /// My customer info.
    /// </value>
    [Entity(FieldName = "My Customer Info")]
    public MyCustomerInfo MyCustomerInfo { get; set; }
  }
}