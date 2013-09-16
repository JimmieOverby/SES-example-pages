// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDetailsPresenter.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the product details presenter.
//   The presenter parses query string to locate product code,
//   read the product and put in into view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Presenters
{
  using System;
  using Sitecore.Ecommerce.Common;
  using Sitecore.Ecommerce.DomainModel.Prices;
  using Sitecore.Ecommerce.DomainModel.Products;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using Sitecore.Ecommerce.OrderManagement;
  using Sitecore.Ecommerce.OrderManagement.Orders;
  using Sitecore.Ecommerce.Products;
  using WebFormsMvp;
  using ProductStock = Sitecore.Ecommerce.DomainModel.Products.ProductStock;

  /// <summary>
  /// Defines the product details presenter.
  /// The presenter parses query string to locate product code,
  /// read the product and put in into view model.
  /// When the one tries to buy the order it creates the simple one and stores
  /// it to the order management database.
  /// All of the orders are created using predefined customer id.
  /// </summary>
  public class ProductDetailsPresenter : Presenter<IProductDetailsView>
  {
    /// <summary>
    /// The product repository.
    /// </summary>
    private readonly IProductRepository productRepository;

    /// <summary>
    /// The product stock manager.
    /// </summary>
    private readonly IProductStockManager stockManager;

    /// <summary>
    /// The product price manager.
    /// </summary>
    private readonly IProductPriceManager priceManager;

    /// <summary>
    /// The order manager.
    /// </summary>
    private readonly VisitorOrderManager orderManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductDetailsPresenter" /> class.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="productRepository">The product repository.</param>
    /// <param name="stockManager">The stock manager.</param>
    /// <param name="priceManager">The price manager.</param>
    /// <param name="orderManager">The order manager.</param>
    public ProductDetailsPresenter(IProductDetailsView view, IProductRepository productRepository, IProductStockManager stockManager, IProductPriceManager priceManager, VisitorOrderManager orderManager)
      : base(view)
    {
      this.productRepository = productRepository;
      this.stockManager = stockManager;
      this.priceManager = priceManager;
      this.orderManager = orderManager;

      this.View.Load += this.Load;
      this.View.Buy += this.Buy;
    }

    /// <summary>
    /// Handles View Load event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void Load(object sender, EventArgs e)
    {
      // Gets product code and stops to work in case the one is not specified.
      var code = this.HttpContext.Request.QueryString["p"];
      if (string.IsNullOrEmpty(code))
      {
        return;
      }

      // Gets all products and sets the View.Model.Product to the one.
      var product = this.productRepository.Get<Product>(code);
      this.View.Model.Product = product;

      // Gets product stock and price.
      if (product != null)
      {
        this.View.Model.Stock = this.stockManager.GetStock(new ProductStockInfo { ProductCode = product.Code }).Stock;
        this.View.Model.Price = this.priceManager.GetProductTotals<Totals, Product, DomainModel.Currencies.Currency>(product, null).PriceExVat;
      }
    }

    /// <summary>
    /// Handles View Buy event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void Buy(object sender, EventArgs e)
    {
      // Gets product stock for the specified order.
      ProductStockInfo productStockInfo = new ProductStockInfo { ProductCode = this.View.Model.Product.Code };
      ProductStock productStock = this.stockManager.GetStock(productStockInfo);

      // Stops to work if the stock less than zero.
      if (productStock.Stock <= 0)
      {
        return;
      }

      // Decrements the stock.
      this.stockManager.Update(productStockInfo, productStock.Stock - 1);

      // Initializes new order from the mvpwebstore
      // with one order line.
      // The customer id is initialized with the predefined value.
      Order order = new Order { State = new State { Code = "New", Name = "New" }, ShopContext = "mvpwebstore", OrderId = Guid.NewGuid().ToString(), PricingCurrencyCode = "USD" };
      OrderLine orderLine = new OrderLine
        {
          Order = order,
          LineItem = new LineItem
            {
              Item = new Item { Code = this.View.Model.Product.Code },
              Price = new Price(new Amount(this.View.Model.Price, "USD")),
              Quantity = 1,
              TotalTaxAmount = new Amount(),
            }
        };
      order.OrderLines.Add(orderLine);
      order.BuyerCustomerParty = new CustomerParty { SupplierAssignedAccountID = Texts.MvpWebStoreCustomerId };

      // Creates the order using Visitor API
      this.orderManager.Create(order);

      // Redirects to the same page. After reload the stock will be decreased.
      this.HttpContext.Response.Redirect(this.HttpContext.Request.RawUrl);
    }
  }
}