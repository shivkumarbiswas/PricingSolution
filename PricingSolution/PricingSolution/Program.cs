using Newtonsoft.Json.Linq;
using PricingSolution.Impl;
using PricingSolution.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var shoppingCartResourceName = "PricingSolution.Resources.ShoppingCart.json";
            JArray shoppingCartResource = JSONUtility.GetJSONArray(shoppingCartResourceName);

            foreach (JObject shoppingCartItem in shoppingCartResource)
            {
                string name = shoppingCartItem.GetValue("name").ToString();
                string description = shoppingCartItem.GetValue("description").ToString();
                decimal price = Convert.ToDecimal(shoppingCartItem.GetValue("price"));
                int productId = Convert.ToInt32(shoppingCartItem.GetValue("productId"));
                int quantity = Convert.ToInt32(shoppingCartItem.GetValue("quantity"));

                // Adding items in shopping cart
                ShoppingCart.ShoppingCartInstance.AddItem(productId, name, description, quantity, price);
            }

            // Calculating total amount of shopping cart
            decimal totalAmount = ShoppingCart.ShoppingCartInstance.CalculateShoppingCartTotalAmount();

            // Check if the discount is applied or not on shopping cart and which discount is applied on the shopping cart
            var discountResourceName = "PricingSolution.Resources.Discount.json";
            JObject discountResource = JSONUtility.GetJSONObject(discountResourceName);
            bool discountApplied = Convert.ToBoolean(discountResource.GetValue("discountApplied"));
            string discountName = discountResource.GetValue("discountName").ToString();

            // If discount is applied then fetch the rules for that discount and compute the discount
            Discount discount = new Discount();
            if (discountApplied && !string.IsNullOrEmpty(discountName))
            {
                discount = DiscountRulesEngine.CalculateDiscountOnShoppingCart(ShoppingCart.ShoppingCartInstance, discountName);
            }

            // If discount "2 + 1 pants" is applied, then the discount object is
            // DiscountProductId is 3 which means discount is applied on product having product id 3 i.e., "Pants", DiscountedAmount is 0 which means there is no discount on monetary terms and 
            // DiscountedQuantity is 1 which means 1 quantity of "Pant" will be available for free at the same cost of two.

            // If discount "25% off" is applied, then the discount object is
            // DiscountProductId is 3 which means discount is applied on product having product id 3 i.e., "Pants", DiscountedAmount is 225 which means there is a discount on monetary terms and 
            // discount is 225 which will be decucted from the total price and the actual price will be 675 and DiscountedQuantity is 0 which means there is no discount on quantity terms.
        }
    }
}
