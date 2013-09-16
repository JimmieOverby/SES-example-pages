<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.ascx.cs"
   Inherits="Sitecore.Ecommerce.layouts.Ecommerce.CheckOutProcess.CreateAccount" %>
<%@ Import Namespace="Sitecore.Globalization" %>
<%@ Import Namespace="Sitecore.Ecommerce.Examples" %>
<div id="createAccountPanel" class="createAccountContainer">
   <div class="colMargin8">
   </div>
   <div>
      <%# !this.Visibility ? GetPromtMessage() : GetConfiramtionMessage()%>
   </div>
   <div class="colMargin8">
   </div>
   <input type="button" id="btnCreateAccount" value='<%= Translate.Text(Texts.CreateAccount) %>'
      style='visibility: <%# !this.Visibility ? "visible":"hidden" %>' />
</div>
