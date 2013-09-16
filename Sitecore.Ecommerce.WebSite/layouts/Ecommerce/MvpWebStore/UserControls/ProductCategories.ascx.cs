// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCategories.ascx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the Product Categories user control.
//   Renders list of the product categories in repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.UserControls
{
  using System;
  using Sitecore.Ecommerce.MvpWebStore.ViewModels;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using WebFormsMvp.Web;

  /// <summary>
  /// Defines the Product Categories user control.
  /// Renders list of the product categories in repository.
  /// </summary>
  public partial class ProductCategories : MvpUserControl<ProductCategoriesViewModel>, IProductCategoriesView
  {
    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      this.ProductCategotyList.DataSource = this.Model.Categories;
    }
  }
}