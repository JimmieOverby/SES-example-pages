<%@ Import Namespace="Sitecore.Ecommerce.Utils" %>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Sitecore.Ecommerce.layouts.Ecommerce.Search" Codebehind="Search.ascx.cs" %>

<script language="JavaScript" type="text/javascript">
    function DoClick(nameButton) {
        document.getElementById(nameButton).value = nameButton;
        document.forms[0].submit();
        return false;
    }
</script>

<div id="ph_search">
    <table id="tbl_search" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td id="td_search_frase">
                <input runat="server" id="headerSearch" name="headerSearch" type="text" class="searchFrase" />
            </td>
            <td id="td_search_btn">
                <input type="submit" value="Search" class="searchBtn" onclick="javascript: DoClick('SearchButton')" />
            </td>
        </tr>
    </table>
</div>
<input id="SearchButton" name="SearchButton" type="hidden" />