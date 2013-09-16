// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderReportModelExtended.cs" company="Sitecore A/S">
//   Copyright (c) Sitecore A/S. All rights reserved.
// </copyright>
// <summary>
//   The custom order report model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Ecommerce.Examples.Reports
{
  using Report;

  /// <summary>
  /// The custom order report model.
  /// </summary>
  public class OrderReportModelExtended : OrderReportModel
  {
    /// <summary>
    /// Gets the buyer party supplier assigned account id.
    /// </summary>
    /// <value>The buyer party supplier assigned account id.</value>
    [NotNull]
    public virtual string FreightForwarderPartyIdentification
    {
      get
      {
        if ((this.Order != null) && (this.Order.DefaultFreightForwarderParty != null))
        {
          return this.Order.DefaultFreightForwarderParty.PartyIdentification;
        }

        return string.Empty;
      }
    } 
  }
}