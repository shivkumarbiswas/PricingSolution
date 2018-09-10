using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingSolution.Impl
{
    public class Discount
    {
        public int DiscountedProductId { get; set; }

        public int DiscountedQuantity { get; set; }

        public decimal DiscountedAmount { get; set; }
    }
}
