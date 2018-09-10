using System.Collections.Generic;
using System.Linq;

/*
    Contains the items that are in the shopping cart and provides methods for their manipulation
*/

public interface IShoppingCart
{
    void AddItem(int productId, string name, string description, int quantity, decimal price);

    void RemoveItem(int productId);

    decimal CalculateShoppingCartTotalAmount();
}