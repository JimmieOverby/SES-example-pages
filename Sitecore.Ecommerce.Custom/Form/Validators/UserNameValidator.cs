namespace Sitecore.Ecommerce.Form.Validators
{
  using System.Web.UI.WebControls;
  using Sitecore.Form.Core.Validators;

  /// <summary>
  /// </summary>
  public class UserNameValidator : FormCustomValidator
  {
    /// <summary>
    /// Initate User Name Validator.
    /// </summary>
    public UserNameValidator()
    {
      this.ServerValidate += OnValidate;
    }

    /// <summary>
    /// Called when [validate].
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="args">
    /// The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.
    /// </param>
    private static void OnValidate(object source, ServerValidateEventArgs args)
    {
      // TODO: Use web.config settings for user validation string to validate if username is valid.
      args.IsValid = true;
    }
  }
}