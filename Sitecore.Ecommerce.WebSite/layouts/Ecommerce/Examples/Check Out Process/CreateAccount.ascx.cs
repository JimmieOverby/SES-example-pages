// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateAccount.ascx.cs" company="Sitecore A/S">
//   Copyright (C) 2010 by Sitecore A/S
// </copyright>
// <summary>
//   Create Account control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.layouts.Ecommerce.CheckOutProcess
{
  using System;
  using System.Text;
  using System.Web.UI;
  using Globalization;
  using Texts = Sitecore.Ecommerce.Examples.Texts;

  /// <summary>
  /// Create Account control.
  /// </summary>
  public partial class CreateAccount : UserControl
  {
    /// <summary>
    /// Gets a value indicating whether this <see cref="CreateAccount"/> is visibility.
    /// </summary>
    /// <value><c>true</c> if visibility; otherwise, <c>false</c>.</value>
    public bool Visibility
    {
      get { return Sitecore.Context.User.IsAuthenticated; }
    }

    /// <summary>
    /// Gets the promt message.
    /// </summary>
    /// <returns>returns promt message</returns>
    public static string GetPromtMessage()
    {
      StringBuilder message = new StringBuilder();
      message.Append(Translate.Text(Texts.YouHaveNotCreatedAnAccount));
      message.Append("<br/>");
      message.Append(Translate.Text(Texts.WouldYouLikeToCreateALogin));

      return message.ToString();
    }

    /// <summary>
    /// Gets the confiramtion message.
    /// </summary>
    /// <returns>comfiramtion message</returns>
    public static string GetConfiramtionMessage()
    {
      StringBuilder message = new StringBuilder();
      message.Append(Translate.Text(Texts.ThankYouForCreatingAnAccount));
      message.Append("<br/>");
      message.Append(Translate.Text(Texts.YouWillReceiveAnEmailWithInformationAboutYourNewAccountSoon));

      return message.ToString();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      this.DataBind();
    }
  }
}