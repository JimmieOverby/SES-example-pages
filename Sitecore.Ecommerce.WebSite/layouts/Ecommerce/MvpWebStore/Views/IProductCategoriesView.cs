// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductCategoriesView.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the IProductCategoriesView interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Views
{
  using Sitecore.Ecommerce.MvpWebStore.ViewModels;
  using WebFormsMvp;

  /// <summary>
  /// Defines the IProductCategoriesView interface.
  /// </summary>
  public interface IProductCategoriesView : IView<ProductCategoriesViewModel>
  {
  }
}