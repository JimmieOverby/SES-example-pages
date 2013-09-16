// <copyright file="CreditCard.aspx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.sitecore.admin.tests.IntegrationalPayment
{
  using System;
  using System.Web;
  using DomainModel.Payments;

  /// <summary>
  /// The credit card code behind.
  /// </summary>
  public partial class CreditCard : System.Web.UI.Page
  {
    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event to initialize the page.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      if (!Request.IsSecureConnection)
      {
        SslTools.SwitchToSsl();
      }

      base.OnInit(e);
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
      string userName = User.Identity.Name;
      this.greetingLabel.Text = "Welcome " + userName;
      HttpClientCertificate clientCertificate = Request.ClientCertificate;
      if (!clientCertificate.IsPresent)
      {
        this.certDataLabel.Text = "No certificate was found.";
        this.tblInfo.Visible = false;
        this.btnPay.Visible = false;
      }

      this.certDataLabel.Text = clientCertificate.Get("SUBJECT O");

      CreditCardInfo creditCardInfo = new CreditCardInfo
      {
        CardNumber = 4111111111111111,
        CardsHolderName = "John Doe",
        ExpirationDate = new DateTime(2012, 10, 01),
        SecurityCode = 431
      };

      this.txtCardNumber.Text = creditCardInfo.CardNumber.ToString();
      this.txtCardsHolderName.Text = creditCardInfo.CardsHolderName;
      this.txtExpirationDate.Text = creditCardInfo.ExpirationDate.ToString("MMyy");
      this.txtSecurityCode.Text = creditCardInfo.SecurityCode.ToString();
    }

    /// <summary>
    /// Called when pay button has been clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void OnPayButtonClick(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }
  }
}
