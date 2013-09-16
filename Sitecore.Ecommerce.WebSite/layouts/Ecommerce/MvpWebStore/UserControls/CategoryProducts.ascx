<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryProducts.ascx.cs"
  Inherits="Sitecore.Ecommerce.MvpWebStore.UserControls.CategoryProducts" %>
<asp:Repeater ID="CategoryProductsRepeater" runat="server">
  <ItemTemplate>
    <div class="span6">
      <h2>
        <a href="/products/viewproduct.aspx?p=<%# ((Sitecore.Ecommerce.DomainModel.Products.ProductBaseData)Container.DataItem).Code %>"><%# ((Sitecore.Ecommerce.DomainModel.Products.ProductBaseData)Container.DataItem).Code %></a>
      </h2>
      <p>Phasellus fermentum tortor sit amet neque hendrerit sollicitudin. In quis nulla lorem, sit amet bibendum nisl. Aenean venenatis adipiscing blandit. Vivamus eget justo libero. Maecenas volutpat laoreet gravida. Proin risus nulla, auctor ut varius ac, bibendum sed eros. Donec ut risus in sapien tempor cursus dapibus sed metus. Duis in facilisis ipsum. Nunc sit amet tellus eget nibh accumsan tristique sit amet sit amet arcu. Curabitur eu cursus sapien.</p>
    </div>
  </ItemTemplate>
</asp:Repeater>
