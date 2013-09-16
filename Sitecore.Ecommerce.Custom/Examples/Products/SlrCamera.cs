// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SlrCamera.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the SLR camera class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.Examples.Products
{
  using Ecommerce.Products;

  /// <summary>
  /// Defines the SLR camera class.
  /// </summary>
  public class SlrCamera : Product
  {
    /// <summary>
    /// Gets or sets the effective pixels.
    /// </summary>
    /// <value>
    /// The effective pixels.
    /// </value>
    public string EffectivePixels { get; set; }

    /// <summary>
    /// Gets or sets the image sensor.
    /// </summary>
    /// <value>
    /// The image sensor.
    /// </value>
    public string ImageSensor { get; set; }
  }
}