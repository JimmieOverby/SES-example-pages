// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDetailsPresenterTest.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the ProductDetailsPresenterTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Tests.Presenters
{
  using System.Collections.Specialized;
  using System.Linq;
  using System.Web;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Ecommerce.DomainModel.Currencies;
  using Sitecore.Ecommerce.DomainModel.Prices;
  using Sitecore.Ecommerce.DomainModel.Products;
  using Sitecore.Ecommerce.MvpWebStore.Presenters;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using Sitecore.Ecommerce.OrderManagement;
  using Sitecore.Ecommerce.OrderManagement.Orders;
  using Sitecore.Ecommerce.Products;
  using Xunit;

  /// <summary>
  /// Defines the ProductDetailsPresenterTest type.
  /// </summary>
  public class ProductDetailsPresenterTest
  {
    /// <summary>
    /// The view.
    /// </summary>
    private readonly IProductDetailsView view;

    /// <summary>
    /// The product repository.
    /// </summary>
    private readonly IProductRepository productRepository;

    /// <summary>
    /// The HTTP context.
    /// </summary>
    private readonly HttpContextBase httpContext;

    /// <summary>
    /// The product.
    /// </summary>
    private readonly Product product;

    /// <summary>
    /// The price manager.
    /// </summary>
    private readonly IProductPriceManager priceManager;

    /// <summary>
    /// The stock manager.
    /// </summary>
    private readonly IProductStockManager stockManager;

    /// <summary>
    /// The order manager.
    /// </summary>
    private readonly VisitorOrderManager orderManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductDetailsPresenterTest" /> class.
    /// </summary>
    public ProductDetailsPresenterTest()
    {
      this.view = Substitute.For<IProductDetailsView>();

      this.product = new Product { Code = "d3" };

      this.productRepository = Substitute.For<IProductRepository>();
      this.productRepository.Get<Product>("d3").Returns(this.product);

      this.priceManager = Substitute.For<IProductPriceManager>();
      this.priceManager.GetProductTotals<Totals, Product, Currency>(this.product, null).Returns(new Totals { PriceExVat = 7.00m });

      this.stockManager = Substitute.For<IProductStockManager>();
      this.stockManager.GetStock(Arg.Is<ProductStockInfo>(p => p.ProductCode == "d3")).Returns(new Products.ProductStock { Code = "d3", Stock = 5 });

      this.orderManager = Substitute.For<VisitorOrderManager>();
      this.httpContext = Substitute.For<HttpContextBase>();

      new ProductDetailsPresenter(this.view, this.productRepository, this.stockManager, this.priceManager, this.orderManager) { HttpContext = this.httpContext };
    }

    /// <summary>
    /// Should read product details from query string and bind to view model.
    /// </summary>
    [Fact]
    public void ShouldReadProductDetailsFromQueryStringAndBindToViewModel()
    {
      // Arrange
      this.httpContext.Request.QueryString.Returns(new NameValueCollection { { "p", "d3" } });

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.view.Model.Product.Should().Be(this.product);
    }

    /// <summary>
    /// Should not bind product details to view model if no product code specified in query string.
    /// </summary>
    [Fact]
    public void ShouldNotBindProductDetailsToViewModelIfNoProductCodeSpecifiedInQueryString()
    {
      // Arrange
      this.httpContext.Request.QueryString.Returns(new NameValueCollection());

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.view.Model.Product.Should().BeNull();
      this.productRepository.DidNotReceiveWithAnyArgs().Get<ProductBaseData>(null);
    }

    /// <summary>
    /// Should load product price.
    /// </summary>
    [Fact]
    public void ShouldLoadProductPrice()
    {
      // Arrange
      this.httpContext.Request.QueryString.Returns(new NameValueCollection { { "p", "d3" } });

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.view.Model.Price.Should().Be(7.00m);
    }

    /// <summary>
    /// Should load product stock.
    /// </summary>
    [Fact]
    public void ShouldLoadProductStock()
    {
      // Arrange
      this.httpContext.Request.QueryString.Returns(new NameValueCollection { { "p", "d3" } });

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.view.Model.Stock.Should().Be(5);
    }

    /// <summary>
    /// Should create order.
    /// </summary>
    [Fact]
    public void ShouldCreateOrder()
    {
      // Arrange
      this.httpContext.Request.QueryString.Returns(new NameValueCollection { { "p", "d3" } });
      this.view.Load += Raise.Event();

      // Act
      this.view.Buy += Raise.Event();

      // Assert
      this.orderManager
        .Received()
        .Create(Arg.Is<Order>(
        order =>
          order.ShopContext == "mvpwebstore" &&
          order.State.Code == "New" &&
          order.State.Name == "New" &&
          order.OrderLines.Count == 1 &&
          order.OrderLines.First().LineItem.Item.Code == "d3" &&
          order.OrderLines.First().LineItem.Quantity == 1 &&
          order.OrderLines.First().LineItem.Price.PriceAmount.Value == 7.00m &&
          order.OrderLines.First().LineItem.Price.PriceAmount.CurrencyID == "USD"));
    }

    /// <summary>
    /// Should update stock before creating order.
    /// </summary>
    [Fact]
    public void ShouldUpdateStockBeforeCreatingOrder()
    {
      // Arrange
      this.httpContext.Request.QueryString.Returns(new NameValueCollection { { "p", "d3" } });
      this.view.Load += Raise.Event();

      // Act
      this.view.Buy += Raise.Event();

      // Assert
      this.stockManager.Received().Update(Arg.Is<ProductStockInfo>(p => p.ProductCode == "d3"), 4);
    }

    /// <summary>
    /// Should refresh product details page when order created.
    /// </summary>
    [Fact]
    // TODO: Review
    public void ShouldRefreshProductDetailsPageWhenOrderCreated()
    {
      // Arrange
      this.httpContext.Request.RawUrl.Returns("http://host?p=d3");
      this.httpContext.Request.QueryString.Returns(new NameValueCollection { { "p", "d3" } });
      this.view.Load += Raise.Event();

      // Act
      this.view.Buy += Raise.Event();

      // Assert
      this.stockManager.Received().Update(Arg.Is<ProductStockInfo>(p => p.ProductCode == "d3"), 4);
      this.httpContext.Response.Received().Redirect("http://host?p=d3");
    }

    /// <summary>
    /// Should not create order if no products in stock.
    /// </summary>
    [Fact]
    public void ShouldNotCreateOrderIfNoProductsInStock()
    {
      // Arrange
      this.stockManager.GetStock(Arg.Is<ProductStockInfo>(p => p.ProductCode == "d3")).Returns(new Products.ProductStock { Code = "d3", Stock = 0 });

      this.httpContext.Request.QueryString.Returns(new NameValueCollection { { "p", "d3" } });
      this.view.Load += Raise.Event();

      // Act
      this.view.Buy += Raise.Event();

      // Assert
      this.orderManager.DidNotReceive().Create(Arg.Any<Order>());
    }

    /// <summary>
    /// Should create the order with hard coded customer id.
    /// </summary>
    [Fact]
    public void ShouldCreateTheOrderWithHardCodedCustomerId()
    {
      // Arrange
      this.httpContext.Request.QueryString.Returns(new NameValueCollection { { "p", "d3" } });
      this.view.Load += Raise.Event();

      // Act
      this.view.Buy += Raise.Event();

      // Assert
      this.orderManager.Received().Create(Arg.Is<Order>(order => order.BuyerCustomerParty.SupplierAssignedAccountID == "100500"));
    }
  }
}