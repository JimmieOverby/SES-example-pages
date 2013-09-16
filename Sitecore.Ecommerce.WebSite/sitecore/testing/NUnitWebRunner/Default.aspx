<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Sitecore.NUnit.WebRunner.Default" %>

<%@ Register TagPrefix="sc" Namespace="Sitecore.NUnit.WebRunner.WebControls" Assembly="Sitecore.NUnit.WebRunner" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Sitecore NUnit WebRunner</title>
  <link href="/sitecore/shell/themes/standard/default/WebFramework.css" rel="Stylesheet" />
  <link href="Styles/NUnitWebRunner.css" rel="stylesheet" type="text/css" />
  <script src="/sitecore/shell/Controls/lib/jQuery/jquery.js" type="text/javascript"></script>
  <script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
  <script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
  <script src="Scripts/NUnitWebRunner.js" type="text/javascript"></script>
</head>
<body>
  <form id="form1" runat="server" class="wf-container">
  <div class="wf-content">
    <h1 id="lblHeader">Sitecore NUnit WebRunner</h1>
    <div class="leftCol">
      <sc:TreeView ID="TreeView" runat="server" CssClass="treeview" />
    </div>
    <div class="rightCol">
      <div class="actions">
        <input id="run" type="button" value="Run" disabled="disabled" />
        <asp:Panel ID="ProgressBar" runat="server" />
      </div>
      <div class="messageBox">
        <ul id="MessageBox" runat="server">
        </ul>
      </div>
      <asp:Panel ID="StackTraceBox" runat="server" CssClass="stackTrace" />
    </div>
  </div>
  </form>
</body>
</html>
