// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCategoriesPresenterTest.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the ProductCategoriesPresenterTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Tests.Presenters
{
  using System.Collections.ObjectModel;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Ecommerce.DomainModel.Products;
  using Sitecore.Ecommerce.MvpWebStore.Presenters;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using Sitecore.Ecommerce.Search;
  using Xunit;

  /// <summary>
  /// Defines the ProductCategoriesPresenterTest type.
  /// </summary>
  public class ProductCategoriesPresenterTest
  {
    /// <summary>
    /// Should load list of product categories.
    /// </summary>
    [Fact]
    public void ShouldLoadListOfProductCategories()
    {
      var view = Substitute.For<IProductCategoriesView>();
      
      var categories = new Collection<ProductCategory> { new ProductCategory { Name = "Cameras" }, new ProductCategory { Name = "Lenses" } };
      var repository = Substitute.For<IProductRepository>();
      repository.Get<ProductCategory, Query>(null).ReturnsForAnyArgs(categories);

      new ProductCategoriesPresenter(view, repository);

      // Act
      view.Load += Raise.Event();

      // Assert
      view.Model.Categories.Should().BeEquivalentTo(categories);
    }
  }
}