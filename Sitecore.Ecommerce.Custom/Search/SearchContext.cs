// <copyright file="SearchContext.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.Search
{  
  using Lucene.Net.Index;
  using Lucene.Net.Search;
  using Sitecore.Search;

  /// <summary>
  /// Search context
  /// </summary>
  public class SearchContext : ISearchContext
  {
    /// <summary>
    /// Decorates the specified query adding context information.
    /// </summary>
    /// <param name="query">The source query.</param>
    /// <returns>The decorated query.</returns>
    public Lucene.Net.Search.Query Decorate(Lucene.Net.Search.Query query)
    {
      BooleanQuery result = new BooleanQuery(true);
      result.Add(query, BooleanClause.Occur.MUST);
      this.AddDecorations(result);
      return result;
    }

    /// <summary>
    /// Adds the decorations.
    /// </summary>
    /// <param name="query">The query.</param>
    protected virtual void AddDecorations(BooleanQuery query)
    {
      query.Add(new TermQuery(new Term(Sitecore.Search.BuiltinFields.Language, Sitecore.Context.Language.Name)), BooleanClause.Occur.MUST); 
    }
  }
}