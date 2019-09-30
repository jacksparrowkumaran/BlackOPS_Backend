using System;
using System.Collections.Generic;
using System.Text;

namespace BlackOPS.Models.PromoLaunch
{
    public class SelectedPromoInfo
    {
        public string ProductCode { get; set; }

        public string PricePlan { get; set; }
        public string PriceScheme { get; set; }
        public decimal IRRegularPrice { get; set; }

        public decimal RetailRegularPrice { get; set; }

        public decimal PromoPrice { get; set; }

        public decimal RetailPromoPrice { get; set; }

        public string StartDate { get; set; }

        public string EndtDate { get; set; }

        public string OldPromoEndDate { get; set; }

        public string CUV { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public string Currency { get; set; }
    }
}
