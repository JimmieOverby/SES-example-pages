<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="Sitecore.Ecommerce.sitecore.admin.tests.IntegrationalPayment.UserInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Integrational Online Payment test. Page 2</title>
</head>
<body>
  <form id="userinfo" runat="server">
  <table>
    <tr>
      <td>
        <span>Name: </span>
      </td>
      <td>
        <asp:TextBox ID="txtName" runat="server" />
      </td>
    </tr>
    <tr>
      <td>
        <span>Address: </span>
      </td>
      <td>
        <asp:TextBox ID="txtAddress" runat="server" />
      </td>
    </tr>
  </table>
  <asp:Button ID="btnNext" Text="Next Page" runat="server" OnClick="OnNextButtonClick"/>
  </form>
</body>
</html>
