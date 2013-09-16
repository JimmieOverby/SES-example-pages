<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckOutPaymentErrorPage.ascx.cs"
  Inherits="Sitecore.Ecommerce.layouts.Ecommerce.CheckOutProcess.CheckOutPaymentErrorPage" %>
<div id="warning" class="errorSummary">
  <div class="content" id="paymentError" runat="server">
    <div class="scfValidationSummary">
      <ul>
        <li>
          <sc:Text ID="sctErrorText" Field="Error Message" runat="server" />
          <asp:Label ID="lblErrorText" Visible="false" runat="server" />
        </li>
      </ul>
    </div>
    <div class="clear">
    </div>
  </div>
</div>
