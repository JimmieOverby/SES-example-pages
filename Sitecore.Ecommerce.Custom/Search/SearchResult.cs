// <copyright file="SearchResult.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.Search
{
  /// <summary>
  /// Simple search result 
  /// </summary>
  public class SearchResult
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SearchResult"/> class.
    /// </summary>
    public SearchResult()
    {
      this.ParentItem = new SearchResultElement();
      this.ResultItem = new SearchResultElement();
    }
    
    /// <summary>
    /// Gets or sets the parent item.
    /// </summary>
    /// <value>The parent item.</value>
    public SearchResultElement ParentItem { get; set; }

    /// <summary>
    /// Gets or sets the result item.
    /// </summary>
    /// <value>The result item.</value>
    public SearchResultElement ResultItem { get; set; }
  }
}