// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICategoryProductsView.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the ICategoryProductsView interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Views
{
  using Sitecore.Ecommerce.MvpWebStore.ViewModels;
  using WebFormsMvp;

  /// <summary>
  /// Defines the ICategoryProductsView interface.
  /// </summary>
  public interface ICategoryProductsView : IView<CategoryProductsViewModel>
  {
  }
}