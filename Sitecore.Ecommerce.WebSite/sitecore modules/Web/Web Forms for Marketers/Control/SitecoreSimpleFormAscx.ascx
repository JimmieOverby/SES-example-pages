<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SitecoreSimpleFormAscx.ascx.cs" Inherits="Sitecore.Ecommerce.Form.Web.UI.Controls.SitecoreSimpleFormAscx" %>
<%@ Register Namespace="Sitecore.Form.Web.UI.Controls" Assembly="Sitecore.Forms.Core" TagPrefix="wfm" %>
<wfm:FormTitle ID="title" runat="server"/>
<wfm:FormIntroduction ID="intro" runat="server"/>
<asp:ValidationSummary ID="summary" runat="server" ValidationGroup="submit" CssClass="scfValidationSummary"/>
<wfm:SubmitSummary ID="submitSummary" runat="server" CssClass="scfSubmitSummary"/>
<asp:Panel ID="fieldContainer" runat="server" />
<wfm:FormFooter ID="footer" runat="server"/>
<wfm:FormSubmit ID="submit" runat="server" Class="scfSubmitButtonBorder"/>
