<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Sitecore.Ecommerce.layouts.Ecommerce.Demo.ThreeColumnContent" Codebehind="ThreeColumnContent.ascx.cs" %>
<div id="page_body" class="clearfix">
    <div class="pb_lc">
        <%----------------------------------------
        --               left side menu
        ----------------------------------------%>
        <sc:XslFile ID="sectionMenu" Path="/xsl/Ecommerce/Examples/Section Menu.xslt" runat="server" />
        <sc:Placeholder runat="server" Key="phleft" ID="phLeft"></sc:Placeholder>
        <div class="clear">
        </div>
    </div>
    <%----------------------------------------
        --               content
        ----------------------------------------%>
    <div class="pb_mc">                
        <sc:Placeholder runat="server" Key="phcenter" ID="phCenter"></sc:Placeholder>        
    </div>
    <%----------------------------------------
        --               right side content
        ----------------------------------------%>
    <div id="pb_menu_secondary" class="pb_rc">        
        <%----------------------------------------
            --               right modules
            ----------------------------------------%>
        <sc:Placeholder runat="server" Key="phright" ID="phRight"></sc:Placeholder>
    </div>
    <div class="clear">
    </div>
</div>
