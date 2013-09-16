<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Microsoft.Practices.Unity" %>
<%@ Import Namespace="Sitecore.Ecommerce.DomainModel.Products" %>
<%@ Import Namespace="Sitecore.Sites" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <script runat="server">
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
      if (IsPostBack)
      {
        return;
      }

      IProductRepository repository = Sitecore.Ecommerce.Context.Entity.Resolve<IProductRepository>();
      string code = this.Request.QueryString["p"];
      if (string.IsNullOrEmpty(code))
      {
        return;
      }

      SiteContext example = SiteContextFactory.GetSiteContext("example");
      using(new SiteContextSwitcher(example))
      {
        ProductBaseData product = repository.Get<ProductBaseData>(code);
        if (product == null)
        {
          return;
        }

        this.ProductSpecificationsGrid.DataSource = product.Specifications;
        this.ProductSpecificationsGrid.DataBind();
      }
    }
  </script>
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <asp:GridView ID="ProductSpecificationsGrid" runat="server" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="Key" HeaderText="Specification Field" />
        <asp:BoundField DataField="Value" HeaderText="Value" />
      </Columns>
    </asp:GridView>
  </div>
  </form>
</body>
</html>
