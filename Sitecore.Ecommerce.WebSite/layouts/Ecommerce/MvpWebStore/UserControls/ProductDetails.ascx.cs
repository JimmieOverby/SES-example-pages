// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDetails.ascx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the Product Details user control.
//   Renders specific product details.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.UserControls
{
  using System;
  using System.Globalization;

  using Sitecore.Ecommerce.MvpWebStore.ViewModels;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using WebFormsMvp.Web;

  /// <summary>
  /// Defines the Product Details user control.
  /// Renders specific product details.
  /// </summary>
  public partial class ProductDetails : MvpUserControl<ProductDetailsViewModel>, IProductDetailsView
  {
    /// <summary>
    /// The Buy event.
    /// </summary>
    public event EventHandler<EventArgs> Buy;

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      this.Title.Text = this.Model.Product.Title;
      this.Description.Text = this.Model.Product.Description;
      this.Stock.Text = this.Model.Stock.ToString(CultureInfo.InvariantCulture);
      this.Price.Text = this.Model.Price.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Raises the <see cref="Buy" /> event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="eventArgs">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    protected void OnBuy(object sender, EventArgs eventArgs)
    {
      var buy = this.Buy;
      if (buy != null)
      {
        buy(this, eventArgs);
      }
    }
  }
}