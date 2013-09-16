<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestFixture.aspx.cs" Inherits="Sitecore.Ecommerce.Presentation.Tests.TestingSuite.TestFixture" %>
<%@ Register TagPrefix="sc" TagName="SampleControlTest" src="Tests/SampleControlTest.ascx" %>

<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8">
  <title>Test fixture</title>
  <link rel="stylesheet" href="css/qunit.css">
  <script src="js/qunit.js"></script>
  <script src="js/jquery.min.js"></script>
  <script src="js/jquery-ui.min.js"></script>
  <!-- Place needed Speak stuff here -->
</head>
<body>
  <div id="qunit"></div>
  <div id="qunit-fixture">
    <form id="form1" runat="server">
      <!-- Place actual tests here -->
      <sc:SampleControlTest runat="server" />
    </form>
  </div>
</body>
</html>
