// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitializePresenterFactory.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The processor is part of the Sitecore Initialize pipeline and
//   used to configure WebFormsMvp Presenter Binder Factory.
//   Sitecore E-Commerce Services is based on Unity IoC Container,
//   so UnityPresenterFactory is used to instantiate presenters and
//   supply them with proper dependencies.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Pipelines.Loader
{
  using Microsoft.Practices.Unity;
  using Sitecore.Pipelines;
  using WebFormsMvp.Binder;
  using WebFormsMvp.Unity;

  /// <summary>
  /// The processor is part of the Sitecore Initialize pipeline and 
  /// used to configure WebFormsMvp Presenter Binder Factory.
  /// Sitecore E-Commerce Services is based on Unity IoC Container, 
  /// so UnityPresenterFactory is used to instantiate presenters and
  /// supply them with proper dependencies.
  /// </summary>
  public class InitializePresenterFactory
  {
    /// <summary>
    /// Gets or sets the name of the web shop.
    /// In Sitecore E-Commerce Services each web shop has got own Unity container
    /// based on the default one configured in Unity.config file.
    /// The web shop name is used to locate the Unity container for specific web shop
    /// using the next pattern: "UnityContainer_[web shop name]".
    /// </summary>
    /// <value>
    /// The name of the web shop.
    /// </value>
    [CanBeNull]
    public string WebShopName { get; set; }

    /// <summary>
    /// Configures the Presenter Binder Factory. 
    /// UnityPresenterFactory is used as default one.
    /// </summary>
    /// <param name="args">The args.</param>
    public void Process(PipelineArgs args)
    {
      // Ignore the processor in case of Sitecore UnitTesting.
      // The processor is part of the Sitecore Initialize pipeline 
      // which run each time Sitecore.IsUnitTesting property is set to True.
      // Since Presenter Binder Factory can be set only once,
      // it is not possible to run tests several times without getting an exception.
      if (Sitecore.Context.IsUnitTesting)
      {
        return;
      }

      var container = (IUnityContainer)args.CustomData[string.Format("UnityContainer_{0}", this.WebShopName)];
      PresenterBinder.Factory = new UnityPresenterFactory(container);
    }
  }
}