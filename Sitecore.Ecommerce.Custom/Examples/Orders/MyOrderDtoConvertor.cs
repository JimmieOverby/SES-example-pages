namespace Sitecore.Ecommerce.Examples.Orders
{
  using System.Data;
  using Data.Convertors;
  using DomainModel.Orders;

  public class MyOrderDtoConvertor : OrderConvertor
  {
    public override void DomainModelToDTO(Order model, ref DataRow row)
    {
      base.DomainModelToDTO(model, ref row);

      row["MyCustomerInfo.Hobby"] = ((MyOrder)model).MyCustomerInfo.Hobby;
    }

    public override void DTOToDomainModel(DataRow row, ref Order model)
    {
      base.DTOToDomainModel(row, ref model);

      ((MyOrder)model).MyCustomerInfo.Hobby = row["MyCustomerInfo.Hobby"] as string ?? string.Empty;
    }
  }
}