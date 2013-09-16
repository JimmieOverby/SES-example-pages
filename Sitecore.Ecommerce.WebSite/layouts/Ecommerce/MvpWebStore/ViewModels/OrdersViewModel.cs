// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrdersViewModel.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The orders view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.ViewModels
{
  using System.Collections.Generic;
  using Sitecore.Ecommerce.OrderManagement.Orders;

  /// <summary>
  /// Defines the OrdersViewModel. 
  /// Used by IOrderView
  /// to render all orders.
  /// </summary>
  public class OrdersViewModel
  {
    /// <summary>
    /// Gets or sets the categories.
    /// </summary>
    /// <value>
    /// The categories.
    /// </value>
    [NotNull]
    public IEnumerable<Order> Orders { get; set; }

    /// <summary>
    /// Gets or sets the serialized orders.
    /// </summary>
    /// <value>
    /// The serialized orders.
    /// </value>
    public string SerializedOrders { get; set; }
  }
}