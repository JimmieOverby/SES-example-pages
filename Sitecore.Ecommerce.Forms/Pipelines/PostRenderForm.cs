// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostRenderForm.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Represents PostRenderForm pipeline processor. Used to modify rendered form by HtmlAgilityPack.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.Forms.Pipelines
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;
  using System.Web;
  using System.Xml.Linq;
  using Form.Core.Pipelines.RenderForm;
  using Reflection;
  using Sitecore.Data.Items;
  using Sitecore.Exceptions;

  /// <summary>
  ///   Represents PostRenderForm pipeline processor. Used to modify rendered form by HtmlAgilityPack.
  /// </summary>
  public class PostRenderForm
  {
    /// <summary>
    /// Gets a value indicating whether this instance is postback.
    /// </summary>
    /// <value><c>true</c> if this instance is postback; otherwise, <c>false</c>.</value>
    private static bool IsPostback
    {
      get
      {
        if (HttpContext.Current != null)
        {
          return HttpContext.Current.Request.Form.Count > 0;
        }

        return false;
      }
    }

    /// <summary>
    /// Processes the specified args.
    /// </summary>
    /// <param name="args">The render form args.</param>
    public void Process(RenderFormArgs args)
    {
      // replaces legends with div tags
      var form = new HtmlFormModifier(args);
      form.ReplaceLegendWithDiv();
      form.RemoveEmptyTags("div", "scfSectionUsefulInfo");
      form.RemoveEmptyTags("div", "scfTitleBorder");
      form.RemoveEmptyTags("div", "scfIntroBorder");
      form.RemoveEmptyTags("span", "scfError");
      form.SorundContentWithUlLi("scfError");

      // TODO: remove this line after web forms fixed bug with unnecessary nbsp symbols
      form.RemoveNbsp();

      string saveActions = args.Item["Save Actions"];
      if (!string.IsNullOrEmpty(saveActions))
      {
        XDocument commands = XDocument.Parse(saveActions);
        List<string> actionIds = (from c in commands.Descendants()
                                  where c.Name.LocalName == "li"
                                        && (c.Attribute(XName.Get("id")) != null
                                        && c.Attribute(XName.Get("id")).Value != null)
                                  select c.Attribute(XName.Get("id")).Value).ToList();

        foreach (string actionId in actionIds)
        {
          Item actionItem = args.Item.Database.GetItem(actionId);
          string assembly = actionItem["assembly"];
          string className = actionItem["Class"];

          object obj = ReflectionUtil.CreateObject(assembly, className, new object[] { });
          if (obj == null)
          {
            throw new ConfigurationException("Could not load " + className + " from " + assembly);
          }

          MethodInfo method = ReflectionUtil.GetMethod(obj, "Load", new object[] { IsPostback, args });
          if (method != null)
          {
            ReflectionUtil.InvokeMethod(method, new object[] { IsPostback, args }, obj);
          }
        }
      }
    }
  }
}