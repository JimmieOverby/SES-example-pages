<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckOutConfirmationMail.ascx.cs"
    Inherits="Sitecore.Ecommerce.layouts.Ecommerce.CheckOutProcess.CheckOutConfirmationMail" %>
<%@ Register src="ShoppingCartAndOrderView.ascx" tagname="ShoppingCartAndOrderView" tagprefix="uc" %>


<div class="content">
    <div class="colMargin8NoTopMargin">
      <p class="teaser"><asp:Literal ID="litStatusMessage" runat="server" /></p>
      </div>
</div>

<uc:ShoppingCartAndOrderView ID="ShoppingCartAndOrderView" CurrentOrderDisplayMode="OrderConfirmation" runat="server" />
