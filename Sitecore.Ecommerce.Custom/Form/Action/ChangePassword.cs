// <copyright file="ChangePassword.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved. 
// </copyright>
namespace Sitecore.Ecommerce.Form.Action
{
  using System.Collections.Specialized;
  using System.Web.Security;
  using DomainModel.Mails;
  using DomainModel.Users;
  using Sitecore.Data;
  using Sitecore.Form.Core.Client.Data.Submit;
  using Sitecore.Form.Core.Client.Submit;  
  using Sitecore.Form.Core.Pipelines.RenderForm;
  using Sitecore.Form.Submit;
  using Sitecore.Security.Authentication;
  using Unity;
  using Utils;

  /// <summary>
  /// The change password.
  /// </summary>
  public class ChangePassword : ISubmit
  {
    #region Constants

    /// <summary>
    /// The mail template name password changed.
    /// </summary>
    private const string MailTemplateNamePasswordChanged = "Your password has been changed";

    #endregion

    #region Public methods

    /// <summary> Submitts the new password for the associated sitecore membership account </summary>
    /// <param name="formid"> The formid. </param>
    /// <param name="fields"> The fields. </param>
    /// <exception cref="ValidatorException">The password information provided is incorrect.</exception>
    public void Submit(ID formid, AdaptedResultList fields)
    {
      NameValueCollection form = new NameValueCollection();
      ActionHelper.FillFormData(form, fields, null);
      
      if (!string.IsNullOrEmpty(form["CreatePassword"]))
      {
        ICustomerManager<CustomerInfo> customerManager = Context.Entity.Resolve<ICustomerManager<CustomerInfo>>();
        string customerId = customerManager.CurrentUser.NickName;

        MembershipUser membershipUser = Membership.GetUser(customerId);

        // Checks that the user information is correct for the user who want's to change password
        if (AuthenticationManager.Login(customerId, form["OldPassword"]) && !string.IsNullOrEmpty(customerId) && !string.IsNullOrEmpty(form["OldPassword"]))
        {
          // We can continue if the information is correct
          if (membershipUser != null)
          {
            string email = customerManager.CurrentUser.Email;

            if (!MainUtil.IsValidEmailAddress(email))
            {
              email = membershipUser.Email;
            }

            if (membershipUser.ChangePassword(form["OldPassword"], form["CreatePassword"]))
            {
              var param = new { Recipient = email };

              IMail mailProvider = Context.Entity.Resolve<IMail>();
              mailProvider.SendMail(MailTemplateNamePasswordChanged, param, string.Empty);
            }
          }
        }
        else
        {
          throw new ValidatorException("The password information provided is incorrect.");
        }
      }
    }

    #endregion

    #region Protected methods

    /// <summary>
    /// The on load. </summary>
    /// <param name="isPostback">Is PostBack </param>
    /// <param name="args">Render form args </param>
    protected void OnLoad(bool isPostback, RenderFormArgs args)
    {
    }

    #endregion
  }
}