// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lense.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the lense class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.Examples.Products
{
  using Ecommerce.Products;

  /// <summary>
  /// Defines the lense class.
  /// </summary>
  public class Lense : Product
  {
    /// <summary>
    /// Gets or sets the length of the focal.
    /// </summary>
    /// <value>
    /// The length of the focal.
    /// </value>
    public string FocalLength { get; set; }
  }
}