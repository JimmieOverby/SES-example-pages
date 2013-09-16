// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryProductsPresenterTest.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the CategoryProductsPresenterTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Tests.Presenters
{
  using System.Collections.ObjectModel;
  using System.Collections.Specialized;
  using System.Web;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Ecommerce.DomainModel.Products;
  using Sitecore.Ecommerce.MvpWebStore.Presenters;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using Xunit;

  /// <summary>
  /// Defines the CategoryProductsPresenterTest type.
  /// </summary>
  public class CategoryProductsPresenterTest
  {
    /// <summary>
    /// The view.
    /// </summary>
    private readonly ICategoryProductsView view;

    /// <summary>
    /// The repository.
    /// </summary>
    private readonly IProductRepository repository;

    /// <summary>
    /// The HTTP context.
    /// </summary>
    private readonly HttpContextBase httpContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryProductsPresenterTest" /> class.
    /// </summary>
    public CategoryProductsPresenterTest()
    {
      this.view = Substitute.For<ICategoryProductsView>();
      this.repository = Substitute.For<IProductRepository>();
      this.httpContext = Substitute.For<HttpContextBase>();

      new CategoryProductsPresenter(this.view, this.repository) { HttpContext = this.httpContext };
    }

    /// <summary>
    /// Should return list of products per category.
    /// </summary>
    [Fact]
    public void ShouldLoadListOfProductsInCategory()
    {
      // Arrange
      var products = new Collection<ProductBaseData>
        {
          new ProductBaseData { Code = "D200" },
          new ProductBaseData { Code = "D3" }
        };

      this.repository.GetSubItems<ProductBaseData, ProductCategory>("Cameras", false).Returns(products);
      this.httpContext.Request.QueryString.Returns(new NameValueCollection { { "cat", "Cameras" } });

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.view.Model.Products.Should().BeEquivalentTo(products);
    }

    /// <summary>
    /// Should not load categories if no category specified in query string.
    /// </summary>
    [Fact]
    public void ShouldNotLoadCategoriesIfNoCategorySpecifiedInQueryString()
    {
      // Arrange
      this.httpContext.Request.QueryString.Returns(new NameValueCollection());

      // Act
      this.view.Load += Raise.Event();

      // Assert
      this.view.Model.Products.Should().BeEmpty();
      this.repository.DidNotReceiveWithAnyArgs().GetSubItems<ProductBaseData, ProductCategory>(null, false);
    }
  }
}