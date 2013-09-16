// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PsCamera.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the SLR camera class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.Examples.Products
{
  using Data;
  using Ecommerce.Products;

  /// <summary>
  /// Defines the SLR camera class.
  /// </summary>
  public class PsCamera : Product
  {
    /// <summary>
    /// Gets or sets the effective pixels.
    /// </summary>
    /// <value>
    /// The effective pixels.
    /// </value>
    public string Monitor { get; set; }
  }
}