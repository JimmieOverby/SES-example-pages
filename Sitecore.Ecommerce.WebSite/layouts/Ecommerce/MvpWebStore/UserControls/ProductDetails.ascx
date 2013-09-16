<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetails.ascx.cs" Inherits="Sitecore.Ecommerce.MvpWebStore.UserControls.ProductDetails" %>

<div class="span10">
  <dl class="dl-horizontal">
    <dt>Name</dt>
    <dd>
      <asp:Label ID="Title" runat="server" /></dd>
    <dt>Description</dt>
    <dd>
      <asp:Label ID="Description" runat="server" /></dd>
    <dt>Price</dt>
    <dd>
      <asp:Label ID="Price" runat="server" /></dd>
    <dt>Stock</dt>
    <dd>
      <asp:Label ID="Stock" runat="server" /></dd>
  </dl>

  <asp:Button CssClass="btn btn-large btn-primary" runat="server" ID="Buyer" OnClick="OnBuy" Text="Buy" />
</div>
