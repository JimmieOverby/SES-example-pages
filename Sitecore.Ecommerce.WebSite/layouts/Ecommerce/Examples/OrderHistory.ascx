<%@ Import Namespace="Sitecore.Ecommerce.Examples" %>
<%@ Import Namespace="Sitecore.Ecommerce.Utils" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderHistory.ascx.cs"
  Inherits="Sitecore.Ecommerce.layouts.Ecommerce.OrderHistory" %>
<%@ Import Namespace="Sitecore.Globalization" %>
<%@ Import Namespace="Sitecore.Ecommerce.DomainModel.Configurations" %>
<%@ Import Namespace="Sitecore.Ecommerce.OrderManagement.Orders" %>
<asp:DataGrid ID="orderList" runat="server" AutoGenerateColumns="False" GridLines="None"
  ShowFooter="True" Width="100%">
  <Columns>
    <asp:TemplateColumn>
      <HeaderStyle CssClass="line-bottom" Font-Bold="True" HorizontalAlign="Center" />
      <ItemStyle CssClass="line-bottom" HorizontalAlign="Center" />
      <HeaderTemplate>
        <%= Translate.Text(Texts.OrderNumber)%>
      </HeaderTemplate>
      <ItemTemplate>
        <%# Eval("OrderId") %>
      </ItemTemplate>
    </asp:TemplateColumn>
    <asp:TemplateColumn>
      <HeaderStyle CssClass="line-bottom" Font-Bold="True" HorizontalAlign="Center" />
      <ItemStyle CssClass="line-bottom" HorizontalAlign="Center" />
      <HeaderTemplate>
        <%= Translate.Text(Texts.OrderDate)%>
      </HeaderTemplate>
      <ItemTemplate>
        <%# Eval("IssueDate", "{0:dd.MM.yyyy}") %>
      </ItemTemplate>
    </asp:TemplateColumn>
    <asp:TemplateColumn>
      <HeaderStyle CssClass="line-bottom" Font-Bold="True" HorizontalAlign="Center" />
      <ItemStyle CssClass="line-bottom" HorizontalAlign="Center" />
      <HeaderTemplate>
        <%= Translate.Text(Texts.Items)%>
      </HeaderTemplate>
      <ItemTemplate>
        <%# GetQuantity(Container.DataItem)%>
      </ItemTemplate>
    </asp:TemplateColumn>
    <asp:TemplateColumn>
      <HeaderStyle CssClass="line-bottom" Font-Bold="True" HorizontalAlign="Right" />
      <ItemStyle CssClass="line-bottom" HorizontalAlign="Right" />
      <HeaderTemplate>
        <%= Translate.Text(Texts.TotalPrice)+":"%>
      </HeaderTemplate>
      <ItemTemplate>
        <%# Eval("AnticipatedMonetaryTotal.TaxInclusiveAmount.Value", Sitecore.Ecommerce.Context.Entity.GetConfiguration<GeneralSettings>().PriceFormatString)%>
      </ItemTemplate>
    </asp:TemplateColumn>
    <asp:TemplateColumn>
      <HeaderStyle CssClass="line-bottom" Font-Bold="True" HorizontalAlign="Center" />
      <ItemStyle CssClass="line-bottom" HorizontalAlign="Right" />
      <HeaderTemplate>
      </HeaderTemplate>
      <ItemTemplate>
        <a href='<%= ItemUtil.GetNavigationLinkPath("View Details") %>?orderid=<%# Eval("OrderId") %>'>
          <%= Translate.Text(Texts.Details)%>
        </a>
      </ItemTemplate>
    </asp:TemplateColumn>
    <asp:TemplateColumn>
      <HeaderStyle CssClass="line-bottom" Font-Bold="True" HorizontalAlign="Center" />
      <ItemStyle CssClass="line-bottom" HorizontalAlign="Right" />
      <HeaderTemplate>
      </HeaderTemplate>
      <ItemTemplate>
        <asp:LinkButton ID="CancelOrderLink" runat="server" CommandArgument='<%# ((Order)Container.DataItem).OrderId%>' 
          OnClick="CancelOrderLink_Click" OnDataBinding="CancelOrderLink_DataBind" />
      </ItemTemplate>
    </asp:TemplateColumn>
  </Columns>
</asp:DataGrid>
