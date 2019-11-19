using System;
using System.Collections.Generic;
using System.Text;

namespace BlackOPS.Models.PromoLaunch
{
    public class AddComboProduct
    {
        public int PricePlanId { get; set; }

        public string ProductCode { get; set; }

        public decimal IRPrice { get; set; }

        public decimal IRPromoPrice { get; set; }

        public decimal RetailPrice { get; set; }

        public decimal RetailPromoPrice { get; set; }
    }
}
