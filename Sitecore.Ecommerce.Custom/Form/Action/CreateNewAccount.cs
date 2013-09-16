// <copyright file="CreateNewAccount.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved. 
// </copyright>
namespace Sitecore.Ecommerce.Form.Action
{
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.Linq;
  using Analytics.Components;
  using Diagnostics;
  using DomainModel.Addresses;
  using DomainModel.Data;
  using DomainModel.Users;
  using Forms;
  using Forms.Actions;
  using Sitecore.Data;
  using Sitecore.Ecommerce.Data;
  using Sitecore.Form.Core.Client.Data.Submit;
  using Sitecore.Form.Core.Configuration;
  using Sitecore.Form.Core.Pipelines.RenderForm;
  using Sitecore.Form.Submit;
  using Utils;

  /// <summary>
  /// The create new account.
  /// </summary>
  public class CreateNewAccount : ILoad, ISubmit
  {
    #region Protected methods

    /// <summary>
    /// Submits the specified formid.
    /// </summary>
    /// <param name="formid">The formid.</param>
    /// <param name="fields">The fields.</param>
    public void Submit(ID formid, AdaptedResultList fields)
    {
      if (StaticSettings.MasterDatabase == null)
      {
        Log.Warn("'Create Item' action : master database is unavailable", this);
      }

      NameValueCollection form = new NameValueCollection();
      ActionHelper.FillFormData(form, fields, this.ProcessField);

      // If username and password was given, create a user.
      if (string.IsNullOrEmpty(form["Email"]) || string.IsNullOrEmpty(form["Password"]))
      {
        return;
      }

      string name = form["Email"].Trim();
      string password = form["Password"];
      string email = form["Email"];

      string fullNickName = Sitecore.Context.Domain.GetFullName(name);

      ICustomerManager<CustomerInfo> customerManager = Context.Entity.Resolve<ICustomerManager<CustomerInfo>>();
      CustomerInfo customerInfo = customerManager.CreateCustomerAccount(fullNickName, password, email);

      if (customerInfo == null)
      {
        AnalyticsUtil.AuthentificationAccountCreationFailed();
        return;
      }

      foreach (string key in form.AllKeys)
      {
        customerInfo[key] = form[key];
      }

      customerInfo.BillingAddress.Name = form["Name"];
      customerInfo.BillingAddress.Address = form["Address"];
      customerInfo.BillingAddress.Zip = form["Zip"];
      customerInfo.BillingAddress.City = form["City"];
      customerInfo.BillingAddress.State = form["State"];

      if (!string.IsNullOrEmpty(form["Country"]))
      {
        IEntityProvider<Country> countryProvider = Context.Entity.Resolve<IEntityProvider<Country>>();
        customerInfo.BillingAddress.Country = countryProvider.Get(form["Country"]);
      }

      if (form["HideThisSection"] == "1")
      {
        customerInfo.ShippingAddress.Name = form["Name"];
        customerInfo.ShippingAddress.Address = form["Address"];
        customerInfo.ShippingAddress.Zip = form["Zip"];
        customerInfo.ShippingAddress.City = form["City"];
        customerInfo.ShippingAddress.State = form["State"];

        if (!string.IsNullOrEmpty(form["ShippingCountry"]))
        {
          IEntityProvider<Country> countryProvider = Context.Entity.Resolve<IEntityProvider<Country>>();
          customerInfo.ShippingAddress.Country = countryProvider.Get(form["ShippingCountry"]);
        }
      }
      else
      {
        EntityHelper entityHepler = Context.Entity.Resolve<EntityHelper>();
        AddressInfo targetAddressInfo = customerInfo.ShippingAddress;

        entityHepler.CopyPropertiesValues(customerInfo.BillingAddress, ref targetAddressInfo);
      }

      customerManager.UpdateCustomerProfile(customerInfo);
      customerManager.CurrentUser = customerInfo;

      AnalyticsUtil.AuthentificationAccountCreated();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// The form load event handler.
    /// </summary>
    /// <param name="isPostback">Gets a value that indicates whether the form is being rendered for the first time or is being loaded in response to a postback.</param>
    /// <param name="args">The render form arguments.</param>
    public void Load(bool isPostback, RenderFormArgs args)
    {
      HtmlFormModifier form = new HtmlFormModifier(args);

      form.DisableInputField("UserName");
    }

    /// <summary> Process for fields </summary>
    /// <param name="fieldName">field name</param>
    /// <param name="fieldValue">field value</param>
    /// <returns>returns processed field</returns>
    private NameValueCollection ProcessField(string fieldName, string fieldValue)
    {
      NameValueCollection field = new NameValueCollection();

      switch (fieldName)
      {
        case "Country":
        case "ShippingCountry":
          fieldValue = (!string.IsNullOrEmpty(fieldValue)) ? BusinessCatalogUtil.GetOptionFromName(fieldValue, BusinessCatalogUtil.COUNTRIES, "Code") : string.Empty;
          break;
      }

      field.Add(fieldName, fieldValue);
      return field;
    }

    #endregion Private Methods
  }
}