﻿<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Sitecore.Install.Framework" %>
<%@ Import Namespace="Sitecore.Ecommerce.Update" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Sitecore E-Commerce FE</title>
  <script language="C#" runat="server">
  /// <summary>
  ///   Runs the conversion
  /// </summary>
  /// <param name="sender">The sender.</param>
  /// <param name="e">The event args</param>
  protected void ConvertRun_Click(object sender, EventArgs e)
  {
    string userCulture = this.cultureContainer.Text;

    if (string.IsNullOrEmpty(userCulture))
    {
      return;
    }

    UpdatePostStep postStep = new UpdatePostStep { PriceCulture = userCulture };
    postStep.Run(new DefaultOutput(), new NameValueCollection());
    
    Response.Write(postStep.Result.ToString());
  }
  </script>
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <table id="mainTable">
      <tr>
        <td>
          <h5>
            Convert from:</h5>
          <asp:TextBox ID="cultureContainer" runat="server" Text="da-DK" />
        </td>
      </tr>
      <tr>
        <td>
          <asp:Button ID="ConvertRun" runat="server" Text="Convert" OnClick="ConvertRun_Click" />
        </td>
      </tr>
    </table>
  </div>
  </form>
</body>
</html>
