// <copyright file="PasswordValidator.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.Form.Validators
{
  using System.Web.UI.WebControls;
  using DomainModel.Users;
  using Sitecore.Form.Core.Validators;
  using Sitecore.Security.Authentication;

  /// <summary>
  /// Validator class that validates the current user towards a password provided.
  /// </summary>
  public class PasswordValidator : FormCustomValidator
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordValidator"/> class.
    /// </summary>
    public PasswordValidator()
    {
      this.ServerValidate += OnValidate;
    }

    /// <summary>
    /// Validates wheether the current user and password fits together.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.
    /// </param>
    private static void OnValidate(object source, ServerValidateEventArgs args)
    {
      ICustomerManager<CustomerInfo> customerManager = Ecommerce.Context.Entity.Resolve<ICustomerManager<CustomerInfo>>();
      string username = customerManager.CurrentUser.NickName;

      var provider = new FormsAuthenticationProvider();
      var helper = new AuthenticationHelper(provider);
      args.IsValid = helper.ValidateUser(username, args.Value);
    }
  }
}