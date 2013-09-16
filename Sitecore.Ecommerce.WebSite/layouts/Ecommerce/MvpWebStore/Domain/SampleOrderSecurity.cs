// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleOrderSecurity.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the SampleOrderSecurity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Domain
{
  using Sitecore.Ecommerce.OrderManagement;
  using Sitecore.Ecommerce.OrderManagement.Orders;
  using Sitecore.Ecommerce.Visitor.OrderManagement;

  /// <summary>
  /// The overriden version of the VisitorOrderSecurity class.
  /// The 'CanCancel(Order):bool' method is simplified.
  /// It doesn't perform any sophisticated check like a default one and
  /// doesn't collaborate in any way with back-end.
  /// The decision whether to allow to cancel an order is taken when the State is not null 
  /// and State.Code is within
  /// the following set: 'New', 'Open', 'InProcess'
  /// In opposite situation the cancellation is denied.
  /// The such is registered in the ~/App_Config/MvpWebStore.Unity.config.
  /// </summary>
  public class SampleOrderSecurity : VisitorOrderSecurity
  {
    /// <summary>
    /// Determines whether this instance can cancel the specified order.
    /// The decision is made according to the value of the Order.State.Code.
    /// When it equals 'New', 'Open', 'InProcess' cancellation is allowed.
    /// Otherwise not.
    /// </summary>
    /// <param name="order">The order.</param>
    /// <returns>
    ///   <c>true</c> if this instance can cancel the specified order; otherwise, <c>false</c>.
    /// </returns>
    public override bool CanCancel(Order order)
    {
      if (order.State != null)
      {
        if ((order.State.Code == OrderStateCode.New) || (order.State.Code == OrderStateCode.Open) || (order.State.Code == OrderStateCode.InProcess))
        {
          return true;
        }
      }

      return false;
    }
  }
}