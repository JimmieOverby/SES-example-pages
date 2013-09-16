// <copyright file="ActionHelper.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.Form.Action
{
  using System.Collections.Specialized;
  using Sitecore.Data.Items;
  using Sitecore.Form.Core.Client.Data.Submit;
  using Sitecore.Form.Core.Controls.Data;

  /// <summary>
  /// Action helper class
  /// </summary>
  public class ActionHelper
  {
    /// <summary>
    /// functions should determinate fields values by form busines logic
    /// </summary>
    /// <param name="fieldName">name of field</param>
    /// <param name="fieldValue">field value</param>
    /// <returns>returns name-value collection of processed field</returns>
    protected internal delegate NameValueCollection ProcessField(string fieldName, string fieldValue);

    /// <summary>
    /// Gets web form field name
    /// </summary>
    /// <param name="field">Form field</param>
    /// <returns>field name</returns>
    public static string GetFieldName(AdaptedControlResult field)
    {
      if (field != null)
      {
        Item fieldItem = Sitecore.Context.Database.SelectSingleItem(field.FieldID);
        if (fieldItem != null)
        {
          return fieldItem.Name;
        }
      }

      return string.Empty;
    }

    /// <summary> Fill form data to name value collection </summary>
    /// <param name="formData">form data to process</param>
    /// <param name="formFields">form fields</param>
    /// <param name="ps">fields processor method</param>
    protected internal static void FillFormData(NameValueCollection formData, AdaptedResultList formFields, ProcessField ps)
    {
      foreach (AdaptedControlResult field in formFields)
      {
        string fieldName = GetFieldName(field);

        if (!string.IsNullOrEmpty(fieldName))
        {
          if (ps != null)
          {            
            formData.Add(ps(fieldName, field.Value));
          }
          else
          {
            formData.Add(fieldName, field.Value);
          }
        }
      }
    }
  }
}