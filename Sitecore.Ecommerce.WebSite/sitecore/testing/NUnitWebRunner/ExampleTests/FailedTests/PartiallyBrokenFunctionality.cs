// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartiallyBrokenFunctionality.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the partially broken functionality class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.NUnit.WebRunner.ExampleTests.FailedTests
{
  using System;
  using global::NUnit.Framework;

  /// <summary>
  /// Defines the partially broken functionality class.
  /// </summary>
  public class PartiallyBrokenFunctionality : SleepyTest
  {
    /// <summary>
    /// Broken test.
    /// </summary>
    /// <exception cref="InvalidOperationException">It's broken!</exception>
    [Test]
    public void BrokenTest()
    {
      throw new InvalidOperationException("This functionality is broken!");
    }

    /// <summary>
    /// Successful test.
    /// </summary>
    [Test]
    public void SuccessfulTest()
    {
    }
  }
}