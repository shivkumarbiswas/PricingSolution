using System;

/*
    Contains data for a particular item
 */
public class CartItem
{
    public CartItem(int productId, string name, string description, int quantity, decimal price)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Quantity = quantity;
        UnitPrice = price;
    }

    #region Properties
        
    public int Quantity { get; set; }
    
    public int ProductId { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }    

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice
    {
        get { return UnitPrice * Quantity; }
    }

    #endregion
}