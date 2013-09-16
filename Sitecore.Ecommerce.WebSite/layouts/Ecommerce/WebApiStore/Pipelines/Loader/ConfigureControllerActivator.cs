namespace Sitecore.Ecommerce.WebApiStore.Pipelines.Loader
{
  using System.Web.Http;
  using System.Web.Http.Dispatcher;
  using Microsoft.Practices.Unity;
  using Sitecore.Ecommerce.WebApiStore.Domain;
  using Sitecore.Pipelines;

  public class ConfigureControllerActivator
  {
    public void Process(PipelineArgs args)
    {
      IUnityContainer container = (IUnityContainer)args.CustomData["UnityContainer"];

      container.RegisterType<ProductRepositoryBase, InMemoryProductRepository>();

      GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new UnityControllerActivator(container));
    }
  }
}