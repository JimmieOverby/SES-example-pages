// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CancelOrderPresenterTest.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the CancelOrderPresenterTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Tests.Presenters
{
  using System;
  using System.Collections.ObjectModel;
  using System.Collections.Specialized;
  using System.Linq;
  using System.Web;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Ecommerce.MvpWebStore.Presenters;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using Sitecore.Ecommerce.OrderManagement;
  using Sitecore.Ecommerce.OrderManagement.Orders;
  using Sitecore.Ecommerce.Visitor.OrderManagement;

  using Xunit;

  /// <summary>
  /// The cancel order presenter test.
  /// </summary>
  public class CancelOrderPresenterTest
  {
    /// <summary>
    /// The view.
    /// </summary>
    private readonly ICancelOrderView view;

    /// <summary>
    /// The order processor.
    /// </summary>
    private readonly VisitorOrderProcessorBase orderProcessor;

    /// <summary>
    /// The HTTP context.
    /// </summary>
    private readonly HttpContextBase httpContext;

    /// <summary>
    /// The order manager.
    /// </summary>
    private readonly VisitorOrderManager orderManager;

    /// <summary>
    /// The order.
    /// </summary>
    private readonly Order order;

    /// <summary>
    /// The orders.
    /// </summary>
    private readonly IQueryable<Order> orders;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelOrderPresenterTest" /> class.
    /// </summary>
    public CancelOrderPresenterTest()
    {
      this.view = Substitute.For<ICancelOrderView>();
      this.orderProcessor = Substitute.For<VisitorOrderProcessorBase>();
      this.orderManager = Substitute.For<VisitorOrderManager, IUserAware>();
      this.httpContext = Substitute.For<HttpContextBase>();

      new CancelOrderPresenter(this.view, this.orderManager, this.orderProcessor) { HttpContext = this.httpContext };

      this.httpContext.Request.QueryString.Returns(new NameValueCollection { { "id", "yyy" }, { "user", "111" } });
      this.order = new Order { OrderId = "yyy", BuyerCustomerParty = new CustomerParty { SupplierAssignedAccountID = "111" } };
      this.orders = new Collection<Order> { this.order }.AsQueryable();
      this.orderManager.GetAll().Returns(this.orders);
    }

    /// <summary>
    /// Should call order processor with provided order id.
    /// </summary>
    [Fact]
    public void ShouldCallOrderProcessorWithProvidedOrderId()
    {
      // Arrange

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.orderProcessor.Received().CancelOrder(this.order);
    }

    /// <summary>
    /// Should set success result if order cancelled without exception.
    /// </summary>
    [Fact]
    public void ShouldSetSuccessResultIfOrderCancelledWithoutException()
    {
      // Arrange

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.view.Model.Result.Should().Be("The order yyy has been cancelled successfully.");
    }

    /// <summary>
    /// Should set result to the exception message in case of exception.
    /// </summary>
    [Fact]
    public void ShouldSetResultToTheExceptionMessageInCaseOfException()
    {
      // Arrange
      this.orderProcessor.When(op => op.CancelOrder(this.order))
                         .Do(op => { throw new Exception("The order is already cancelled."); });

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.view.Model.Result.Should().Be("The order is already cancelled.");
    }

    /// <summary>
    /// Should initialize customer id if repository is user aware.
    /// </summary>
    [Fact]
    public void ShouldInitializeCustomerIdIfRepositoryIsUserAware()
    {
      // Arrange

      // Act
      this.view.Load += Raise.Event();

      // Assert
      ((IUserAware)this.orderManager).CustomerId.Should().Be("111");
    }

    /// <summary>
    /// Should not get and process order if id is not specified.
    /// </summary>
    [Fact]
    public void ShouldNotGetAndProcessOrderIfIdIsNotSpecified()
    {
      // Arrange
      this.httpContext.Request.QueryString.Returns(new NameValueCollection());

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.orderManager.DidNotReceive().GetAll();
    }

    /// <summary>
    /// Should set result to order id is not specified.
    /// </summary>
    [Fact]
    public void ShouldSetResultToOrderIdIsNotSpecified()
    {
      // Arrange
      this.httpContext.Request.QueryString.Returns(new NameValueCollection());

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.view.Model.Result.Should().Be("The order id is not specified.");
    }
  }
}