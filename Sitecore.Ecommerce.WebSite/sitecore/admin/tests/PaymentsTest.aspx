<%@ Page Language="C#" Debug="true" %>

<%@ Import Namespace="Sitecore.Ecommerce.DomainModel.Orders" %>
<%@ Import Namespace="Sitecore.Ecommerce.DomainModel.Payments" %>
<%@ Import Namespace="Sitecore.Sites" %>
<%@ Import Namespace="Sitecore.Ecommerce.Payments" %>
<%@ Import Namespace="Sitecore.Configuration" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="Sitecore.Ecommerce.DomainModel.Carts" %>
<%@ Import Namespace="Sitecore.Ecommerce.Search" %>
<%@ Import Namespace="Sitecore.Ecommerce" %>
<%@ Import Namespace="Sitecore.Ecommerce.Orders.Statuses" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
  
  /// <summary>
  ///  Site context switcher
  /// </summary>
  private SiteContextSwitcher siteContextSwitcher = new SiteContextSwitcher(Factory.GetSite("example"));

  /// <summary>
  /// Payment Url resolver
  /// </summary>
  private static readonly PaymentUrlResolver paymentUrlResolver = new PaymentUrlResolver();

  /// <summary>
  /// Payment argumetns
  /// </summary>
  private PaymentArgs paymentArgs = new PaymentArgs { PaymentUrls = paymentUrlResolver.Resolve() };

  /// <summary>
  /// Order Manager
  /// </summary>
  private readonly IOrderManager<Order> orderManager = Sitecore.Ecommerce.Context.Entity.Resolve<IOrderManager<Order>>();

  /// <summary>
  /// Transaction provider
  /// </summary>
  private readonly ITransactionData transactionDataProvider = Sitecore.Ecommerce.Context.Entity.Resolve<ITransactionData>();

  /// <summary>
  ///   Called during disposing of the page
  /// </summary>
  public override void Dispose()
  {
    base.Dispose();
    if (this.siteContextSwitcher != null)
    {
      this.siteContextSwitcher.Dispose();
    }
  }

  /// <summary>
  /// Page load event handler
  /// </summary>
  /// <param name="sender">The sender.</param>
  /// <param name="e">The e.</param>
  protected void Page_Load(object sender, EventArgs e)
  {
    Query query = new Query();
    query.Add(new FieldQuery("OrderNumber", string.Empty, MatchVariant.NotEquals));
    var reservedOrders = this.orderManager
      .GetOrders(query)
      .Where(o => !string.IsNullOrEmpty(o.AuthorizationCode) && o.PaymentSystem != null && (o.Status.GetType() == typeof(Authorized) || decimal.Parse(o.Comment) < o.Totals.PriceIncVat))
      .Select(or => new
      {
        or.OrderDate,
        or.OrderNumber,
        or.TransactionNumber,
        Amount = or.Totals.PriceIncVat,
        or.AuthorizationCode,
        or.PaymentSystem.Code,
        Status = or.Status.GetType().Name
      })
      .OrderBy(ob => ob.OrderNumber)
      .ToArray();

    this.ReservationTicketsView.DataSource = reservedOrders;
    this.ReservationTicketsView.DataBind();
  }

  /// <summary>
  /// Row command event handler
  /// </summary>
  /// <param name="sender">The sender.</param>
  /// <param name="e">The event args.</param>
  protected void ReservationTicketsView_RowCommand(object sender, GridViewCommandEventArgs e)
  {
    int index = Convert.ToInt32(e.CommandArgument);
    GridViewRow selectedRow = this.ReservationTicketsView.Rows[index];

    Order order = this.orderManager.GetOrder(selectedRow.Cells[1].Text);

    ReservationTicket reservationTicket = new ReservationTicket(order);

    this.paymentArgs.ShoppingCart = new ShoppingCart
    {
      Currency = order.Currency
    };

    decimal previouslyCapturedAmount;
    decimal.TryParse(order.Comment, out previouslyCapturedAmount);
    bool partialCapturing = true;
    decimal amount = 0;

    PaymentProvider paymentProvider = Sitecore.Ecommerce.Context.Entity.Resolve<PaymentProvider>(order.PaymentSystem.Code);
    IReservable reservableProvider = (IReservable)paymentProvider;

    switch (e.CommandName)
    {
      case "Capture":
        decimal.TryParse(this.AmountTextBox.Text, out amount);
        // Check, if this attemption may fully capture the reserved sum
        if((amount < reservationTicket.Amount && (amount + previouslyCapturedAmount) == reservationTicket.Amount) || (amount == reservationTicket.Amount && previouslyCapturedAmount == 0))
        {
          partialCapturing = false;
        }
        // Incorrect amounts will be asserted within the payment providers
        reservableProvider.Capture(order.PaymentSystem, this.paymentArgs, reservationTicket, amount);
        break;
      case "CancelReservation":
        reservableProvider.CancelReservation(order.PaymentSystem, this.paymentArgs, reservationTicket);
        break;
      default:
        break;
    }

    string result = selectedRow.Cells[6].Text = this.transactionDataProvider.GetPersistentValue(order.OrderNumber).ToString();
    switch (result)
    {
      case "Captured":
        if(partialCapturing)
        {
          selectedRow.Cells[6].Text = "CapturedPartially";
        }
        else
        {
          order.Status = Sitecore.Ecommerce.Context.Entity.Resolve<OrderStatus>("Captured");
          selectedRow.Cells[7].Enabled = false;
        }
        previouslyCapturedAmount = previouslyCapturedAmount + amount;
        order.Comment = previouslyCapturedAmount.ToString();
        break;
      case "Canceled":
        order.Status = Sitecore.Ecommerce.Context.Entity.Resolve<OrderStatus>("Canceled");
        selectedRow.Cells[8].Enabled = false;
        break;
    }

    orderManager.SaveOrder(order);
  }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Reservation tickets management page</title>
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
        <h3>Reservation Tickets</h3>
        <asp:GridView ID="ReservationTicketsView" runat="server" EnableModelValidation="True"
          AutoGenerateColumns="False" OnRowCommand="ReservationTicketsView_RowCommand">
          <Columns>
            <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
            <asp:BoundField DataField="OrderNumber" HeaderText="Order Number" />
            <asp:BoundField DataField="TransactionNumber" HeaderText="Transaction Number" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />
            <asp:BoundField DataField="AuthorizationCode" HeaderText="Authorization Code" />
            <asp:BoundField DataField="Code" HeaderText="PaymentSystem" />
            <asp:BoundField HeaderText="Status" DataField="Status" />
            <asp:ButtonField CommandName="Capture" HeaderText="Capture Payment" Text="Capture" />
            <asp:ButtonField CommandName="CancelReservation" HeaderText="Cancel Payment Reservation"
              Text="Cancel Reservation" />
          </Columns>
        </asp:GridView>
        <div>
        Input the amount: <asp:TextBox ID="AmountTextBox" Text="0" runat="server" />
        </div>
      </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
      <ProgressTemplate>
        Loading...
      </ProgressTemplate>
    </asp:UpdateProgress>
  </div>
  </form>
</body>
</html>
