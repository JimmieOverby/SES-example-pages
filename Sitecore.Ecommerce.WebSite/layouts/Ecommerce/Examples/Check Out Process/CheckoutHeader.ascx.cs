// <copyright file="CheckoutHeader.ascx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.layouts.Ecommerce.CheckOutProcess
{
  #region

  using System;
  using System.Web.UI;

  #endregion

  /// <summary>
  /// The checkout header.
  /// </summary>
  public partial class CheckoutHeader : UserControl
  {
    #region Protected methods

    /// <summary>
    /// The page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The event argument.
    /// </param>
    protected void Page_Load(object sender, EventArgs e)
    {
      // TODO: this hardcode should be replaced to getting images from checkout process field

      if (Sitecore.Context.Item.Name == "Customer Details")
      {
        this.headerImage.ImageUrl = "/images/ecommerce/checkout_header_first.gif";
      }
      else
      {
        this.headerImage.ImageUrl = "/images/ecommerce/checkout_header_last.gif";
      }
    }

    #endregion
  }
}