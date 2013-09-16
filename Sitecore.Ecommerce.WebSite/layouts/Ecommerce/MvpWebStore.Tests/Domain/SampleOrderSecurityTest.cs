// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleOrderSecurityTest.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   Defines the SampleOrderSecurityTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.MvpWebStore.Tests.Domain
{
  using FluentAssertions;
  using Sitecore.Ecommerce.MvpWebStore.Domain;
  using Sitecore.Ecommerce.OrderManagement;
  using Sitecore.Ecommerce.OrderManagement.Orders;
  using Sitecore.Ecommerce.Visitor.OrderManagement;

  using Xunit;
  using Xunit.Extensions;

  /// <summary>
  /// The sample order security test.
  /// </summary>
  public class SampleOrderSecurityTest
  {
    /// <summary>
    /// The order security
    /// </summary>
    private readonly VisitorOrderSecurity orderSecurity;

    /// <summary>
    /// Initializes a new instance of the <see cref="SampleOrderSecurityTest" /> class.
    /// </summary>
    public SampleOrderSecurityTest()
    {
      this.orderSecurity = new SampleOrderSecurity();
    }

    /// <summary>
    /// Should manage order security according to order state code.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    [Theory]
    [InlineData(OrderStateCode.New, true)]
    [InlineData(OrderStateCode.Open, true)]
    [InlineData(OrderStateCode.InProcess, true)]
    [InlineData(OrderStateCode.Cancelled, false)]
    [InlineData(OrderStateCode.Closed, false)]
    [InlineData(null, false)]
    [InlineData("", false)]
    public void ShouldManageOrderSecurityAccordingToOrderStateCode(string code, bool expected)
    {
      // Arrange
      Order order = new Order { State = new State { Code = code } };

      // Act
      bool result = this.orderSecurity.CanCancel(order);

      // Assert
      result.Should().Be(expected);
    }

    /// <summary>
    /// Should return false if order state is null.
    /// </summary>
    [Fact]
    public void ShouldReturnFalseIfOrderStateIsNull()
    {
      // Arrange
      Order order = new Order();

      // Act
      bool result = this.orderSecurity.CanCancel(order);

      // Assert
      result.Should().Be(false);
    }
  }
}