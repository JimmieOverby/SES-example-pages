// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTests.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the unit tests class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.NUnit.WebRunner.ExampleTests.Categories
{
  using global::NUnit.Framework;

  /// <summary>
  /// Defines the unit tests class.
  /// </summary>
  [TestFixture]
  public class UnitTests : SleepyTest
  {
    /// <summary>
    /// Somes the unit test.
    /// </summary>
    [Test, Category("UnitTests")]
    public void SomeUnitTest()
    {
    }

    /// <summary>
    /// Somes the integration test.
    /// </summary>
    [Test, Category("IntegrationTest")]
    public void SomeIntegrationTest()
    {
    }
  }
}