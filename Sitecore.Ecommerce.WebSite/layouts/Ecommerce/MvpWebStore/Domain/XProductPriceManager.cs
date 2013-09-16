// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XProductPriceManager.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The simple implementation of IProductPriceManager interface.
//   It uses ProductRepository to find a product price matrix and
//   then parses the XML to get a price. Member prices and VAT calculations
//   are ignored.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Domain
{
  using System.Linq;
  using System.Xml.Linq;
  using Sitecore.Ecommerce.DomainModel.Currencies;
  using Sitecore.Ecommerce.DomainModel.Prices;
  using Sitecore.Ecommerce.DomainModel.Products;
  using Sitecore.Ecommerce.Products;

  /// <summary>
  /// The simple implementation of IProductPriceManager interface.
  /// It uses ProductRepository to find a product price matrix and 
  /// then parses the XML to get a price. Member prices and VAT calculations 
  /// are ignored.
  /// </summary>
  public class XProductPriceManager : IProductPriceManager
  {
    /// <summary>
    /// The product repository.
    /// </summary>
    private readonly IProductRepository repository;

    /// <summary>
    /// The totals factory.
    /// </summary>
    private readonly TotalsFactory totalsFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="XProductPriceManager" /> class.
    /// </summary>
    /// <param name="repository">The product repository.</param>
    /// <param name="totalsFactory">The totals factory.</param>
    public XProductPriceManager(IProductRepository repository, TotalsFactory totalsFactory)
    {
      this.repository = repository;
      this.totalsFactory = totalsFactory;
    }

    /// <summary>
    /// Gets the product totals.
    /// </summary>
    /// <typeparam name="TTotals">The type of the totals.</typeparam>
    /// <typeparam name="TProduct">The type of the product.</typeparam>
    /// <typeparam name="TCurrency">The type of the currency.</typeparam>
    /// <param name="product">The product.</param>
    /// <param name="currency">The currency.</param>
    /// <returns>Get product totals.</returns>
    public TTotals GetProductTotals<TTotals, TProduct, TCurrency>(TProduct product, TCurrency currency)
      where TTotals : Totals
      where TProduct : ProductBaseData
      where TCurrency : Currency
    {
      ProductPrice productPrice = this.repository.Get<ProductPrice>(product.Code);

      XElement element = XElement.Parse(productPrice.PriceMatrix);
      var price = element.Descendants("price").ElementAt(0).Value;

      var totals = this.totalsFactory.Create();
      totals.PriceExVat = decimal.Parse(price);

      return (TTotals)totals;
    }

    public TTotals GetProductTotals<TTotals, TProduct, TCurrency>(TProduct product, TCurrency currency, uint quantity)
      where TTotals : Totals
      where TProduct : ProductBaseData
      where TCurrency : Currency
    {
      throw new System.NotImplementedException();
    }
  }
}