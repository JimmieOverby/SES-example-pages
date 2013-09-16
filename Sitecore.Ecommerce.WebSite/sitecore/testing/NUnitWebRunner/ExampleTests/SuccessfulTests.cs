// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SuccessfullTests.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the successful tests class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.NUnit.WebRunner.ExampleTests
{
  using global::NUnit.Framework;

  /// <summary>
  /// Defines the successful tests class.
  /// </summary>
  [TestFixture]
  public class SuccessfulTests : SleepyTest
  {
    /// <summary>
    /// Successful test one.
    /// </summary>
    [Test]
    public void SuccessfulTestOne()
    {
    }

    /// <summary>
    /// Successful test two.
    /// </summary>
    [Test]
    public void SuccessfulTestTwo()
    {
    }

    [TestCase(TestName = "Should do some special feature")]
    [TestCase(TestName = "Should not do some another feature")]
    public void SomeFeatureTest()
    {
    }
  }
}