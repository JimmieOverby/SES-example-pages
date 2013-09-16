// <copyright file="SitecoreSimpleFormAscx.ascx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.Form.Web.UI.Controls
{
  using System;
  using System.Web.UI;
  using Configuration;
  using Diagnostics;
  using Sitecore.Form.Core.Pipelines.RenderForm;
  using Sitecore.Pipelines;
  using Sitecore.Web;

  /// <summary>
  /// Overrides WFM SitecoreSimpleFormAscx control in order to register JavaScript in the right way and to add missing pipelines for Web Forms for Marketers v 2.2.
  /// </summary>
  public partial class SitecoreSimpleFormAscx : Sitecore.Form.Web.UI.Controls.SitecoreSimpleFormAscx
  {
    /// <summary>
    /// Stores value indicating whether Web Forms for Marketers are at version >=2.2 where the postFormRender pipeline is missing
    /// </summary>
    private static Nullable<bool> isWffm22OrLater = null;
    
    /// <summary>
    /// Renders the control.
    /// </summary>
    /// <param name="writer">The writer.</param>
    public override void RenderControl(HtmlTextWriter writer)
    {
      HtmlTextWriter innerWriter = this.IsWffm22OrLater() ? new HtmlTextWriter(new System.IO.StringWriter()) : writer;
      innerWriter.Write("<div class='scfForm' id=\"" + ID + "\"");
      Attributes.Render(innerWriter);
      innerWriter.Write(">");

      this.RenderControl(innerWriter, this.Adapter);

      innerWriter.Write("</div>");

      if (this.IsWffm22OrLater())
      {
        RenderFormArgs args = new RenderFormArgs(this.FormItem.InnerItem);
        args.Parameters = WebUtil.ParseQueryString(this.Parameters);
        args.DisableWebEdit = this.DisableWebEditing;
        args.Result.FirstPart = args.Result.FirstPart + innerWriter.InnerWriter;

        using (new LongRunningOperationWatcher(Settings.Profiling.RenderFieldThreshold, "postRenderForm pipeline[id={0}]", new[] { this.FormID.ToString() }))
        {
          CorePipeline.Run("postRenderForm", args);
        }

        writer.Write(args.Result);
      }
    }

    /// <summary>
    /// Determines whether Web Forms for Marketers are at version >=2.2 with missing postFormRender pipeline
    /// </summary>
    /// <returns>true if Web Forms for Marketers are at version >=2.2, otherwise false</returns>
    private bool IsWffm22OrLater()
    {
      if (!isWffm22OrLater.HasValue)
      {
        foreach (System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
          if ((assembly.GetName().Name == "Sitecore.Forms.Core") && (System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location).FileMajorPart >= 2) && (System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location).FileMinorPart >= 2))
          {
            isWffm22OrLater = true;
            break;
          }
        }

        isWffm22OrLater = isWffm22OrLater.GetValueOrDefault();
      }

      return isWffm22OrLater.Value;
    }
  }
}