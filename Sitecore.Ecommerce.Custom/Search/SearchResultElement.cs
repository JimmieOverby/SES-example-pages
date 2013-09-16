// <copyright file="SearchResultElement.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.Search
{
  /// <summary>
  /// Search result elemnt
  /// </summary>
  public class SearchResultElement
  {
    /// <summary>
    /// Gets or sets the result item URI.
    /// </summary>
    /// <value>The result item URI.</value>
    public string ItemUri { get; set; }

    /// <summary>
    /// Gets or sets the result item link.
    /// </summary>
    /// <value>The result item link.</value>
    public string ItemLink { get; set; }

    /// <summary>
    /// Gets or sets the result item title.
    /// </summary>
    /// <value>The result item title.</value>
    public string ItemTitle { get; set; }
  }
}