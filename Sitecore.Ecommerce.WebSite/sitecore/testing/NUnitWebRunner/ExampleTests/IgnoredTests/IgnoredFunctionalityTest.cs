// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IgnoredFunctionalityTest.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the ignored functionality test class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.NUnit.WebRunner.ExampleTests.IgnoredTests
{
  using global::NUnit.Framework;

  /// <summary>
  /// Defines the ignored functionality test class.
  /// </summary>
  [TestFixture]
  public class IgnoredFunctionalityTest : SleepyTest
  {
    /// <summary>
    /// Ignored test.
    /// </summary>
    [Test, Ignore("Some innored functionality")]
    public void IgnoredTest()
    {
    }
  }
}