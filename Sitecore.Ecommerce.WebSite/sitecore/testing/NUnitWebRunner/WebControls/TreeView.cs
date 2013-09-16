// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeView.cs" company="Sitecore A/S">
//   Copyright (C) 2010 by Sitecore A/S
// </copyright>
// <summary>
//   Defines the tree view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.NUnit.WebRunner.WebControls
{
  using System.Collections.Generic;
  using System.Web;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  /// <summary>
  /// Defines the tree view.
  /// </summary>
  public class TreeView : WebControl
  {
    /// <summary>
    /// Gets or sets the nodes.
    /// </summary>
    /// <value>The nodes.</value>
    public IList<TreeNode> Nodes { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey
    {
      get { return HtmlTextWriterTag.Div; }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    protected override void OnInit(System.EventArgs e)
    {
      base.OnInit(e);

      this.Nodes = new List<TreeNode>();
    }

    /// <summary>
    /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
    protected override void RenderContents(HtmlTextWriter writer)
    {
      base.RenderContents(writer);

      this.RenderNodes(writer, this.Nodes);
    }

    /// <summary>
    /// Renders the nodes.
    /// </summary>
    /// <param name="writer">The writer.</param>
    /// <param name="nodes">The nodes.</param>
    protected virtual void RenderNodes(HtmlTextWriter writer, IEnumerable<TreeNode> nodes)
    {
      writer.RenderBeginTag(HtmlTextWriterTag.Ul);

      foreach (TreeNode childNode in nodes)
      {
        this.RenderNode(writer, childNode);
      }

      writer.RenderEndTag();
    }

    /// <summary>
    /// Renders the node.
    /// </summary>
    /// <param name="writer">The writer.</param>
    /// <param name="node">The node.</param>
    protected virtual void RenderNode(HtmlTextWriter writer, TreeNode node)
    {
      bool hasChildren = node.ChildNodes.Count > 0;

      if (node.NotInCategory)
      {
        writer.AddAttribute(HtmlTextWriterAttribute.Class, "notincategory");
      }

      writer.RenderBeginTag(HtmlTextWriterTag.Li);

      string nodeClass = hasChildren ? "node" : "node last";
      writer.AddAttribute(HtmlTextWriterAttribute.Class, nodeClass);
      writer.RenderBeginTag(HtmlTextWriterTag.Span);
      writer.Write("&nbsp;");
      writer.RenderEndTag();

      string stateClass = "state " + node.CssClass;
      writer.AddAttribute(HtmlTextWriterAttribute.Class, stateClass);
      writer.RenderBeginTag(HtmlTextWriterTag.Span);
      writer.Write("&nbsp;");
      writer.RenderEndTag();

      writer.AddAttribute(HtmlTextWriterAttribute.Id, node.Id);
      writer.AddAttribute(HtmlTextWriterAttribute.Class, "title");
      writer.RenderBeginTag(HtmlTextWriterTag.Span);
      writer.Write(HttpUtility.HtmlEncode(node.Text));
      writer.RenderEndTag();

      if (hasChildren)
      {
        this.RenderNodes(writer, node.ChildNodes);
      }

      writer.RenderEndTag();
    }
  }
}