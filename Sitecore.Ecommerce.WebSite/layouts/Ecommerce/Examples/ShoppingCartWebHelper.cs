// <copyright file="ShoppingCartWebHelper.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.layouts.Ecommerce.Examples
{
  using System;
  using System.Linq;
  using Analytics.Components;
  using Diagnostics;
  using DomainModel.Carts;
  using Sitecore.Analytics;

  /// <summary>
  /// Shopping Cart Web Helper
  /// </summary>
  public class ShoppingCartWebHelper
  {
    /// <summary>
    /// Adds to shopping cart.
    /// </summary>
    /// <param name="productCode">The product code.</param>
    /// <param name="quantity">The quantity.</param>    
    public static void AddToShoppingCart(string productCode, string quantity)
    {
      Assert.ArgumentNotNullOrEmpty(productCode, "productCode");
      Assert.ArgumentNotNullOrEmpty(quantity, "quantity");

      IShoppingCartManager shoppingCartManager = Context.Entity.Resolve<IShoppingCartManager>();

      uint q;
      if (string.IsNullOrEmpty(quantity) || !uint.TryParse(quantity, out q))
      {
        shoppingCartManager.AddProduct(productCode, 1);
      }
      else
      {
        shoppingCartManager.AddProduct(productCode, q);
      }

      ShoppingCart shoppingCart = Context.Entity.GetInstance<ShoppingCart>();
      ShoppingCartLine existingShoppingCartLine = shoppingCart.ShoppingCartLines.FirstOrDefault(p => p.Product.Code.Equals(productCode));

      try
      {
        Tracker.StartTracking();
        AnalyticsUtil.AddToShoppingCart(existingShoppingCartLine.Product.Code, existingShoppingCartLine.Product.Title, 1, existingShoppingCartLine.Totals.PriceExVat);
      }
      catch (Exception ex)
      {
        LogException(ex);
      }
    }

    /// <summary>
    /// Deletes from shopping cart.
    /// </summary>
    /// <param name="productCode">The product code.</param>
    public static void DeleteFromShoppingCart(string productCode)
    {
      Assert.ArgumentNotNullOrEmpty(productCode, "productCode");

      ShoppingCart shoppingCart = Context.Entity.GetInstance<ShoppingCart>();
      ShoppingCartLine existingShoppingCartLine = shoppingCart.ShoppingCartLines.FirstOrDefault(p => p.Product.Code.Equals(productCode));

      try
      {
        if (existingShoppingCartLine != null)
        {
          Tracker.StartTracking();
          AnalyticsUtil.ShoppingCartProductRemoved(existingShoppingCartLine.Product.Code, existingShoppingCartLine.Product.Title, existingShoppingCartLine.Quantity);
        }
      }
      catch (Exception ex)
      {
        LogException(ex);
      }

      IShoppingCartManager shoppingCartManager = Context.Entity.Resolve<IShoppingCartManager>();
      shoppingCartManager.RemoveProduct(productCode);
    }

    /// <summary>
    /// Deletes the product line from shopping cart.
    /// </summary>
    /// <param name="productCode">The product code.</param>
    public static void DeleteProductLineFromShoppingCart(string productCode)
    {
      Assert.ArgumentNotNullOrEmpty(productCode, "productCode");

      ShoppingCart shoppingCart = Context.Entity.GetInstance<ShoppingCart>();
      ShoppingCartLine existingShoppingCartLine = shoppingCart.ShoppingCartLines.FirstOrDefault(p => p.Product.Code.Equals(productCode));

      if (existingShoppingCartLine != null)
      {
        try
        {
          Tracker.StartTracking();
          AnalyticsUtil.ShoppingCartItemRemoved(existingShoppingCartLine.Product.Code, existingShoppingCartLine.Product.Title, existingShoppingCartLine.Quantity);
        }
        catch (Exception ex)
        {
          LogException(ex);
        }
      }

      IShoppingCartManager shoppingCartManager = Context.Entity.Resolve<IShoppingCartManager>();
      shoppingCartManager.RemoveProductLine(productCode);
    }

    /// <summary>
    /// Logs the exception.
    /// </summary>
    /// <param name="ex">The exception.</param>
    private static void LogException(Exception ex)
    {
      Log.Error("Analytics error:", ex);
    }
  }
}