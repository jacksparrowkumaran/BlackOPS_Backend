using System;
using System.Collections.Generic;
using System.Text;

namespace BlackOPS.Models.PromoLaunch
{
    public class UpdatePromoInfo
    {
        public string PriceSchemeId { get; set; }
        public string ProductCode { get; set; }
        public int PricePlanId { get; set; }
        public string PricePlanType { get; set; }
        public string PriceScheme { get; set; }
        public decimal IRRegularPrice { get; set; }
        public decimal RetailRegularPrice { get; set; }
        public decimal RetailPromoPrice { get; set; }
        public decimal IRPromoPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? OldPromoDate { get; set; }
        public decimal CUV { get; set; }
        public string CountryCode { get; set; }
        public string Currency { get; set; }

        public decimal ShipFee { get; set; }

        public decimal RSP { get; set; }
    }
}
