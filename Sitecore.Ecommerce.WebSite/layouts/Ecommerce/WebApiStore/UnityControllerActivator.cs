namespace Sitecore.Ecommerce.WebApiStore
{
  using System;
  using System.Net.Http;
  using System.Web.Http.Controllers;
  using System.Web.Http.Dispatcher;
  using Microsoft.Practices.Unity;

  public class UnityControllerActivator : IHttpControllerActivator
  {
    private readonly IUnityContainer container;

    public UnityControllerActivator(IUnityContainer container)
    {
      this.container = container;
    }

    public IUnityContainer Container
    {
      get { return this.container; }
    }

    public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
    {
      var controller = (IHttpController)this.container.Resolve(controllerType);

      request.RegisterForDispose(new Release(() => this.container.Teardown(controller)));

      return controller;
    }

    private class Release : IDisposable
    {
      private readonly Action release;

      public Release(Action release)
      {
        this.release = release;
      }

      public void Dispose()
      {
        this.release();
      }
    }
  }
}