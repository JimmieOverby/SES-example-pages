<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Sitecore.Ecommerce.layouts.Ecommerce.CheckOutProcess.ThreeColumnContent" Codebehind="ThreeColumnContent.ascx.cs" %>
<div id="page_body" class="clearfix">
    <div class="pb_lc">
        <%----------------------------------------
        --               left side menu
        ----------------------------------------%>
        <sc:Placeholder runat="server" Key="phleft" ID="phLeft"></sc:Placeholder>
        <div class="clear">
        </div>
    </div>
    <%----------------------------------------
        --               content
        ----------------------------------------%>
    <div class="checkout_mc">               
        <sc:Placeholder runat="server" Key="phcenter" ID="phCenter"></sc:Placeholder>        
    </div>
    <%----------------------------------------
        --               right side content
        ----------------------------------------%>
    <div id="pb_menu_secondary" class="pb_rc">        
        <sc:Placeholder runat="server" Key="phright" ID="phRight"></sc:Placeholder>
    </div>
    <div class="clear">
    </div>
</div>
