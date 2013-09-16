// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryProductsViewModel.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the CategoryProductsViewModel. Used by CategoryProductsView
//   to render list of products in category.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.ViewModels
{
  using System.Collections.Generic;
  using Sitecore.Ecommerce.DomainModel.Products;

  /// <summary>
  /// Defines the CategoryProductsViewModel. Used by CategoryProductsView
  /// to render list of products in category.
  /// </summary>
  public class CategoryProductsViewModel
  {
    /// <summary>
    /// Gets or sets the products.
    /// </summary>
    /// <value>The products.</value>
    [NotNull]
    public IEnumerable<ProductBaseData> Products { get; set; }
  }
}