// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WatiNTestBase.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the wati N test base class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.Presentation.Tests.Watin
{
  using System;
  using System.Collections.Specialized;
  using System.Configuration;
  using UITests;
  using WatiN.Core;

  /// <summary>
  /// Defines the wati N test base class.
  /// </summary>
  public class WatiNTestBase : IDisposable
  {
    /// <summary>
    /// The WaitForCompleteTimeOut.
    /// </summary>
    public static readonly int WaitForCompleteTimeOut;

    /// <summary>
    /// The WaitUntilExistsTimeOut.
    /// </summary>
    public static readonly int WaitUntilExistsTimeOut;

    /// <summary>
    /// The browser.
    /// </summary>
    private readonly IE browser = new SafeIE();

    /// <summary>
    /// Initializes static members of the <see cref="WatiNTestBase"/> class. 
    /// </summary>
    static WatiNTestBase()
    {
      NameValueCollection appSettings = ConfigurationManager.AppSettings;

      Settings.AutoMoveMousePointerToTopLeft = false;
      Settings.Instance.MakeNewIeInstanceVisible = bool.Parse(appSettings["MakeNewIeInstanceVisible"]);

      Settings.Instance.WaitForCompleteTimeOut = WaitForCompleteTimeOut = int.Parse(appSettings["WaitForCompleteTimeOut"]);
      Settings.Instance.WaitUntilExistsTimeOut = WaitUntilExistsTimeOut = int.Parse(appSettings["WaitUntilExistsTimeOut"]);
    }

    /// <summary>
    /// Gets the browser.
    /// </summary>
    protected IE CurrentBrowser
    {
      get { return this.browser; }
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      this.browser.ForceClose();
    }
  }
}