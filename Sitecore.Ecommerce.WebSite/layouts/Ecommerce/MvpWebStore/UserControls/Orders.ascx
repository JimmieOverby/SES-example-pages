<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Orders.ascx.cs" Inherits="Sitecore.Ecommerce.MvpWebStore.UserControls.Orders" %>
<asp:Repeater ID="OrdersRepeater" runat="server">
    <ItemTemplate>
        <div class="row ">
            <div class="span4 label">
                ID: <%# ((Sitecore.Ecommerce.OrderManagement.Orders.Order)Container.DataItem).OrderId %>;
                Shop: <%# ((Sitecore.Ecommerce.OrderManagement.Orders.Order)Container.DataItem).ShopContext %>
            </div>
            <div class="span4">
                <a class="btn" href="/orders/cancelorder.aspx?id=<%# ((Sitecore.Ecommerce.OrderManagement.Orders.Order)Container.DataItem).OrderId %>&user=<%# ((Sitecore.Ecommerce.OrderManagement.Orders.Order)Container.DataItem).BuyerCustomerParty.SupplierAssignedAccountID %>">Cancel »</a>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<div class="row">
  <asp:Button CssClass="btn btn-large btn-primary" runat="server" ID="Exporter" OnClick="OnExport" Text="Export" />
</div>
<div class="row label label-info">
  <asp:FileUpload ID="ImportOrdersDialog" runat="server"  />
  <asp:Button CssClass="btn btn-large btn-primary" runat="server" ID="Importer" OnClick="OnImport" Text="Import" />
</div>