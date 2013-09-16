// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CancelOrderViewModel.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the CancelOrderViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.ViewModels
{
  /// <summary>
  /// Defines the CancelOrderViewModel. 
  /// Used by ICancelOrderView
  /// to render the result of the order cancelation.
  /// </summary>
  public class CancelOrderViewModel
  {
    /// <summary>
    /// Gets or sets the result.
    /// </summary>
    /// <value>
    /// The result.
    /// </value>
    public string Result { get; set; }
  }
}