// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CancelOrderPresenter.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The cancel order presenter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Presenters
{
  using System;
  using System.Linq;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using Sitecore.Ecommerce.OrderManagement;
  using Sitecore.Ecommerce.Visitor.OrderManagement;
  using WebFormsMvp;

  /// <summary>
  /// Defines the presenter that manages the presentation and behavior of the CancelOrder view.
  /// First of all the presenter check the id that is provided via query string.
  /// If it doesn't exist it stops the working.
  /// After that it checks the specified user id, casts the repository to IUserAware interface and sets the one.
  /// The repository itself retrieves the order with specified order id and sends it to the order processor.
  /// At the end the presenter sets the result property of the ViewModel to the one of the predefined constants or to the exception message
  /// according to the circumstances.
  /// </summary>
  public class CancelOrderPresenter : Presenter<ICancelOrderView>
  {
    /// <summary>
    /// The order processor.
    /// </summary>
    private readonly VisitorOrderProcessorBase orderProcessor;

    /// <summary>
    /// The order repository.
    /// </summary>
    private readonly VisitorOrderManager orderManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelOrderPresenter" /> class.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="orderManager">The order repository.</param>
    /// <param name="orderProcessor">The order processor.</param>
    public CancelOrderPresenter(ICancelOrderView view, VisitorOrderManager orderManager, VisitorOrderProcessorBase orderProcessor)
      : base(view)
    {
      this.View.Load += this.Load;

      this.orderManager = orderManager;
      this.orderProcessor = orderProcessor;
    }

    /// <summary>
    /// Loads the specified sender.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void Load(object sender, EventArgs e)
    {
      try
      {
        // Gets the value of the order id from the query string
        // and stops to work and set the warning message
        // in case the one doesn't specified.
        var id = this.HttpContext.Request.QueryString["id"];
        if (string.IsNullOrEmpty(id))
        {
          this.View.Model.Result = Texts.TheOrderIdIsNotSpecified;
          return;
        }

        // Gets the value of the user id from the query string,
        // casts the order repository to the IUserAware interface
        // and set the CustomerId
        var userId = this.HttpContext.Request.QueryString["user"]; 
        var aware = this.orderManager as IUserAware;
        if (aware != null)
        {
          aware.CustomerId = userId;
        }

        // Retrieve the order with the specified id.
        var order = this.orderManager.GetAll().FirstOrDefault(o => o.OrderId == id);

        // Cancels the order.
        this.orderProcessor.CancelOrder(order);

        // Sets the successful result.
        this.View.Model.Result = string.Format(Texts.TheOrderHasBeenCancelledSuccessfully, id);
      }
      catch (Exception exception)
      {
        // Sets the failure result.
        this.View.Model.Result = exception.Message;
      }
    }
  }
}