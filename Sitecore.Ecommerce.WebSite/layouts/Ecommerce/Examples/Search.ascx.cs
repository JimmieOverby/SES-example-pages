// <copyright file="Search.ascx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.layouts.Ecommerce
{
  using System;
  using System.Web.UI;
  using Classes;
  using Globalization;

  public partial class Search : UserControl
  {
    private readonly string keywords = Translate.Text(Sitecore.Ecommerce.Examples.Texts.Keywords);

    private const string onblur = "javascript: if (document.getElementById('{0}').value=='{1}' || document.getElementById('{0}').value=='' ) {2} document.getElementById('{0}').value='{1}'; {3}";

    private const string onfocus = "javascript: if (document.getElementById('{0}').value=='{1}') {2}  document.getElementById('{0}').value=''; {3}";

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!Page.IsPostBack)
      {
        BindJavaScript();
      }
      if (SearchClicked())
      {
        SimpleSearch();
      }
    }

    private void SimpleSearch()
    {
      var searchKeywords = headerSearch.Value;
      if (searchKeywords != keywords)
      {
        var url = NicamHelper.RedirectUrl(Consts.SearchResultPage, "search", searchKeywords);
        // Search Result Page
        Response.Redirect(url);
      }
    }

    private static bool SearchClicked()
    {
      return NicamHelper.SafeRequest("SearchButton").Equals("SearchButton");
    }

    private void BindJavaScript()
    {
      headerSearch.Value = keywords;
      headerSearch.Attributes["onfocus"] = string.Format(onfocus, headerSearch.ClientID, keywords, "{", "}");
      headerSearch.Attributes["onblur"] = string.Format(onblur, headerSearch.ClientID, keywords, "{", "}");
    }
  }
}