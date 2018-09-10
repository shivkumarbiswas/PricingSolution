using Newtonsoft.Json.Linq;
using PricingSolution.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingSolution.Utilities
{
    public class DiscountRulesEngine
    {
        public static readonly JArray DiscountRules;

        // Fetch the Discount Rules
        static DiscountRulesEngine()
        {
            var discountRulesResourceName = "PricingSolution.Resources.DiscountRules.json";
            DiscountRules = JSONUtility.GetJSONArray(discountRulesResourceName);
        }

        public static Discount CalculateDiscountOnShoppingCart(ShoppingCart shoppingCart, string discountName)
        {
            JObject discount = null;
            Discount discountOnShoppingCart = new Discount();

            foreach (JObject discountRule in DiscountRules)
            {
                string name = discountRule.GetValue("discountName").ToString();
                if (name.Equals(discountName))
                {
                    discount = discountRule;
                    break;
                }                
            }

            if (discount != null)
            {
                int discountObjectProductId = Convert.ToInt32(discount.GetValue("discountObjectProductId"));
                string discountObjectProperty = discount.GetValue("discountObjectProperty").ToString();
                string discountObjectComparison = discount.GetValue("discountObjectComparison").ToString();
                int discountObjectTargetValue = Convert.ToInt32(discount.GetValue("discountObjectTargetValue"));                

                CartItem product = shoppingCart.Items.FirstOrDefault(item => item.ProductId == discountObjectProductId);

                if (discountObjectProperty.Equals("quantity"))
                {
                    if (discountObjectComparison.Equals("greater_than"))
                    {
                        if (product.Quantity > discountObjectTargetValue)
                        {
                            if (!string.IsNullOrEmpty(discount.GetValue("discountedQuantity").ToString()))
                            {
                                int discountedQuantity = Convert.ToInt32(discount.GetValue("discountedQuantity"));
                                discountOnShoppingCart.DiscountedProductId = discountObjectProductId;
                                discountOnShoppingCart.DiscountedQuantity = discountedQuantity;
                                discountOnShoppingCart.DiscountedAmount = 0;
                            }                      
                        }
                    }
                    else if (discountObjectComparison.Equals("less_than"))
                    {
                        if (product.Quantity < discountObjectTargetValue)
                        {

                        }
                    }
                    else if (discountObjectComparison.Equals("equals"))
                    {
                        if (product.Quantity == discountObjectTargetValue)
                        {

                        }
                    }
                }
                else if (discountObjectProperty.Equals("total_price"))
                {
                    if (discountObjectComparison.Equals("greater_than"))
                    {
                        if (product.TotalPrice > discountObjectTargetValue)
                        {
                            if (!string.IsNullOrEmpty(discount.GetValue("discountedAmountInPercentage").ToString()))
                            {
                                decimal discountedAmountInPercentage = Convert.ToInt32(discount.GetValue("discountedAmountInPercentage"));
                                discountOnShoppingCart.DiscountedProductId = discountObjectProductId;
                                discountOnShoppingCart.DiscountedQuantity = 0;
                                discountOnShoppingCart.DiscountedAmount = product.TotalPrice - (discountedAmountInPercentage/100 * (product.TotalPrice));
                            }
                        }
                    }
                    else if (discountObjectComparison.Equals("less_than"))
                    {
                        if (product.Quantity < discountObjectTargetValue)
                        {

                        }
                    }
                    else if (discountObjectComparison.Equals("equals"))
                    {
                        if (product.Quantity == discountObjectTargetValue)
                        {

                        }
                    }
                }                
            }

            return discountOnShoppingCart;
        }
    }
}
