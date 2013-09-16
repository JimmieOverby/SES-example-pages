namespace Sitecore.Ecommerce.WebApiStore.Models
{
  using System;

  public class OrderHistoryModel
  {
    public string OrderNumber { get; set; }

    public DateTime Date { get; set; }

    public decimal Price { get; set; }
  }
}