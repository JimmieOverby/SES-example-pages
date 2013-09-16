// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCategoriesViewModel.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the ProductCategoriesViewModel. Used by ProductCategoriesView
//   to render list of product categories.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.ViewModels
{
  using System.Collections.Generic;
  using Sitecore.Ecommerce.DomainModel.Products;

  /// <summary>
  /// Defines the ProductCategoriesViewModel. Used by ProductCategoriesView
  /// to render list of product categories.
  /// </summary>
  public class ProductCategoriesViewModel
  {
    /// <summary>
    /// Gets or sets the categories.
    /// </summary>
    /// <value>The categories.</value>
    [NotNull]
    public IEnumerable<ProductCategory> Categories { get; set; }
  }
}