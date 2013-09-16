// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CancelOrder.ascx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The cancel order.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.UserControls
{
  using System;
  using Sitecore.Ecommerce.MvpWebStore.ViewModels;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using WebFormsMvp.Web;

  /// <summary>
  /// Defines the Cancel Order user control.
  /// Renders result of the order cancelation.
  /// </summary>
  public partial class CancelOrder : MvpUserControl<CancelOrderViewModel>, ICancelOrderView
  {
    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
   protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.Result.Text = this.Model.Result;
    }
  }
}