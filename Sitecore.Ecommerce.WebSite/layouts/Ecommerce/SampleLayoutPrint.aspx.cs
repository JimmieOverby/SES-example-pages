// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleLayoutPrint.aspx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the SampleLayoutPrint type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.Examples
{
  using System;
  using System.Web.UI;

  /// <summary>
  /// Defines the SampleLayoutPrint type.
  /// </summary>
  public partial class SampleLayoutPrint : Page
  {
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Sitecore.Context.Item != null)
      {
        this.scID.Attributes["content"] = Sitecore.Context.Item.ID.ToString();
      }

      if (Sitecore.Context.Item != null &&
          Sitecore.Context.Item.Template != null &&
          Sitecore.Context.Item.Template.Name.Equals("Home"))
      {
        this.pageContainer.Attributes.Add("class", "home_page");
      }
    }
  }
}