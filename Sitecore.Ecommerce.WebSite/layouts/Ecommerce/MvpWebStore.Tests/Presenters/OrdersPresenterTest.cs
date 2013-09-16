// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrdersPresenterTest.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The orders presenter test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Tests.Presenters
{
  using System;
  using System.Collections.ObjectModel;
  using System.Collections.Specialized;
  using System.IO;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Web;
  using FluentAssertions;
  using Newtonsoft.Json;
  using NSubstitute;
  using Sitecore.Ecommerce.Data;
  using Sitecore.Ecommerce.MvpWebStore.Domain;
  using Sitecore.Ecommerce.MvpWebStore.Presenters;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using Sitecore.Ecommerce.OrderManagement;
  using Sitecore.Ecommerce.OrderManagement.Orders;
  using Sitecore.Ecommerce.Visitor.OrderManagement;
  using Xunit;

  /// <summary>
  /// The orders presenter test.
  /// </summary>
  public class OrdersPresenterTest
  {
    /// <summary>
    /// The order repository.
    /// </summary>
    private readonly VisitorOrderManager orderManager;

    /// <summary>
    /// The view
    /// </summary>
    private readonly IOrdersView view;

    /// <summary>
    /// The orders.
    /// </summary>
    private readonly IQueryable<Order> orders;

    /// <summary>
    /// The HTTP context.
    /// </summary>
    private readonly HttpContextBase httpContext;

    /// <summary>
    /// The core order manager
    /// </summary>
    private readonly SampleOrderManager coreOrderManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrdersPresenterTest" /> class.
    /// </summary>
    public OrdersPresenterTest()
    {
      this.orderManager = Substitute.For<VisitorOrderManager, IUserAware>();
      this.coreOrderManager = Substitute.For<SampleOrderManager>(Substitute.For<CoreOrderStateConfiguration>(), Substitute.For<Repository<Order>>());
      var order1 = new Order { OrderId = "first", BuyerCustomerParty = new CustomerParty { SupplierAssignedAccountID = "111" } };
      var order2 = new Order { OrderId = "second", BuyerCustomerParty = new CustomerParty { SupplierAssignedAccountID = "111" } };
      this.orders = new Collection<Order> { order1, order2 }.AsQueryable();
      this.orderManager.GetAll().Returns(this.orders);
      this.coreOrderManager.GetAllOrders(Arg.Any<Expression<Func<Order, bool>>>()).Returns(this.orders);
      this.view = Substitute.For<IOrdersView>();
      this.httpContext = Substitute.For<HttpContextBase>();
      new OrdersPresenter(this.view, this.orderManager, this.coreOrderManager) { HttpContext = this.httpContext };

      this.httpContext.Request.QueryString.Returns(new NameValueCollection { { "user", "111" } });
    }

    /// <summary>
    /// Should list all orders.
    /// </summary>
    [Fact]
    public void ShouldListAllOrders()
    {
      // Arrange

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.view.Model.Orders.Should().BeEquivalentTo(this.orders);
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
    /// Should not list orders if user is not specified.
    /// </summary>
    [Fact]
    public void ShouldNotListOrdersIfUserIsNotSpecified()
    {
      // Arrange
      this.httpContext.Request.QueryString.Returns(new NameValueCollection());

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.orderManager.DidNotReceive().GetAll();
    }

    /// <summary>
    /// Should set serialized orders.
    /// </summary>
    [Fact]
    public void ShouldSetSerializedOrders()
    {
      // Arrange
      var settings = new JsonSerializerSettings
      {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Ignore,
        PreserveReferencesHandling = PreserveReferencesHandling.Objects
      };
      var expected = JsonConvert.SerializeObject(this.orders, Formatting.None, settings);

      // Act
      this.view.Export += Raise.Event();

      // Assert
      this.view.Model.SerializedOrders.Should().Be(expected);
    }

    /// <summary>
    /// Should set response information on export.
    /// </summary>
    [Fact]
    public void ShouldSetResponseInformationOnExport()
    {
      // Arrange

      // Act
      this.view.Export += Raise.Event();

      // Assert
      this.httpContext.Response.Received().Clear();
      this.httpContext.Response.ContentType.Should().Be("application/json");
      this.httpContext.Response.Received().AddHeader("content-disposition", "attachment; filename=\"orders.json\"");
      this.httpContext.Response.Received().WriteFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "orders.json"));
      this.httpContext.Response.Received().Flush();
      this.httpContext.Response.Received().End();
    }

    /// <summary>
    /// Should save deserialized orders.
    /// </summary>
    [Fact(Skip = "")]
    public void ShouldSaveDeserializedOrders()
    {
      // Arrange

      // Act
      this.view.Import += Raise.Event();

      // Assert
      this.coreOrderManager.Received().SaveSingleOrder(Arg.Any<Order>());
    }
  }
}