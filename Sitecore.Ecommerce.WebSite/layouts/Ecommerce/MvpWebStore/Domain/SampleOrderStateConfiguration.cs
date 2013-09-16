// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleOrderStateConfiguration.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The sample order state configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Domain
{
  using Sitecore.Ecommerce.OrderManagement;

  /// <summary>
  /// The sample order state configuration.
  /// </summary>
  public class SampleOrderStateConfiguration : CoreOrderStateConfiguration
  {
    /// <summary>
    /// Determines whether the specified state is valid.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <returns>
    ///   <c>true</c> if the specified state is valid; otherwise, <c>false</c>.
    /// </returns>
    protected override bool IsValid(OrderManagement.Orders.State state)
    {
      return true;
    }
  }
}