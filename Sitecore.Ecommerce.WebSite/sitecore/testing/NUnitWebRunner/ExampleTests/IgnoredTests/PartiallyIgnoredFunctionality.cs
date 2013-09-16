// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartiallyIgnoredFunctionality.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the partially ignored functionality class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.NUnit.WebRunner.ExampleTests.IgnoredTests
{
  using global::NUnit.Framework;

  /// <summary>
  /// Defines the partially ignored functionality class.
  /// </summary>
  [TestFixture]
  public class PartiallyIgnoredFunctionality : SleepyTest
  {
    /// <summary>
    /// Successfull test.
    /// </summary>
    [Test]
    public void SuccessfulTest()
    {
    }

    /// <summary>
    /// Ignored test.
    /// </summary>
    [Test, Ignore("Ignored")]
    public void IgnoredTest()
    {
    }
  }
}