<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutorizeNetReservationPaymentTest.aspx.cs"
  Inherits="Sitecore.Ecommerce.sitecore.admin.tests.AutorizeNetReservationPaymentTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Autorize.NET reservation payment test</title>
</head>
<body>
  <form id="AutorizeNetReservationPaymentTest" runat="server">
  <div>
    <asp:Button ID="ReserveMoney" runat="server" Text="Reserve Payment" OnClick="ReserveMoney_Click" />
    <asp:Button ID="CaptureMoney" runat="server" Text="Capture Payment" OnClick="CaptureMoney_Click" />
    <asp:Button ID="CancelTransaction" runat="server" Text="Cancel Payment" OnClick="Cancel_Click" />
  </div>
  <div>
  <asp:TextBox runat="server" ID="Text" Width="100%" />
  </div>
  </form>
</body>
</html>
