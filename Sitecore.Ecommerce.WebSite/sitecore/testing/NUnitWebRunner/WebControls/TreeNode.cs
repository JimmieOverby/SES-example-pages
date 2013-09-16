// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeNode.cs" company="Sitecore A/S">
//   Copyright (C) 2010 by Sitecore A/S
// </copyright>
// <summary>
//   Defines the tree node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.NUnit.WebRunner.WebControls
{
  using System.Collections.Generic;

  /// <summary>
  /// Defines the tree node.
  /// </summary>
  public class TreeNode
  {
    /// <summary>
    /// The child nodes.
    /// </summary>
    private IList<TreeNode> childNodes;

    /// <summary>
    /// Initializes a new instance of the <see cref="TreeNode"/> class.
    /// </summary>
    public TreeNode()
      : this(string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TreeNode"/> class.
    /// </summary>
    /// <param name="text">The text.</param>
    public TreeNode(string text)
    {
      this.Text = text;
    }

    /// <summary>
    /// Gets or sets the node id.
    /// </summary>
    /// <value>The node id.</value>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the CSS class.
    /// </summary>
    /// <value>The CSS class.</value>
    public string CssClass { get; set; }

    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    /// <value>The text.</value>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [non in category].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [non in category]; otherwise, <c>false</c>.
    /// </value>
    public bool NotInCategory { get; set; }

    /// <summary>
    /// Gets the child nodes.
    /// </summary>
    /// <value>The child nodes.</value>
    public IList<TreeNode> ChildNodes
    {
      get { return this.childNodes ?? (this.childNodes = new List<TreeNode>()); }
    }
  }
}