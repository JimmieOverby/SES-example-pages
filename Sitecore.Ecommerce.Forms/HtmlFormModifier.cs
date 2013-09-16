// <copyright file="HtmlFormModifier.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
namespace Sitecore.Ecommerce.Forms
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Xml.Linq;
  using HtmlAgilityPack;
  using Sitecore.Data.Items;
  using Sitecore.Form.Core.Pipelines.RenderForm;

  /// <summary>
  /// Html form modifier.
  /// </summary>
  public class HtmlFormModifier
  {
    /// <summary>
    /// Render form argumets.
    /// </summary>
    private readonly RenderFormArgs args;

    /// <summary>
    /// Xml document first part.
    /// </summary>
    private readonly XDocument firstPart;

    /// <summary>
    /// </summary>
    private readonly Item formItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="HtmlFormModifier"/> class.
    /// </summary>
    /// <param name="args">The render form arguments.</param>
    public HtmlFormModifier(RenderFormArgs args)
    {
      this.formItem = args.Item;
      this.args = args;

      var firstPartDoc = new HtmlDocument();
      firstPartDoc.LoadHtml(args.Result.FirstPart);
      this.firstPart = ToXDocument(firstPartDoc);
    }

    /// <summary>
    /// Gets the result first part.
    /// </summary>
    /// <returns>The first result part.</returns>
    private string GetResultFirstPart()
    {
      return this.firstPart.ToString();
    }

    /// <summary>
    /// Toes the X document.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <returns>Teh X document.</returns>
    private static XDocument ToXDocument(HtmlDocument document)
    {
      using (var sw = new StringWriter())
      {
        document.OptionOutputAsXml = true;
        document.Save(sw);
        return XDocument.Parse(sw.GetStringBuilder().ToString());
      }
    }

    /// <summary>
    /// Sets the text area value.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public bool SetTextAreaValue(string fieldName, string value)
    {
      XElement e = this.GetInputElement(fieldName);
      if (e != null && value != null)
      {
        e.Value = value;
        this.args.Result.FirstPart = this.GetResultFirstPart();
        return true;
      }

      return false;
    }

    /// <summary>
    /// Sets the input value.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public bool SetInputValue(string fieldName, string value)
    {
      XElement input = this.GetInputElement(fieldName);
      if (input != null)
      {
        input.SetAttributeValue("value", value);
        this.args.Result.FirstPart = this.GetResultFirstPart();
        return true;
      }

      return false;
    }

    /// <summary>
    /// Sets the checkbox selected.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="value">if set to <c>true</c> [value].</param>
    /// <returns></returns>
    public bool SetCheckboxSelected(string fieldName, bool value)
    {
      XElement input = this.GetInputElement(fieldName);
      if (input != null)
      {
        SetChecked(input, value);
        this.args.Result.FirstPart = this.GetResultFirstPart();
        return true;
      }

      return false;
    }

    /// <summary>
    /// Sets the selected drop list value.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public bool SetSelectedDropListValue(string fieldName, string value)
    {
      XElement selectList = this.GetInputElement(fieldName);
      if (selectList != null)
      {
        SetSelectedDropListValue(selectList, value);
        return true;
      }

      return false;
    }

    /// <summary>
    /// Sets the selected drop list value.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="value">The value.</param>
    private void SetSelectedDropListValue(XElement element, string value)
    {
      foreach (XElement option in element.Descendants())
      {
        string optValue = GetValueAttribute(option);
        if (optValue != null && optValue == value)
        {
          SetSelected(option, true);
        }
        else
        {
          SetSelected(option, false);
        }
      }

      this.args.Result.FirstPart = this.GetResultFirstPart();
    }

    /// <summary>
    /// Gets the attribute.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns></returns>
    private static string GetValueAttribute(XElement element)
    {
      string attrName = "value";
      if (element.Attribute(attrName) != null)
      {
        return element.Attribute(attrName).Value;
      }

      return null;
    }

    /// <summary>
    /// Sets the selected list box values.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="values">The values.</param>
    /// <returns></returns>
    public bool SetSelectedListBoxValues(string fieldName, params string[] values)
    {
      XElement selectList = this.GetInputElement(fieldName);
      if (selectList != null)
      {
        foreach (XElement option in selectList.Descendants())
        {
          string optValue = GetValueAttribute(option);
          if (optValue != null && values.Contains(optValue))
          {
            SetSelected(option, true);
          }
          else
          {
            SetSelected(option, false);
          }
        }

        this.args.Result.FirstPart = this.GetResultFirstPart();
        return true;
      }

      return false;
    }

    /// <summary>
    /// Sets the selected radio value.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public bool SetSelectedRadioValue(string fieldName, string value)
    {
      XElement table = this.GetInputElement(fieldName);
      if (table != null)
      {
        IEnumerable<XElement> radios = from t in table.Descendants()
                                       where t.Attribute("type") != null
                                             && t.Attribute("type").Value == "radio"
                                       select t;

        foreach (XElement radio in radios)
        {
          string val = GetValueAttribute(radio);
          if (val != null && val == value)
          {
            SetChecked(radio, true);
          }
          else
          {
            SetChecked(radio, false);
          }
        }

        this.args.Result.FirstPart = this.GetResultFirstPart();
        return true;
      }

      return false;
    }

    /// <summary>
    /// Sets the selected date.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    internal bool SetSelectedDate(string fieldName, DateTime value)
    {
      Item field = this.formItem.Axes.GetDescendant(fieldName);
      string identifier = field.ID.ToString().Replace("{", string.Empty).Replace("}", string.Empty).Replace("-", string.Empty);

      XElement div = this.GetInputElement(fieldName);
      if (div != null)
      {
        XElement dayElem = this.GetElementByLasIdPart(div, identifier + "_day");
        if (dayElem != null)
        {
          SetSelectedDropListValue(dayElem, value.Day.ToString());
        }

        XElement monthElem = this.GetElementByLasIdPart(div, identifier + "_month");
        if (monthElem != null)
        {
          SetSelectedDropListValue(monthElem, value.Month.ToString());
        }

        XElement yearElem = this.GetElementByLasIdPart(div, identifier + "_year");
        if (yearElem != null)
        {
          SetSelectedDropListValue(yearElem, value.Year.ToString());
        }

        this.args.Result.FirstPart = this.GetResultFirstPart();
        return true;
      }

      return false;
    }

    /// <summary>
    /// Gets the element by las id part.
    /// </summary>
    /// <param name="root">The root.</param>
    /// <param name="endsWith">The ends with.</param>
    /// <returns></returns>
    private XElement GetElementByLasIdPart(XElement root, string endsWith)
    {
      return (from el in root.Descendants()
              where el.Attribute("id") != null &&
                    el.Attribute("id").Value != null &&
                    el.Attribute("id").Value.EndsWith(endsWith)
              select el).FirstOrDefault();
    }

    /// <summary>
    /// Sets the checked.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="value">if set to <c>true</c> [value].</param>
    private static void SetChecked(XElement input, bool value)
    {
      if (value)
      {
        input.SetAttributeValue("checked", "checked");
      }
      else
      {
        if (input.Attribute("checked") != null)
        {
          input.Attribute("checked").Remove();
        }
      }
    }

    /// <summary>
    /// Sets the selected.
    /// </summary>
    /// <param name="option">The option.</param>
    /// <param name="value">if set to <c>true</c> [value].</param>
    private static void SetSelected(XElement option, bool value)
    {
      if (value)
      {
        option.SetAttributeValue("selected", "selected");
      }
      else
      {
        if (option.Attribute("selected") != null)
        {
          option.Attribute("selected").Remove();
        }
      }
    }

    /// <summary>
    /// Gets the input control.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    private XElement GetInputElement(string fieldName)
    {
      Item field = this.formItem.Axes.GetDescendant(fieldName);
      if (field != null)
      {
        string identifier = field.ID.ToString().Replace("{", string.Empty).Replace("}", string.Empty).Replace("-", string.Empty);

        XElement e = (from el in this.firstPart.Descendants()
                      where el.Attribute("id") != null &&
                            el.Attribute("id").Value != null &&
                            el.Attribute("id").Value.EndsWith(identifier)
                      select el).FirstOrDefault();
        return e;
      }

      return null;
    }

    /// <summary>
    /// Gets the element.
    /// </summary>
    /// <param name="tagName">Name of the tag.</param>
    /// <param name="attributeName">Name of the attribute.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <returns>returns first matched element by element tag and atribute</returns>
    private XElement GetElement(string tagName, string attributeName, string attributeValue)
    {
      XElement e = (from el in this.firstPart.Descendants()
                    where el.Name.LocalName == tagName &&
                          el.Attribute(attributeName) != null &&
                          el.Attribute(attributeName).Value.Contains(attributeValue)
                    select el).FirstOrDefault();
      return e;
    }

    /// <summary>
    /// Sets the selected check box list values.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="values">The values.</param>
    /// <returns></returns>
    public bool SetSelectedCheckBoxListValues(string fieldName, params string[] values)
    {
      XElement table = this.GetInputElement(fieldName);
      if (table != null)
      {
        IEnumerable<XElement> checkboxes = from t in table.Descendants()
                                           where t.Attribute("type") != null
                                                 && t.Attribute("type").Value == "checkbox"
                                           select t;

        foreach (XElement checkbox in checkboxes)
        {
          string val = (from c in checkbox.Parent.Descendants()
                        where c.Name.LocalName == "label"
                        select c.Value).FirstOrDefault();

          // var val = GetValueAttribute(checkbox);
          if (val != null && values.Contains(val))
          {
            SetChecked(checkbox, true);
          }
          else
          {
            SetChecked(checkbox, false);
          }
        }

        this.args.Result.FirstPart = this.GetResultFirstPart();
        return true;
      }

      return false;
    }

    /// <summary>
    /// Hides the section.
    /// </summary>
    /// <param name="sectionName">Name of the section.</param>
    /// <returns></returns>
    public bool HideSection(string sectionName)
    {
      Item sectionItem = this.formItem.Axes.GetDescendant(sectionName);
      if (sectionItem != null)
      {
        XElement sectionDiv = (from f in this.firstPart.Descendants()
                               where f.Name.LocalName == "div"
                                     && f.Attribute("class") != null
                                     && f.Attribute("class").Value == "scfLegendAsDiv"
                                     && f.Value.Contains(sectionItem["Title"])
                               select f.Parent.Parent).FirstOrDefault();

        if (sectionDiv != null)
        {
          sectionDiv.SetAttributeValue("style", "display:none");
          IEnumerable<XElement> inputFields = from s in sectionDiv.Descendants()
                                              where s.Attribute("id") != null
                                                    && s.Attribute("id").Value.EndsWith("_ecstate")
                                              select s;

          foreach (XElement inputField in inputFields)
          {
            inputField.SetAttributeValue("value", "disabled");
          }

          this.args.Result.FirstPart = this.GetResultFirstPart();
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Hides the element.
    /// </summary>
    /// <param name="tagName">Name of the tag.</param>
    /// <param name="attribute">The attribute.</param>
    /// <param name="attributeValue">The attribute value.</param>
    public void HideElement(string tagName, string attribute, string attributeValue)
    {
      XElement elem = this.GetElement(tagName, attribute, attributeValue);
      if (elem != null)
      {
        elem.SetAttributeValue("style", "display:none");
        this.args.Result.FirstPart = this.GetResultFirstPart();
      }
    }

    public void HideSectionByField(string sectionName, string sectionFieldName)
    {
      Item sectionItem = this.formItem.Axes.GetDescendant(sectionName);
      if (sectionItem != null)
      {
        Item fieldItem = sectionItem.Axes.GetDescendant(sectionFieldName);
        if (fieldItem != null)
        {
          string fieldId = fieldItem.ID.ToString().Replace("{", string.Empty).Replace("}", string.Empty);
          XElement sectionDiv = (from f in this.firstPart.Descendants()
                                 where f.Name.LocalName == "div"
                                       && f.Attribute("class") != null
                                       && f.Attribute("class").Value.Contains(fieldId)
                                 select f.Parent.Parent.Parent).FirstOrDefault();
          if (sectionDiv != null)
          {
            sectionDiv.SetAttributeValue("style", "display:none;");

            this.DisableFields(sectionDiv);
            this.args.Result.FirstPart = this.GetResultFirstPart();
          }
        }
      }
    }

    /// <summary>
    /// Hides the field.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public bool HideField(string fieldName)
    {
      Item field = this.formItem.Axes.GetDescendant(fieldName);
      if (field != null)
      {
        XElement fieldDiv = (from f in this.firstPart.Descendants()
                             where f.Name.LocalName == "div"
                                   && f.Attribute("fieldid") != null
                                   && f.Attribute("fieldid").Value == field.ID.ToString()
                             select f).FirstOrDefault();
        if (fieldDiv != null)
        {
          fieldDiv.SetAttributeValue("style", "display:none");
          IEnumerable<XElement> inputFields = from s in fieldDiv.Descendants()
                                              where s.Attribute("id") != null
                                                    && s.Attribute("id").Value.EndsWith("_ecstate")
                                              select s;
          foreach (XElement inputField in inputFields)
          {
            inputField.SetAttributeValue("value", "disabled");
          }

          this.args.Result.FirstPart = this.GetResultFirstPart();
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Hides the form and leavs the validation summary (removing validation summary will cause an script error)
    /// </summary>
    public void HideForm()
    {
      XElement rootDiv = (from f in this.firstPart.Descendants()
                          where f.Attribute("class") != null
                                && f.Attribute("class").Value == "scfForm"
                          select f).FirstOrDefault();
      if (rootDiv != null)
      {
        // rootDiv.Descendants().Remove();
        rootDiv.SetAttributeValue("style", "display:none;");

        // rootDiv.Value = " ";
      }

      this.args.Result.FirstPart = this.GetResultFirstPart();
    }

    /// <summary>
    /// Adds a button from navigation link.
    /// </summary>
    /// <param name="navigationLinkItem">The navigation link item.</param>
    /// <param name="addFirst">if set to <c>true</c> [add first].</param>
    /// <returns></returns>
    public bool AddButtonFromNavigationLink(Item navigationLinkItem, bool addFirst)
    {
      XElement submitDiv = (from f in this.firstPart.Descendants()
                            where f.Attribute("type") != null
                                  && f.Attribute("type").Value == "submit"
                            select f.Parent).FirstOrDefault();
      if (submitDiv != null)
      {
        string title = Utils.ItemUtil.GetNavigationLinkTitle(navigationLinkItem);
        string path = Utils.ItemUtil.GetNavigationLinkPath(navigationLinkItem);

        var btn = new XElement("input");
        btn.SetAttributeValue("type", "submit");
        btn.SetAttributeValue("value", title);
        btn.SetAttributeValue("onclick", "javascript:location.href='" + path + "';return false;");

        if (addFirst)
        {
          submitDiv.AddFirst(btn);
        }
        else
        {
          submitDiv.Add(btn);
        }

        this.args.Result.FirstPart = this.GetResultFirstPart();
        return true;
      }

      return false;
    }

    /// <summary>
    /// Adds a button from navigation link.
    /// </summary>
    /// <param name="navigationLinkItem">The navigation link item.</param>
    /// <returns></returns>
    internal bool AddButtonFromNavigationLink(Item navigationLinkItem)
    {
      return this.AddButtonFromNavigationLink(navigationLinkItem, false);
    }

    /// <summary>
    /// Replaces all legend with div tag.
    /// </summary>
    internal void ReplaceLegendWithDiv()
    {
      IEnumerable<XElement> legends = from f in this.firstPart.Descendants()
                                      where f.Name.LocalName == "legend"
                                      select f;

      foreach (XElement legend in legends)
      {
        var div = new XElement("div");
        div.SetAttributeValue("class", "scfLegendAsDiv");
        div.Value = legend.Value;
        legend.AddBeforeSelf(div);
      }

      legends.Remove();

      this.args.Result.FirstPart = this.GetResultFirstPart();
    }

    /// <summary>
    /// Removes the empty tags.
    /// </summary>
    /// <param name="tagName">Name of the tag.</param>
    /// <param name="className">Name of the class.</param>
    internal void RemoveEmptyTags(string tagName, string className)
    {
      (from f in this.firstPart.Descendants()
       where f.Name.LocalName == tagName
             && f.Attribute("class") != null
             && f.Attribute("class").Value == className
             && string.IsNullOrEmpty(f.Value)
       select f).Remove();

      this.args.Result.FirstPart = this.GetResultFirstPart();
    }

    /// <summary>
    /// Changes the form introduction.
    /// </summary>
    /// <param name="value">The new introduction value.</param>
    public void ChangeFormIntroduction(string value)
    {
      XElement formInfo = this.GetElement("div", "class", "scfIntroBorder");
      if (formInfo != null)
      {
        XElement info = formInfo.Descendants("span").FirstOrDefault();
        if (info != null)
        {
          info.Value = value;
          this.args.Result.FirstPart = this.GetResultFirstPart();
        }
      }
    }

    /// <summary>
    /// Changes the form title.
    /// </summary>
    /// <param name="value">The value.</param>
    public void ChangeFormTitle(string value)
    {
      //XElement formTitle = this.GetElement("div", "class", "scfTitleBorder");
      XElement formTitle = this.GetElementByLasIdPart(this.firstPart.Root ,"_title");
      if (formTitle != null)
      {
        formTitle.Value = value;
        this.args.Result.FirstPart = this.GetResultFirstPart();
      }
    }

    /// <summary>
    /// Sorunds the content with ul li.
    /// </summary>
    /// <param name="className">Name of the class.</param>
    public void SorundContentWithUlLi(string className)
    {
      XElement tag = (from f in this.firstPart.Descendants()
                      where f.Attribute("class") != null &&
                            f.Attribute("class").Value == className
                      select f).FirstOrDefault();
      if (tag != null)
      {
        var li = new XElement("li");
        var ul = new XElement("ul", li);
        li.Value = tag.Value;
        tag.ReplaceNodes(ul);
        this.args.Result.FirstPart = this.GetResultFirstPart();
      }
    }

    //TODO: remove this code after web forms fixed bug with unnecessary &nbsp symbols
    public void RemoveNbsp()
    {
      this.RemoveFromChild(this.firstPart.Root);
      this.args.Result.FirstPart = this.GetResultFirstPart();
    }

    private void RemoveFromChild(XElement root)
    {
      foreach (XElement elem in root.Descendants())
      {
        (from f in elem.Nodes()
           where f.NodeType == System.Xml.XmlNodeType.Text &&
           f.ToString().Contains("&amp;nbsp")
           select f).Remove();
           
        if (elem.HasElements)
        {
          this.RemoveFromChild(elem);
        }
      }
    }

    public void DisableInputField(string fieldName)
    {
      XElement input = this.GetInputElement(fieldName);
      if (input != null)
      {
        input.SetAttributeValue("disabled", "disabled");
        this.args.Result.FirstPart = this.GetResultFirstPart();
  }
    }

    private void DisableFields(XElement sectionDiv)
    {
      IEnumerable<XElement> inputFields = from s in sectionDiv.Descendants()
                                          where s.Attribute("id") != null
                                                && s.Attribute("id").Value.EndsWith("_ecstate")
                                          select s;

      foreach (XElement inputField in inputFields)
      {
        inputField.SetAttributeValue("value", "disabled");
      }
    }
  }
}