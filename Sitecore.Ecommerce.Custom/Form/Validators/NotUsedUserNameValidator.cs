namespace Sitecore.Ecommerce.Form.Validators
{
  using System.Web.Security;
  using System.Web.UI.WebControls;
  using Sitecore.Form.Core.Validators;

  /// <summary>
  /// Validates if the username provided is in use.
  /// </summary>
  public class NotUsedUserNameValidator : FormCustomValidator
  {
    // private const string ExtranetPrefix = @"extranet\";

    /// <summary>
    /// Initate the Not Used User Name Validator
    /// </summary>
    public NotUsedUserNameValidator()
    {
      this.ServerValidate += this.OnValidate;
    }

    /// <summary>
    /// Username is not in use before.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.
    /// </param>
    private void OnValidate(object source, ServerValidateEventArgs args)
    {
      args.IsValid = Membership.GetUser(Sitecore.Context.Domain.Name + @"\" + args.Value) == null;
    }
  }
}