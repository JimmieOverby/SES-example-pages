﻿<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>
<%@ Import Namespace="Sitecore.Ecommerce.Utils" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchResult.ascx.cs"
    Inherits="Sitecore.Ecommerce.layouts.Ecommerce.SearchResult" %>
<%@ Import Namespace="Sitecore.Globalization"%>
<%@ Import Namespace="Sitecore.Ecommerce.Examples" %>
<style>
table, td, th
{
  border: none;
}
</style>
<div id="pb_product_list_container">
    <div class="content">
        <div id="pb_header_shaddow">
            <h1>
                <sc:Text runat="server" ID="Text1" Field="Title"></sc:Text>
            </h1>
            <div class="clear">
            </div>
        </div>
        <ul class="ulArticleList">
            <li>
                <input name="bodySearch" id="bodySearch" type="text" runat="server" />
                <input id="SearchAgain" name="SearchAgain" type="hidden" />
                <a href="#" id="btn_search_again" class="button button_wider_red_on_white" onclick="javascript: document.getElementById('SearchAgain').value='SearchAgain';document.forms[0].submit();return false;">
                    <%= Translate.Text(Texts.SearchAgain)%></a>
                <div class="clear" id="ctl5">
                </div>
                <asp:Label runat="server" ID="searchResultText"></asp:Label>
            </li>
        </ul>
        <div class="clear">
        </div>
        <asp:TreeView ID="linksTree" runat="server"/>
        <div class="clear">
        </div>
    </div>
</div>
