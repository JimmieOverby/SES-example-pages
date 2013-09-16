// <copyright file="FormUtil.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
using Sitecore.Ecommerce.DomainModel.Configurations;

namespace Sitecore.Ecommerce.Forms
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Linq;
  using System.Web.Security;
  using System.Web.UI.WebControls;
  using Sitecore.Data.Items;

  /// <summary>
  /// </summary>
  public static class FormUtil
  {
    /// <summary>Value='Kode'</summary>
    public const string ECOMMERCE_SETTINGS_COUNTRIES_CODE = "Code";

    /// <summary>Value='NO'</summary>
    public const string ECOMMERCE_SETTINGS_COUNTRIES_DEFAULT_CODE = "NO";

    /// <summary>Value='Norge'</summary>
    public const string ECOMMERCE_SETTINGS_COUNTRIES_DEFAULT_NAME = "Norway";

    /// <summary>Value='Navn'</summary>
    public const string ECOMMERCE_SETTINGS_COUNTRIES_TITLE = "Title";

    /// <summary>
    /// Gets the countries registered in Sitecore.
    /// </summary>
    /// <value>The countries.</value>
    private static IEnumerable<Item> Countries
    {
      get
      {
        var countries = new List<Item>();
        BusinessCatalogSettings businessCatalogSettings = Context.Entity.GetConfiguration<BusinessCatalogSettings>();
        Item countirsItem = Sitecore.Context.Database.GetItem(businessCatalogSettings.CountriesLink);
        Item[] items = countirsItem.Children.ToArray();

        // Array.Sort (items);
        foreach (Item item in items)
        {
          countries.Add(item);
        }

        IOrderedEnumerable<Item> countriesSorted = from c in countries
                                                   orderby c.Name
                                                   select c;

        return countriesSorted;
      }
    }

    /// <summary>
    /// Deletes a Sitecore user.
    /// </summary>
    /// <param name="username">
    /// The user´s username.
    /// </param>
    public static void DeleteSitecoreUser(string username)
    {
      try
      {
        Membership.DeleteUser(username);
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
      }
    }

    /// <summary>
    /// Fills a given <see cref="ListControl"/> with the countries registered in Sitecore.
    /// </summary>
    /// <param name="listControl">
    /// The list control.
    /// </param>
    public static void FillCountryDropdown(ListControl listControl)
    {
      foreach (Item country in Countries)
      {
        listControl.Items.Add(new ListItem(country[ECOMMERCE_SETTINGS_COUNTRIES_TITLE], country.ID.ToString()));
      }

      // TODO: Implement default code.
      listControl.SelectedValue = ECOMMERCE_SETTINGS_COUNTRIES_DEFAULT_CODE;
    }

    /// <summary>
    /// Gets the country code for a given country name.
    /// </summary>
    /// <param name="countryName">
    /// Name of the country.
    /// </param>
    /// <returns>
    /// </returns>
    public static string GetCountryCode(string countryName)
    {
      foreach (Item item in Countries)
      {
        if (item[ECOMMERCE_SETTINGS_COUNTRIES_TITLE] == countryName)
        {
          string code = item[ECOMMERCE_SETTINGS_COUNTRIES_CODE];
          return code;
        }
      }

      return ECOMMERCE_SETTINGS_COUNTRIES_DEFAULT_CODE;
    }

    /// <summary>
    /// Gets the name of the country for a given country code.
    /// </summary>
    /// <param name="countryCode">
    /// The country code.
    /// </param>
    /// <returns>
    /// </returns>
    public static string GetCountryName(string countryCode)
    {
      foreach (Item item in Countries)
      {
        if (item[ECOMMERCE_SETTINGS_COUNTRIES_CODE] == countryCode)
        {
          string name = item[ECOMMERCE_SETTINGS_COUNTRIES_TITLE];
          return name;
        }
      }

      return ECOMMERCE_SETTINGS_COUNTRIES_DEFAULT_NAME;
    }
  }
}