// <copyright file="CheckOutPaymentErrorPage.ascx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.layouts.Ecommerce.CheckOutProcess
{
  using System;
  using System.Web.UI;

  /// <summary>
  /// The error page.
  /// </summary>
  public partial class CheckOutPaymentErrorPage : UserControl
  {
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.IsPostBack)
      {
        this.paymentError.Visible = false;
        this.lblErrorText.Visible = false;
      }

      string message = this.Session["paymentErrorMessage"] as string;

      if (!string.IsNullOrEmpty(message))
      {
        this.sctErrorText.Visible = false;
        this.lblErrorText.Visible = true;

        this.lblErrorText.Text = message;

        this.Session.Remove("paymentErrorMessage");
      }
    }
  }
}