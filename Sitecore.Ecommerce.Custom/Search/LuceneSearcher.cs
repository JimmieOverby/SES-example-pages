// <copyright file="LuceneSearcher.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>

namespace Sitecore.Ecommerce.Search
{
  using System.Collections.Generic;
  using Diagnostics;
  using Lucene.Net.Search;
  using Sitecore.Search;

  /// <summary>
  /// Lucene searcher
  /// </summary>
  public class LuceneSearcher
  {
    /// <summary>
    /// Lucene query for searching
    /// </summary>
    private Lucene.Net.Search.Query query;

    /// <summary>
    /// Name of index for search
    /// </summary>
    private string indexName;

    /// <summary>
    /// Initializes a new instance of the <see cref="LuceneSearcher"/> class.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <param name="indexName">Name of the index.</param>
    public LuceneSearcher(Lucene.Net.Search.Query query, string indexName)
    {
      Assert.ArgumentNotNull(query, "query");
      Assert.ArgumentNotNullOrEmpty(indexName, "indexName");

      this.query = query;
      this.indexName = indexName;
    }

    /// <summary>
    /// Searches this instance.
    /// </summary>
    /// <returns>Returns the search results</returns>
    public virtual IEnumerable<SearchResult> Search()
    {
      using (IndexSearchContext context = SearchManager.GetIndex(this.indexName).CreateSearchContext())
      {
        SearchResultController controller = new SearchResultController();        
        return controller.GetSearchResult(context.Search(new PreparedQuery(this.query))); 
      }
    }
  }
}