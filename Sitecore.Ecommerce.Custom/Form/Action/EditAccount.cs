// <copyright file="EditAccount.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved. 
// </copyright>
namespace Sitecore.Ecommerce.Form.Action
{
  using System.Collections.Specialized;
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
  /// The Edit Account form.
  /// </summary>
  public class EditAccount : ILoad, ISubmit
  {
    #region Public methods

    /// <summary>
    /// The form load event handler. Retrieves the current user information from Sitecore. The fieldnames are defined in the belonging webform.
    /// </summary>
    /// <param name="isPostback">Gets a value that indicates whether the form is being rendered for the first time or is being loaded in response to a postback.</param>
    /// <param name="args">The render form arguments.</param>
    public void Load(bool isPostback, RenderFormArgs args)
    {
      HtmlFormModifier form = new HtmlFormModifier(args);

      if (isPostback)
      {
        return;
      }

      ICustomerManager<CustomerInfo> customerManager = Context.Entity.Resolve<ICustomerManager<CustomerInfo>>();

      // Sets the Billing and Shipping Address from the user object.
      form.SetInputValue("Name", customerManager.CurrentUser.BillingAddress.Name);
      form.SetInputValue("Address", customerManager.CurrentUser.BillingAddress.Address);
      form.SetInputValue("Zip", customerManager.CurrentUser.BillingAddress.Zip);
      form.SetInputValue("City", customerManager.CurrentUser.BillingAddress.City);

      form.SetSelectedDropListValue("State", customerManager.CurrentUser.BillingAddress.State);
      form.SetSelectedDropListValue("Country", customerManager.CurrentUser.BillingAddress.Country.Title);

      foreach (string key in customerManager.CurrentUser.CustomProperties.AllKeys)
      {
        form.SetInputValue(key, customerManager.CurrentUser.CustomProperties[key]);
      }
    }

    /// <summary> Updates the customer information in both Sitecore and in session </summary>
    /// <param name="formid"> The formid. </param>
    /// <param name="fields"> The fields. </param>
    public void Submit(ID formid, AdaptedResultList fields)
    {
      if (StaticSettings.MasterDatabase == null)
      {
        Log.Warn("'Create Item' action : master database is unavailable", this);
      }

      NameValueCollection form = new NameValueCollection();
      ActionHelper.FillFormData(form, fields, this.ProcessField);

      ICustomerManager<CustomerInfo> customerManager = Context.Entity.Resolve<ICustomerManager<CustomerInfo>>();
      CustomerInfo customerInfo = customerManager.GetCustomerInfo(customerManager.CurrentUser.NickName);

      if (customerInfo == null)
      {
        return;
      }

      customerInfo.CustomerId = customerManager.CurrentUser.CustomerId;

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

      foreach (string key in form.AllKeys)
      {
        customerInfo[key] = form[key];
      }

      EntityHelper entityHepler = Context.Entity.Resolve<EntityHelper>();
      AddressInfo targetAddressInfo = customerInfo.ShippingAddress;

      entityHepler.CopyPropertiesValues(customerInfo.BillingAddress, ref targetAddressInfo);

      customerManager.CurrentUser = customerInfo;
      customerManager.UpdateCustomerProfile(customerInfo);
    }

    #endregion

    #region Private methods

    /// <summary>
    /// The process field. </summary>
    /// <param name="fieldName"> The field name. </param>
    /// <param name="fieldValue"> The field value. </param>
    /// <returns> returns processed field </returns>
    private NameValueCollection ProcessField(string fieldName, string fieldValue)
    {
      NameValueCollection field = new NameValueCollection();

      switch (fieldName)
      {
        case "Country":
          fieldValue = (!string.IsNullOrEmpty(fieldValue)) ? BusinessCatalogUtil.GetOptionFromName(fieldValue, BusinessCatalogUtil.COUNTRIES, "Code") : string.Empty;
          break;
      }

      field.Add(fieldName, fieldValue);
      return field;
    }

    #endregion
  }
}