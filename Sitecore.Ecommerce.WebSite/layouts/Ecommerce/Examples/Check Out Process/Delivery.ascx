<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Delivery.ascx.cs" Inherits="Sitecore.Ecommerce.layouts.Ecommerce.CheckOutProcess.Delivery" %>
<div id="deliveryContainer">
  <div>
    <asp:Label ID="lblShippingMethods" runat="server" />
    <asp:Label ID="lblShippingMethod" runat="server" />
    <asp:DropDownList ID="ddlShippingMethods" AutoPostBack="true" runat="server" />
    <asp:Label ID="lblShippingCost" Style="float: right;" runat="server" />
  </div>
  <div style="margin-top: 10px;" id="changeDestinationForm" runat="server">
    <asp:Label ID="lblShippingDestination" runat="server" />
    <a id="changeDestination" runat="server" href="#"></a>
    <div id="ShippingAdressForm" style="display: none">
      <div class="boxShaddow1">
        <dl>
          <dd>
            <table>
              <tr>
                <td colspan="2">
                  <h2>
                    <asp:Label ID="lblFormTitle" runat="server" /></h2>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <p>
                    Please fill the form</p>
                </td>
              </tr>
              <tr>
                <td>
                  <asp:Label ID="lblCountries" runat="server" />
                </td>
                <td>
                  <asp:DropDownList ID="ddlShippingCountries" runat="server" />
                </td>
              </tr>
              <tr>
                <td>
                  <asp:Label ID="lblStates" runat="server" />
                </td>
                <td>
                  <select id="ddlShippingStates" runat="server" />
                </td>
              </tr>
              <tr>
                <td>
                  <input id="confirmShippingForm" type="button" runat="server" value="OK" enableviewstate="false" />
                </td>
                <td>
                  <input id="cancelShippingForm" type="button" runat="server" value="Cancel" enableviewstate="false" />
                </td>
              </tr>
            </table>
          </dd>
          <dd class="bottom">
            <div />
          </dd>
        </dl>
      </div>
    </div>
  </div>
  <div id="ShippingAdressForm-overlay">
  </div>
</div>
