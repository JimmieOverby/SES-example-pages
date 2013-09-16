<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductCategories.ascx.cs"
  Inherits="Sitecore.Ecommerce.MvpWebStore.UserControls.ProductCategories" %>
<%@ Import Namespace="Sitecore.Ecommerce.DomainModel.Products" %>

<asp:Repeater ID="ProductCategotyList" runat="server">
  <ItemTemplate>
    <div class="span6">
      <h2>
        <a href="/products.aspx?cat=<%# ((ProductCategory)Container.DataItem).Code %>"><%# ((ProductCategory)Container.DataItem).Name %></a>
      </h2>
      <p>Curabitur dictum lobortis pellentesque. Vivamus et enim sem. Quisque pellentesque, sem in ornare venenatis, ipsum sapien vulputate risus, non feugiat diam diam a mauris. Donec at risus gravida arcu auctor ultrices. Aenean placerat bibendum cursus. Morbi bibendum scelerisque lectus non dignissim.</p>
    </div>
  </ItemTemplate>
</asp:Repeater>

