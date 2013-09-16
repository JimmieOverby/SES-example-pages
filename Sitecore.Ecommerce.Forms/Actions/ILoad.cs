// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILoad.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Describes ILoad interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.Forms.Actions
{
  using Sitecore.Form.Core.Pipelines.RenderForm;

  /// <summary>
  /// ILoad interface.
  /// </summary>
  public interface ILoad
  {
    /// <summary>
    /// The form load event handler.
    /// </summary>
    /// <param name="isPostback">Gets a value that indicates whether the form is being rendered for the first time or is being loaded in response to a postback.</param>
    /// <param name="args">The render form arguments.</param>
    void Load(bool isPostback, RenderFormArgs args);
  }
}