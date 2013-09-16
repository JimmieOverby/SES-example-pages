// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleOrderManager.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the SampleOrderManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Domain
{
  using System;
  using System.Collections.ObjectModel;
  using System.Linq;
  using System.Linq.Expressions;
  using Sitecore.Ecommerce.Data;
  using Sitecore.Ecommerce.OrderManagement;
  using Sitecore.Ecommerce.OrderManagement.Orders;

  /// <summary>
  /// The sample order manager.
  /// </summary>
  // TODO: ?
  public class SampleOrderManager //: CoreOrderManager
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SampleOrderManager" /> class.
    /// </summary>
    /// <param name="orderStateConfiguration">The order state configuration.</param>
    /// <param name="repository">The repository.</param>
    public SampleOrderManager(CoreOrderStateConfiguration orderStateConfiguration, Repository<Order> repository)
    {
      // this.StateConfiguration = orderStateConfiguration;
      // this.Repository = repository;
    }

    /// <summary>
    /// Gets all orders.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Collection of the orders.</returns>
    public virtual IQueryable<Order> GetAllOrders(Expression<Func<Order, bool>> expression)
    {
      // return this.GetOrders().Where(expression);
      return new Collection<Order>().AsQueryable();
    }

    /// <summary>
    /// Saves the single order.
    /// </summary>
    /// <param name="order">The order.</param>
    public virtual void SaveSingleOrder(Order order)
    {
      // this.SaveOrder(order);
    }
  }
}