// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductDetailsView.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the IProductDetailsView interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Views
{
  using System;
  using Sitecore.Ecommerce.MvpWebStore.ViewModels;
  using WebFormsMvp;

  /// <summary>
  /// Defines the IProductDetailsView interface.
  /// </summary>
  public interface IProductDetailsView : IView<ProductDetailsViewModel>
  {
    /// <summary>
    /// Occurs when user buys a product.
    /// </summary>
    event EventHandler<EventArgs> Buy;
  }
}