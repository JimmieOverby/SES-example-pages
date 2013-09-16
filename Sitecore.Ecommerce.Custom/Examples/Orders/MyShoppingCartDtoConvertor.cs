namespace Sitecore.Ecommerce.Examples.Orders
{
  using System.Data;
  using Data.Convertors;
  using DomainModel.Carts;

  public class MyShoppingCartDtoConvertor : ShoppingCartConvertor
  {
    public override void DomainModelToDTO(ShoppingCart model, ref DataRow row)
    {
      base.DomainModelToDTO(model, ref row);

      row["MyCustomerInfo.Hobby"] = ((MyShoppingCart)model).MyCustomerInfo.Hobby;
    }

    public override void DTOToDomainModel(DataRow row, ref ShoppingCart model)
    {
      base.DTOToDomainModel(row, ref model);

      ((MyShoppingCart)model).MyCustomerInfo.Hobby = row["MyCustomerInfo.Hobby"] as string ?? string.Empty;
    }
  }
}