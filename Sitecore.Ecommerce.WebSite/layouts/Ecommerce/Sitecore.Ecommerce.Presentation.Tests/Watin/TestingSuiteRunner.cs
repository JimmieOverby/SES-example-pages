// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestingSuiteRunner.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the client scripts test class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.Presentation.Tests.Watin
{
  using System.Configuration;
  using Xunit;

  /// <summary>
  /// Defines the client scripts test class.
  /// </summary>
  public class TestingSuiteRunner : WatiNTestBase
  {
    /// <summary>
    /// The home page URL.
    /// </summary>
    private readonly string startUrl = ConfigurationManager.AppSettings["StartUrl"];

    /// <summary>
    /// The test fixture URL.
    /// </summary>
    private readonly string testFixtureUrl = ConfigurationManager.AppSettings["TestFixtureUrl"];

    /// <summary>
    /// Should pass the client script tests.
    /// </summary>
    [Fact]
    public void ShouldPassTheClientScriptTests()
    {
      // Arrange
      var testingSuiteUrl = string.Format("{0}{1}", this.startUrl, this.testFixtureUrl);
      
      // Act
      this.CurrentBrowser.GoTo(testingSuiteUrl);
      var testSuitePage = this.CurrentBrowser.Page<TestingSuitePage>();

      // Assert
      testSuitePage.AssertTestsArePassed();
    } 
  }
} 