// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XPriceManagerTest.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines XPriceManagerTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Tests.Domain
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Ecommerce.DomainModel.Currencies;
  using Sitecore.Ecommerce.DomainModel.Prices;
  using Sitecore.Ecommerce.DomainModel.Products;
  using Sitecore.Ecommerce.MvpWebStore.Domain;
  using Sitecore.Ecommerce.Products;
  using Xunit;

  /// <summary>
  /// Defines XPriceManagerTest type.
  /// </summary>
  public class XPriceManagerTest
  {
    /// <summary>
    /// Should return product price.
    /// </summary>
    [Fact]
    public void ShouldReturnProductPrice()
    {
      // Arrange
      ProductBaseData product = new ProductBaseData { Code = "d1" };

      IProductRepository repository = Substitute.For<IProductRepository>();
      ProductPrice productPrice = new ProductPrice { PriceMatrix = "<el><price>100</price></el>" };
      repository.Get<ProductPrice>("d1").Returns(productPrice);

      TotalsFactory factory = Substitute.For<TotalsFactory>();
      factory.Create().Returns(new Totals());

      XProductPriceManager priceManager = new XProductPriceManager(repository, factory);

      // Act
      Totals result = priceManager.GetProductTotals<Totals, ProductBaseData, Currency>(product, null);

      // Assert
      result.PriceExVat.Should().Be(100);
    }
  }
}
