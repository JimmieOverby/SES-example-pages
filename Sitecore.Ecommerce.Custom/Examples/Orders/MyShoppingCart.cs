namespace Sitecore.Ecommerce.Examples.Orders
{
  using Carts;
  using Data;

  /// <summary>
  /// Defines my shopping cart class.
  /// </summary>
  public class MyShoppingCart : ShoppingCart
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MyShoppingCart"/> class.
    /// </summary>
    public MyShoppingCart()
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