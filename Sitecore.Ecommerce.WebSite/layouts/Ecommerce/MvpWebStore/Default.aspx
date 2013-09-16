<%@ Page Language="c#" Inherits="System.Web.UI.Page" CodePage="65001" %>

<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>
<%@ OutputCache Location="None" VaryByParam="none" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>SES | MVP Web Store</title>
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  <meta name="CODE_LANGUAGE" content="C#" />
  <meta name="vs_defaultClientScript" content="JavaScript" />
  <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <link href="Content/bootstrap.min.css" rel="stylesheet" media="screen" />
  <link href="Content/bootstrap-responsive.css" rel="stylesheet" />
  <link href="Content/styles.css" rel="stylesheet" />
  <script src="http://code.jquery.com/jquery-latest.js"></script>
  <script src="/layouts/Ecommerce/MvpWebStore/Scripts/bootstrap.min.js"></script>
</head>
<body>
  <form id="form1" runat="server">
    <div class="navbar navbar-inverse navbar-fixed-top">
      <div class="navbar-inner">
        <div class="container">
          <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </a>
          <a class="brand" href="/">MVP Web Store</a>
          <div class="nav-collapse collapse">
            <ul class="nav">
              <li class="active"><a href="/">Categories</a></li>
              <li class="active"><a href="/orders?user=100500">Orders</a></li>
              <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">About <b class="caret"></b></a>
                <ul class="dropdown-menu">
                  <li>This is the sample MVP Web Store</li>
                </ul>
              </li>
            </ul>
          </div>
          <!--/.nav-collapse -->
        </div>
      </div>
    </div>

    <div class="container">
      <div class="row">
        <sc:Placeholder key="main" runat="server" />
      </div>
    </div>
    <!-- /container -->

  </form>
</body>
</html>
