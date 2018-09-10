using System.Collections.Generic;
using System.Linq;

/*
    Contains the items that are in the shopping cart and provides methods for their manipulation
*/

public class ShoppingCart : IShoppingCart
{
    #region Properties

    // List of items in the shopping cart
    public List<CartItem> Items { get; private set; }
    
    public static readonly ShoppingCart ShoppingCartInstance;

    // The static constructor is called as soon as the class is loaded into memory
    static ShoppingCart()
    {
        ShoppingCartInstance = new ShoppingCart();
        ShoppingCartInstance.Items = new List<CartItem>();
    }

    // A protected constructor ensures that an object can't be created from outside
    private ShoppingCart() { }

    #endregion

    #region Shopping Cart Methods
    
    //Adds an item to the shopping cart
    public void AddItem(int productId, string name, string description, int quantity, decimal price)
    {
        // If the item is already present in the shopping cart, increase the quantity
        // Otherwise, add the new item to the shopping cart

        CartItem existingItem = ShoppingCartInstance.Items.FirstOrDefault(item => item.ProductId == productId);

        if (existingItem != null)
        { 
            existingItem.Quantity++;
        }
        else
        {
            // Create a new item and add to the cart
            CartItem newItem = new CartItem(productId, name, description, quantity, price);
            ShoppingCartInstance.Items.Add(newItem);
        }
    }


    //Removes an item from the shopping cart
    public void RemoveItem(int productId)
    {
        CartItem itemToRemove = ShoppingCartInstance.Items.FirstOrDefault(item => item.ProductId == productId);
        Items.Remove(itemToRemove);
    }

    public decimal CalculateShoppingCartTotalAmount()
    {
        decimal total = 0;
        foreach (CartItem item in ShoppingCartInstance.Items)
            total += item.TotalPrice;

        return total;
    }

    #endregion    
}