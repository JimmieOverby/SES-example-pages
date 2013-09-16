// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestingSuitePage.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the TestingSuitePage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.Presentation.Tests.Watin
{
  using FluentAssertions;
  using WatiN.Core;

  /// <summary>
  /// Defines the testing suite page class.
  /// </summary>
  public class TestingSuitePage : Page
  {
    /// <summary>
    /// Gets the banner.
    /// </summary>
    private Element Banner
    {
      get
      {
        return this.Document.Element("qunit-banner");
      }
    }

    /// <summary>
    /// Asserts the tests are passed.
    /// </summary>
    public void AssertTestsArePassed()
    {
      this.Banner.ClassName.Should().Contain("qunit-pass");
    }
  }
}