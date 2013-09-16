// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleOrderCancelationStrategyTest.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The sample order cancelation strategy test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Tests.Domain
{
  using FluentAssertions;
  using Sitecore.Ecommerce.MvpWebStore.Domain;
  using Sitecore.Ecommerce.OrderManagement;
  using Sitecore.Ecommerce.OrderManagement.Orders;
  using Xunit;

  /// <summary>
  /// The sample order cancelation strategy test.
  /// </summary>
  public class SampleOrderCancelationStrategyTest
  {
    /// <summary>
    /// Should set order state code to cancelled.
    /// </summary>
    [Fact]
    public void ShouldSetOrderStateCodeToCancelled()
    {
      // Arrange
      var strategy = new SampleOrderCancelationStrategy();
      var order = new Order { State = new State { Code = OrderStateCode.New } };

      // Act
      strategy.Process(order);

      // Assert
      order.State.Code.Should().Be(OrderStateCode.Cancelled);
    }

    /// <summary>
    /// Should set order state name to cancelled.
    /// </summary>
    [Fact]
    public void ShouldSetOrderStateNameToCancelled()
    {
      // Arrange
      var strategy = new SampleOrderCancelationStrategy();
      var order = new Order { State = new State { Name = OrderStateCode.New } };

      // Act
      strategy.Process(order);

      // Assert
      order.State.Name.Should().Be(OrderStateCode.Cancelled);
    }
  }
}