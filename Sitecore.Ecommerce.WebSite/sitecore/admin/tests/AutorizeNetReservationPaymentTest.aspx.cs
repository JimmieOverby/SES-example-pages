// <copyright file="AutorizeNetReservationPaymentTest.aspx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.sitecore.admin.tests
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Web;
  using Diagnostics;
  using DomainModel.Addresses;
  using DomainModel.Carts;
  using DomainModel.Currencies;
  using DomainModel.Payments;
  using DomainModel.Prices;
  using DomainModel.Shippings;
  using DomainModel.Users;
  using Payments;
  using Payments.AuthorizeNet;
  using Products;
  using Sites;
  using Text;
  using Unity;

  /// <summary>
  /// The AutorizeNetReservationPaymentTest page.
  /// </summary>
  public partial class AutorizeNetReservationPaymentTest : System.Web.UI.Page
  {
    /// <summary>
    /// The reservable provider.
    /// </summary>
    private static IReservableOnlinePaymentProvider paymentProvider;

    /// <summary>
    /// Payment arguments
    /// </summary>
    private static PaymentArgs paymentArgs;

    /// <summary>Payment system</summary>
    private static DomainModel.Payments.PaymentSystem paymentSystem;

    /// <summary>
    /// Instance of payment URL resolver
    /// </summary>
    private static PaymentUrlResolver paymentUrlResolver;

    /// <summary>
    /// The reservation ticket.
    /// </summary>
    private static ReservationTicket ticket;

    /// <summary>
    /// The return page url.
    /// </summary>
    private static UrlString returnUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutorizeNetReservationPaymentTest"/> class.
    /// </summary>
    public AutorizeNetReservationPaymentTest()
    {
      UrlString url = new UrlString
      {
        HostName = HttpContext.Current.Request.Url.Host,
        Path = "/sitecore/admin/tests/AutorizeNetReservationPaymentTest.aspx",
      };
      returnUrl = url;
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
      this.CaptureMoney.Visible = false;
      this.CancelTransaction.Visible = false;
      this.ReserveMoney.Visible = true;

      if (Page.IsPostBack)
      {
        return;
      }

      string status = HttpContext.Current.Request.QueryString["ec_action"];

      if (string.IsNullOrEmpty(status))
      {
        return;
      }

      if (status == "reserve")
      {
        this.CancelTransaction.Visible = true;
        this.CaptureMoney.Visible = true;
        this.ReserveMoney.Visible = false;
        using (new SiteContextSwitcher(SiteContextFactory.GetSiteContext("website")))
        {
          paymentProvider.ProcessPaymentCallBack(paymentSystem, paymentArgs, HttpContext.Current.Request);
          Assert.IsNotNull(ticket, "Reservation Ticket is null");

          this.Text.Text = string.Concat("Reservation Ticket Info. Total Price: ", ticket.Amount, "; AuthCode: ", ticket.AuthorizationCode, "; Transaction Id: ", ticket.TransactionNumber, "; Invoice Number: ", ticket.InvoiceNumber);
        }
      }

      if (status == "capture" || status == "cancel")
      {
        using (new SiteContextSwitcher(SiteContextFactory.GetSiteContext("website")))
        {
          paymentProvider.ProcessPaymentCallBack(paymentSystem, paymentArgs, HttpContext.Current.Request);
        }
      }
    }

    /// <summary>
    /// Handles the Click event of the ReserveMoney control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ReserveMoney_Click(object sender, EventArgs e)
    {
      using (new SiteContextSwitcher(SiteContextFactory.GetSiteContext("website")))
      {
        InitTestData();

        UrlString path = returnUrl;
        path.Add("ec_action", "reserve");
        paymentArgs.PaymentUrls.ReturnPageUrl = path.ToString();

        paymentProvider.InvokePayment(paymentSystem, paymentArgs);
      }
    }

    /// <summary>
    /// Handles the Click event of the CaptureMoney control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void CaptureMoney_Click(object sender, EventArgs e)
    {
      Assert.IsNotNull(ticket, "Reservation Ticket is null");

      UrlString path = returnUrl;
      path.Add("ec_action", "capture");
      paymentArgs.PaymentUrls.ReturnPageUrl = path.ToString();

      using (new SiteContextSwitcher(SiteContextFactory.GetSiteContext("website")))
      {
        paymentProvider.CapturePayment(paymentSystem, paymentArgs, ticket);

        ITransactionData transactionDataProvider = Ecommerce.Context.Entity.Resolve<ITransactionData>();
        this.Text.Text = string.Concat("Finished capturing. ", transactionDataProvider.GetPersistentValue(ticket.InvoiceNumber, "CaptureResponceString"));
      }
    }

    /// <summary>
    /// Handles the Click event of the Cancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Cancel_Click(object sender, EventArgs e)
    {
      Assert.IsNotNull(ticket, "Reservation Ticket is null");

      UrlString path = returnUrl;
      path.Add("ec_action", "cancel");
      paymentArgs.PaymentUrls.ReturnPageUrl = path.ToString();

      using (new SiteContextSwitcher(SiteContextFactory.GetSiteContext("website")))
      {
        paymentProvider.CancelPaymentReservation(paymentSystem, paymentArgs, ticket);

        ITransactionData transactionDataProvider = Ecommerce.Context.Entity.Resolve<ITransactionData>();
        this.Text.Text = string.Concat("Finished canceling. ", transactionDataProvider.GetPersistentValue(ticket.InvoiceNumber, "CaptureResponceString"));
      }
    }

    /// <summary>
    /// Inits the test data.
    /// </summary>
    private static void InitTestData()
    {
      paymentUrlResolver = new PaymentUrlResolver();
      paymentArgs = new PaymentArgs
      {
        PaymentUrls = paymentUrlResolver.Resolve()
      };
      paymentProvider = new AuthorizeNetReservablePaymentProvider();
      OnlinePaymentProviderBase provider = paymentProvider as OnlinePaymentProviderBase;

      Assert.IsNotNull(provider, "provider is null");

      ShoppingCart shoppingCart = new ShoppingCart
      {
        ShoppingCartLines = new List<ShoppingCartLine>
        {
         new ShoppingCartLine
         {
           Product = new Product { Code = "322", ShortDescription = "Nikon SLR D200", Title = "D200" },
           Totals = new Totals(new Dictionary<string, decimal>())
           {
             PriceExVat = 300.00m,
             PriceIncVat = 400.00m,
             VAT = 100m,
             MemberPrice = 300.00m,
           },
           Quantity = 5,
         }
        },
        PaymentSystem = new DomainModel.Payments.PaymentSystem
        {
          Code = "Authorize.NET",
          Title = "Authorize.NET",
          Username = "2KqZ28bf",
          Password = "4s9uQ39K5M55cPQV",
          PaymentUrl = "https://test.authorize.net/gateway/transact.dll",
          PaymentSettings =
            @"<setting id='x_relay_response'>TRUE</setting>
            <setting id='x_receipt_link_method'>POST</setting>
            <setting id='x_receipt_link_text'>Return back to the shop</setting>
            <setting id='x_version'>3.1</setting>
            <setting id='x_method'>CC</setting>
            <setting id='x_show_form'>PAYMENT_FORM</setting>
            <setting id='x_test_request'>FALSE</setting>
            <setting id='x_delim_data'>TRUE</setting>
            <setting id='x_delim_char'>|</setting>",
        },
        Currency = new Currency
        {
          Code = "USD",
          Name = "USD"
        },
        NotificationOption = new NotificationOption
        {
          Code = "Email"
        },
        NotificationOptionValue = "asa@sitecore.net",
        OrderNumber = new Random().Next(1000, 9000).ToString(),
        CustomerInfo = new CustomerInfo
        {
          AccountId = "{B43FF5E8-C9AD-41BC-A04A-72741E0AE36C}",
          NickName = "asa",
          Email = "asa@sitecore.net",
          ShippingAddress = new AddressInfo
          {
            Address = "Address",
            Address2 = "Address 2",
            Name = "John",
            Name2 = "Rambo",
            State = "JK",
            Zip = "4454",
            Country = new Country
            {
              Name = "UK",
            }
          },
          BillingAddress = new AddressInfo
          {
            Address = "Address",
            Address2 = "Address 2",
            Name = "John",
            Name2 = "Rambo",
            State = "JK",
            Zip = "4454",
            Country = new Country 
            {
              Name = "UK",
            }
          },
        }
      };

      paymentSystem = shoppingCart.PaymentSystem;
      paymentArgs.ShoppingCart = shoppingCart;
      paymentArgs.Description = shoppingCart.ShoppingCartLines[0].Product.Title;

      Ecommerce.Context.Entity.SetInstance(shoppingCart);
    }
  }
}
