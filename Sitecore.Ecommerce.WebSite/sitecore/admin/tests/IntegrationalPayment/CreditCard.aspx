<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreditCard.aspx.cs" Inherits="Sitecore.Ecommerce.sitecore.admin.tests.IntegrationalPayment.CreditCard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Integrational Online Payment test. Page 2</title>
</head>
<body>
  <form id="frmCreditCardInfo" runat="server">
  <asp:Label id="greetingLabel" runat="server" />
  <asp:Label id="certDataLabel" runat="server" />
  <table id="tblInfo" runat="server">
    <tr>
      <td>
        <span>Cards Holder Name: </span>
      </td>
      <td>
        <asp:TextBox ID="txtCardsHolderName" runat="server" />
      </td>
    </tr>
    <tr>
      <td>
        <span>Card Number: </span>
      </td>
      <td>
        <asp:TextBox ID="txtCardNumber" runat="server" />
      </td>
    </tr>
    <tr>
      <td>
        <span>Expiration Date: </span>
      </td>
      <td>
        <asp:TextBox ID="txtExpirationDate" runat="server" />
      </td>
    </tr>
    <tr>
      <td>
        <span>Security Code: </span>
      </td>
      <td>
        <asp:TextBox ID="txtSecurityCode" runat="server" />
      </td>
    </tr>
  </table>
  <asp:Button ID="btnPay" Text="Pay" runat="server" OnClick="OnPayButtonClick" />
  </form>
</body>
</html>
