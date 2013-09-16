namespace Sitecore.Ecommerce.WebApiStore.Pipelines.Loader
{
  using System;
  using System.Web.Http;
  using System.Web.Routing;

  public class MapWebStoreRouters
  {
    public void Process(EventArgs e)
    {
      RouteTable.Routes.MapHttpRoute(
        name: "DefaultApi",
        routeTemplate: "api/{controller}/{id}",
        defaults: new { id = RouteParameter.Optional });
    }
  }
}