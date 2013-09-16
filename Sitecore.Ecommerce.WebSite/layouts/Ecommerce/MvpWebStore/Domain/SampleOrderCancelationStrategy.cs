// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleOrderCancelationStrategy.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the visitor order cancelation strategy class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Domain
{
  using Sitecore.Diagnostics;
  using Sitecore.Ecommerce.OrderManagement;
  using Sitecore.Ecommerce.OrderManagement.Orders;

  /// <summary>
  /// The simple implementation of the ProcessingStrategy abstract class.
  /// It just sets the State.Code of the provided order to the 'Cancelled' value
  /// without any collaboration with back-end. 
  /// </summary>
  public class SampleOrderCancelationStrategy : ProcessingStrategy
  {
    /// <summary>
    /// Processes an order, i.e. updates
    /// some fields of the one.
    /// </summary>
    /// <param name="order">The order.</param>
    public override void Process([NotNull] Order order)
    {
      Assert.ArgumentNotNull(order, "order");

      // Simply set the Order.State.Code and Order.State.Name 
      // to the 'Cancelled' value.
      order.State.Code = OrderStateCode.Cancelled;
      order.State.Name = OrderStateCode.Cancelled;
    }
  }
}