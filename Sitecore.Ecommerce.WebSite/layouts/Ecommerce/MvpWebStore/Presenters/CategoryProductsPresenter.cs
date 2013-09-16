// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryProductsPresenter.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the category products presenter.
//   The presenter loads all the products for specific category
//   and puts them into view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Presenters
{
  using System;
  using System.Collections.ObjectModel;
  using Sitecore.Ecommerce.DomainModel.Products;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using WebFormsMvp;

  /// <summary>
  /// Defines the category products presenter.
  /// The presenter loads all the products for specific category
  /// and puts them into view model.
  /// </summary>
  public class CategoryProductsPresenter : Presenter<ICategoryProductsView>
  {
    /// <summary>
    /// The productRepository.
    /// </summary>
    private readonly IProductRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryProductsPresenter" /> class.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="repository">The productRepository.</param>
    public CategoryProductsPresenter(ICategoryProductsView view, IProductRepository repository)
      : base(view)
    {
      this.repository = repository;

      this.View.Load += this.Load;
    }

    /// <summary>
    /// Loads the specified sender.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void Load(object sender, EventArgs e)
    {
      // Gets the category id.
      string category = this.HttpContext.Request.QueryString["cat"];

      // In case the one is not specified initializes the order
      // with empty collection.
      if (string.IsNullOrEmpty(category))
      {
        this.View.Model.Products = new Collection<ProductBaseData>();
        return;
      }

      // Gets the list of products from the category and initializes the View.Model.
      var products = this.repository.GetSubItems<ProductBaseData, ProductCategory>(category, false);
      this.View.Model.Products = products;
    }
  }
}