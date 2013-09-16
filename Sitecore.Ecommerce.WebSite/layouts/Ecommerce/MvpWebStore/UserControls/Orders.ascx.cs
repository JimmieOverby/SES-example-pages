// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Orders.ascx.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The orders.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.UserControls
{
  using System;
  using System.Configuration;
  using System.IO;
  using System.Linq;
  using System.Web.Configuration;
  using System.Xml.Linq;
  using Sitecore.Ecommerce.MvpWebStore.ViewModels;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using WebFormsMvp.Web;

  /// <summary>
  /// Defines the Orders user control.
  /// Renders all orders that belong to specific user.
  /// </summary>
  public partial class Orders : MvpUserControl<OrdersViewModel>, IOrdersView
  {
    /// <summary>
    /// The file name.
    /// </summary>
    private const string FileName = "orders.json";

    /// <summary>
    /// The file path.
    /// </summary>
    private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);

    /// <summary>
    /// The Export event.
    /// </summary>
    public event EventHandler<EventArgs> Export;

    /// <summary>
    /// Occurs when import orders..
    /// </summary>
    public event EventHandler<EventArgs> Import;

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      this.UpdateAssemblyBindings();

      this.OrdersRepeater.DataSource = this.Model.Orders;
    }

    /// <summary>
    /// Raises the <see cref="Export" /> event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="eventArgs">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    protected void OnExport(object sender, EventArgs eventArgs)
    {
      var export = this.Export;
      if (export != null)
      {
        export(this, eventArgs);
      }
    }

    /// <summary>
    /// Called when import.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="eventArgs">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected void OnImport(object sender, EventArgs eventArgs)
    {
      if (this.ImportOrdersDialog.HasFile)
      {
        this.ImportOrdersDialog.SaveAs(this.filePath);
        var import = this.Import;
        if (import != null)
        {
          import(this, eventArgs);
        }
      }
    }

    /// <summary>
    /// Updates the assembly bindings.
    /// Performs the trick with setting the Newtonsoft.Json version
    /// from the default 3.5 that is provided with Sitecore CMS installation
    /// to the 4.5 that is provided with remote services.
    /// </summary>
    private void UpdateAssemblyBindings()
    {
      const string NewDependentAssemblyDefinition = "<dependentAssembly xmlns=\"urn:schemas-microsoft-com:asm.v1\"><codeBase version=\"4.5.0.0\" href=\"sitecore modules/Shell/Ecommerce/Services/servicebin/Newtonsoft.Json.dll\" /><assemblyIdentity name=\"Newtonsoft.Json\" publicKeyToken=\"30ad4fe6b2a6aeed\" version=\"4.5.0.0\" culture=\"neutral\" /><bindingRedirect oldVersion=\"3.5.0.0\" newVersion=\"4.5.0.0\"/></dependentAssembly>";
      var info = WebConfigurationManager.OpenWebConfiguration("~");
      ConfigurationSection runtime = info.Sections["runtime"];
      var rawXml = runtime.SectionInformation.GetRawXml();
      XElement rootRuntimeElement = XElement.Parse(rawXml);

      var assemblyBindingElement = rootRuntimeElement.FirstNode as XElement;
      if (assemblyBindingElement != null)
      {
        XNamespace xmlns = assemblyBindingElement.Attribute("xmlns").Value;
        var exists = assemblyBindingElement.Descendants(xmlns + "assemblyIdentity").Any(el => el.Attribute("name").Value == "Newtonsoft.Json");
        if (!exists)
        {
          var newDependentAssemblyElement = XElement.Parse(NewDependentAssemblyDefinition);
          assemblyBindingElement.Add(newDependentAssemblyElement);
          runtime.SectionInformation.SetRawXml(rootRuntimeElement.ToString());
          info.Save(ConfigurationSaveMode.Modified);
        }
      }
    }
  }
}