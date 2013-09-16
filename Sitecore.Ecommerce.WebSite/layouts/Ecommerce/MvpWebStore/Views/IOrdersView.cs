// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrdersView.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The i orders view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Views
{
  using System;
  using Sitecore.Ecommerce.MvpWebStore.ViewModels;
  using WebFormsMvp;

  /// <summary>
  /// The orders view.
  /// </summary>
  public interface IOrdersView : IView<OrdersViewModel>
  {
    /// <summary>
    /// Occurs when user exports the orders.
    /// </summary>
    event EventHandler<EventArgs> Export;

    /// <summary>
    /// Occurs when user imports the orders.
    /// </summary>
    event EventHandler<EventArgs> Import;
  }
}