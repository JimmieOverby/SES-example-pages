namespace Sitecore.Ecommerce.WebApiStore.Controllers
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.Http;
  using Sitecore.Ecommerce.OrderManagement;
  using Sitecore.Ecommerce.WebApiStore.Models;

  public class OrderHistoryController : ApiController
  {
    private readonly IVisitorOrderRepository repository;

    public OrderHistoryController(IVisitorOrderRepository repository)
    {
      this.repository = repository;
    }

    public IEnumerable<OrderHistoryModel> GetOrders()
    {
      var userName = this.User.Identity.Name;

      return this.repository.GetOrdersFor(userName).Select(o => new OrderHistoryModel { OrderNumber = o.OrderId, Date = o.IssueDate });
    }
  }
}