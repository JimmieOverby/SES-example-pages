// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoField.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved. 
// </copyright>
// <summary>
//   The info field control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.Web.UI.Controls
{
  using System;
  using System.Web.UI;
  using Sitecore.Form.Core.Attributes;
  using Sitecore.Form.Core.Controls.Data;
  using Sitecore.Form.Core.Visual;
  using Sitecore.Form.Web.UI.Controls;

  /// <summary>
  /// The info field control.
  /// </summary>
  public class InfoField : HelpControl
  {
    /// <summary>
    /// Gets or sets the information.
    /// </summary>
    /// <value>The information.</value>
    [VisualFieldType(typeof(TextAreaField))]
    [VisualCategory("Appearance")]
    [VisualProperty("HTML:")]
    [Localize]
    public new string Information { get; set; }

    /// <summary>
    /// Gets the control result.
    /// </summary>
    /// <value>The control result.</value>
    public override ControlResult Result
    {
      get { return new ControlResult(this.ControlName, string.Empty, null); }
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey
    {
      get { return HtmlTextWriterTag.Div; }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      this.Attributes["class"] = "scfSectionUsefulInfo";
      var literal = new LiteralControl(this.Information ?? string.Empty);
      this.Controls.Add(literal);
    }
  }
}