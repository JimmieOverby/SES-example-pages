// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDetailsViewModel.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the ProductDetailsViewModel. Used by ProductDetailsView
//   to render product details.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.ViewModels
{
  using Sitecore.Ecommerce.Products;

  /// <summary>
  /// Defines the ProductDetailsViewModel. Used by ProductDetailsView
  /// to render product details.
  /// </summary>
  public class ProductDetailsViewModel
  {
    /// <summary>
    /// Gets or sets the product.
    /// </summary>
    /// <value>The product.</value>
    [NotNull]
    public Product Product { get; set; }

    /// <summary>
    /// Gets or sets the product stock.
    /// </summary>
    /// <value>The product stock.</value>
    public long Stock { get; set; }

    /// <summary>
    /// Gets or sets the product price.
    /// </summary>
    /// <value>The product price.</value>
    public decimal Price { get; set; }
  }
}