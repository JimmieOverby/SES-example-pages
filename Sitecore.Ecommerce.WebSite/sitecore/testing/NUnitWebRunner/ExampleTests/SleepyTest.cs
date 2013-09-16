// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SleepyTest.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the sleepy test class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.NUnit.WebRunner.ExampleTests
{
  using System;

  /// <summary>
  /// Defines the sleepy test class.
  /// </summary>
  public class SleepyTest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SleepyTest"/> class.
    /// </summary>
    public SleepyTest()
    {
      Random random = new Random();
      int timeout = random.Next(50, 500);
      System.Threading.Thread.Sleep(timeout);
    }
  }
}