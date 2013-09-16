// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryProducts.ascx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the Category Products user control.
//   Renders list of producs in category.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.UserControls
{
  using System;
  using Sitecore.Ecommerce.MvpWebStore.ViewModels;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using WebFormsMvp.Web;

  /// <summary>
  /// Defines the Category Products user control.
  /// Renders list of producs in category.
  /// </summary>
  public partial class CategoryProducts : MvpUserControl<CategoryProductsViewModel>, ICategoryProductsView
  {
    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      this.CategoryProductsRepeater.DataSource = this.Model.Products;
    }
  }
}