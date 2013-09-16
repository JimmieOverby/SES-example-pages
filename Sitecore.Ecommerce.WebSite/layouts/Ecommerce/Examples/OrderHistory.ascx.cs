// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderHistory.ascx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Order history user control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.layouts.Ecommerce
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.UI;
  using System.Web.UI.WebControls;
  using Globalization;
  using OrderManagement;
  using OrderManagement.Orders;
  using Visitor.OrderManagement;

  /// <summary>
  /// Order history user control.
  /// </summary>
  public partial class OrderHistory : UserControl
  {
    /// <summary>
    /// Cancelled state.
    /// </summary>
    private const string CancelledState = "Cancelled";

    /// <summary>
    ///   VisitorOrderManager instance.
    /// </summary>
    private VisitorOrderManager orderRepository;

    /// <summary>
    ///  VisitorOrderProcessorBase instance.
    /// </summary>
    private VisitorOrderProcessorBase orderProcessor;

    /// <summary>
    ///  VisitorOrderSecurity instance.
    /// </summary>
    private VisitorOrderSecurity orderSecurity;

    /// <summary>
    /// Collection of the orders.
    /// </summary>
    private IEnumerable<Order> orders;

    /// <summary>
    /// Gets or sets the order repository.
    /// </summary>
    /// <value>
    /// The order repository.
    /// </value>
    public virtual VisitorOrderManager OrderRepository
    {
      get
      {
        return this.orderRepository ?? (this.orderRepository = Sitecore.Ecommerce.Context.Entity.Resolve<VisitorOrderManager>());
      }

      set
      {
        this.orderRepository = value;
      }
    }

    /// <summary>
    /// Gets or sets the order processor.
    /// </summary>
    /// <value>
    /// The order processor.
    /// </value>
    public virtual VisitorOrderProcessorBase OrderProcessor
    {
      get
      {
        return this.orderProcessor ?? (this.orderProcessor = Sitecore.Ecommerce.Context.Entity.Resolve<VisitorOrderProcessorBase>());
      }

      set
      {
        this.orderProcessor = value;
      }
    }

    /// <summary>
    /// Gets or sets the order security.
    /// </summary>
    /// <value>
    /// The order security.
    /// </value>
    public virtual VisitorOrderSecurity OrderSecurity
    {
      get
      {
        return this.orderSecurity ?? (this.orderSecurity = Sitecore.Ecommerce.Context.Entity.Resolve<VisitorOrderSecurity>());
      }

      set
      {
        this.orderSecurity = value;
      }
    }

    /// <summary>
    /// Gets the quantity.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <returns>The quantity of all products.</returns>
    protected static decimal GetQuantity(object dataItem)
    {
      Order order = dataItem as Order;
      if (order == null)
      {
        return 0;
      }

      uint itemsInShoppingCart = 0;
      order.OrderLines.ToList().ForEach(p => itemsInShoppingCart += (uint)p.LineItem.Quantity);
      return itemsInShoppingCart;
    }


    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
      this.orders = this.OrderRepository.GetAll().ToArray();

      this.orderList.DataSource = this.orders.Where(o => o.ShopContext == Sitecore.Context.Site.Name);
      this.orderList.DataBind();
    }

    /// <summary>
    /// Handles the Command event of the CancelOrderLink control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void CancelOrderLink_Click(object sender, EventArgs e)
    {
      LinkButton linkButton = (LinkButton)sender;
      string orderNumber = linkButton.CommandArgument;
      Order order = this.GetOrder(orderNumber);
      if (order != null)
      {
        this.OrderProcessor.CancelOrder(order);

        Response.Redirect(Request.RawUrl);
      }
    }

    /// <summary>
    /// Handles the DataBind event of the CancelOrderLink control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void CancelOrderLink_DataBind(object sender, EventArgs e)
    {
      LinkButton linkButton = (LinkButton)sender;
      string orderNumber = linkButton.CommandArgument;
      Order order = this.GetOrder(orderNumber);
      if (order != null)
      {
        bool isCancelled = order.State != null && order.State.Code == CancelledState;
        bool canCancel = this.OrderSecurity.CanCancel(order);
        linkButton.Text = Translate.Text(isCancelled ? Texts.TheOrderIsCancelled : (canCancel ? Texts.CancelOrder : Texts.TheOrderCannotBeCancelled));
        linkButton.Enabled = !isCancelled && canCancel;
      }
    }

    /// <summary>
    /// Gets the order.
    /// </summary>
    /// <param name="orderNumber">The order number.</param>
    /// <returns>
    /// The order.
    /// </returns>
    private Order GetOrder(string orderNumber)
    {
      return this.orders.FirstOrDefault(o => o.OrderId == orderNumber);
    }
  }
}