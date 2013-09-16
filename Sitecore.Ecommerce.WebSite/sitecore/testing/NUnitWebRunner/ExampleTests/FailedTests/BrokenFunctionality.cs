// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrokenFunctionality.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the failed test class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.NUnit.WebRunner.ExampleTests.FailedTests
{
  using System;
  using global::NUnit.Framework;

  /// <summary>
  /// Defines the failed test class.
  /// </summary>
  [TestFixture]
  public class BrokenFunctionality : SleepyTest
  {
    /// <summary>
    /// Failure .
    /// </summary>
    /// <exception cref="InvalidOperationException">Something is wrong!</exception>
    [Test]
    public void FailedTest()
    {
      throw new InvalidOperationException("Something is wrong!");
    }
  }
}