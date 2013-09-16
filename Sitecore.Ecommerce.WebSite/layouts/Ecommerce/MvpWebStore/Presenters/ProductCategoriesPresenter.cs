// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCategoriesPresenter.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the product categories presenter.
//   The presenter loads all the product categories located in repository
//   and puts it into view model.
//   The repository is set by Products Link in Business Catalog Setting.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Presenters
{
  using System;
  using Sitecore.Ecommerce.DomainModel.Products;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using Sitecore.Ecommerce.Search;
  using WebFormsMvp;

  /// <summary>
  /// Defines the product categories presenter.
  /// The presenter loads all the product categories located in repository
  /// and puts it into view model.
  /// The repository is set by Products Link in Business Catalog Setting.
  /// </summary>
  public class ProductCategoriesPresenter : Presenter<IProductCategoriesView>
  {
    /// <summary>
    /// The product category template id.
    /// </summary>
    private const string ProductCategoryTemplateId = "{AF520323-7586-4D80-92E1-538828E11B70}";

    /// <summary>
    /// The product repository.
    /// </summary>
    private readonly IProductRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductCategoriesPresenter" /> class.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="repository">The repository.</param>
    public ProductCategoriesPresenter(IProductCategoriesView view, IProductRepository repository)
      : base(view)
    {
      this.repository = repository;

      view.Load += this.Load;
    }

    /// <summary>
    /// Loads the specified sender.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="eventArgs">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void Load(object sender, EventArgs eventArgs)
    {
      var query = new Query();
      query.AppendAttribute("templateid", ProductCategoryTemplateId, MatchVariant.Exactly);

      // Gets the list of categories and initializes the model.
      var categories = this.repository.Get<ProductCategory, Query>(query);

      this.View.Model.Categories = categories;
    }
  }
}