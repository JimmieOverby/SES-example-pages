// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrdersPresenter.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The orders presenter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Presenters
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using Newtonsoft.Json;
  using Sitecore.Ecommerce.MvpWebStore.Domain;
  using Sitecore.Ecommerce.MvpWebStore.Views;
  using Sitecore.Ecommerce.OrderManagement;
  using Sitecore.Ecommerce.OrderManagement.Orders;
  using Sitecore.Ecommerce.Visitor.OrderManagement;
  using WebFormsMvp;
  using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

  /// <summary>
  /// Defines the presenter that manages the presentation and behavior of the Orders view.
  /// First of all it checks the specified user id, casts the repository to IUserAware interface and sets the one.
  /// The repository itself retrieves all the orders that belong to such user.
  /// At the end the presenter sets the Orders collection of the ViewModel to the retrieved orders collection.
  /// The presenter contains the functionality to export all orders to JSON format.
  /// Unlike the main page it retrieves all orders using Core API, i.e. without any restrictions.
  /// </summary>
  public class OrdersPresenter : Presenter<IOrdersView>
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
    /// The order repository.
    /// </summary>
    private readonly VisitorOrderManager orderManager;

    /// <summary>
    /// The order manager.
    /// </summary>
    private readonly SampleOrderManager sampleOrderManager;

    /// <summary>
    /// The serializer settings.
    /// </summary>
    private readonly JsonSerializerSettings settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrdersPresenter" /> class.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="orderManager">The order repository.</param>
    /// <param name="sampleOrderManager">The order manager.</param>
    public OrdersPresenter(IOrdersView view, VisitorOrderManager orderManager, SampleOrderManager sampleOrderManager)
      : base(view)
    {
      this.View.Load += this.Load;
      this.View.Export += this.Export;
      this.View.Import += this.Import;

      this.orderManager = orderManager;
      this.sampleOrderManager = sampleOrderManager;

      this.settings = new JsonSerializerSettings
                   {
                     ReferenceLoopHandling = ReferenceLoopHandling.Ignore, 
                     MissingMemberHandling = MissingMemberHandling.Ignore, 
                     PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                     Error = this.JsonErrorHandler 
                   };
    }

    /// <summary>
    /// Loads the specified sender.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void Load(object sender, EventArgs e)
    {
      // Gets the user id from the query string.
      var userId = this.HttpContext.Request.QueryString["user"];

      // Stops to work in case the one isn't specified.
      if (string.IsNullOrEmpty(userId))
      {
        return;
      }

      // Casts the repository to the IUserAware and
      // sets the CustomerId.
      var aware = this.orderManager as IUserAware;
      if (aware != null)
      {
        aware.CustomerId = userId;
      }

      // Retrieves all orders and sets the View.Model to this collection.
      var orders = this.orderManager.GetAll().ToArray();
      this.View.Model.Orders = orders;
    }

    /// <summary>
    /// Exports all orders to JSON format.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void Export(object sender, EventArgs e)
    {
      this.ConfigureSerializedOrders();

      this.WriteSerializedOrders();

      this.ExportSerializedOrders();
    }

    /// <summary>
    /// Imports all orders to the database from the JSON format.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void Import(object sender, EventArgs e)
    {
      this.ReadSerializedOrders();

      this.ImportDeserializedOrders();

      // Redirects to the same page. After reload new orders will appear.
      this.HttpContext.Response.Redirect(this.HttpContext.Request.RawUrl);
    }

    /// <summary>
    /// Reads the serialized orders from the imported file.
    /// </summary>
    private void ReadSerializedOrders()
    {
      using (var file = new FileStream(this.filePath, FileMode.Open, FileAccess.Read))
      {
        using (var stream = new StreamReader(file))
        {                                                                
          this.View.Model.SerializedOrders = stream.ReadToEnd();
        }
      }
    }

    /// <summary>
    /// Imports the deserialized orders.
    /// </summary>
    private void ImportDeserializedOrders()
    {
      var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(this.View.Model.SerializedOrders,  this.settings).AsQueryable();
      foreach (var order in orders)
      {
        this.sampleOrderManager.SaveSingleOrder(order);
      }
    }

    /// <summary>
    /// Retrieves all orders using Core API
    /// and serializes them to JSON format.
    /// </summary>
    private void ConfigureSerializedOrders()
    {
      var orders = this.sampleOrderManager.GetAllOrders(o => true).ToArray();
      this.View.Model.SerializedOrders = JsonConvert.SerializeObject(orders, Formatting.None, this.settings);
    }

    /// <summary>
    /// Writes the serialized orders to the file 
    /// under the web site root.
    /// </summary>
    private void WriteSerializedOrders()
    {
      using (var file = new FileStream(this.filePath, FileMode.Create, FileAccess.Write))
      {
        using (var stream = new StreamWriter(file))
        {
          stream.Write(this.View.Model.SerializedOrders);
        }
      }
    }

    /// <summary>
    /// Writes the file with serialized orders
    /// to the current Response.
    /// </summary>
    private void ExportSerializedOrders()
    {
      this.HttpContext.Response.Clear();
      this.HttpContext.Response.ContentType = "application/json";
      this.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=\"" + FileName + "\"");
      this.HttpContext.Response.WriteFile(this.filePath);
      this.HttpContext.Response.Flush();
      this.HttpContext.Response.End();
    }

    /// <summary>
    /// Error handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ErrorEventArgs" /> instance containing the event data.</param>
    private void JsonErrorHandler(object sender, ErrorEventArgs e)
    {
      e.ErrorContext.Handled = true;
    }
  }
}