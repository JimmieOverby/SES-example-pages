// <copyright file="UserInfo.aspx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.sitecore.admin.tests.IntegrationalPayment
{
  using System;

  /// <summary>
  /// The user info code behind.
  /// </summary>
  public partial class UserInfo : System.Web.UI.Page
  {
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
      this.txtName.Text = "John Doe";
      this.txtAddress.Text = "USA, NY";
    }

    /// <summary>
    /// Called when next button has been clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void OnNextButtonClick(object sender, EventArgs e)
    {
      Context.Response.Redirect("~/sitecore/admin/tests/IntegrationalPayment/CreditCard.aspx");
    }
  }
}
