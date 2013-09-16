namespace Sitecore.Ecommerce.WebApiStore.Controllers
{
  using System.Web.Http;
  using Sitecore.Ecommerce.DomainModel.Users;

  public class LoginController : ApiController
  {
    private readonly ICustomerManager<CustomerInfo> customerManager;

    public LoginController(ICustomerManager<CustomerInfo> customerManager)
    {
      this.customerManager = customerManager;
    }

    public void Login(string login, string password)
    {
      this.customerManager.LogInCustomer(login, password);
    }
  }
}