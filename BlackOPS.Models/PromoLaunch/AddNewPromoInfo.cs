

using System;

namespace BlackOPS.Models.PromoLaunch
{
    public class AddNewPromoInfo
    {
        public string ProductCode { get; set; }
        public int PricePlanId { get; set; }
        public string PriceScheme { get; set; }
        public string IRRegularPrice { get; set; }
        public decimal RetailRegularPrice { get; set; }
        public decimal RetailPromoPrice { get; set; }
        public decimal IRPromoPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime OldPromoDate { get; set; }
        public int CUV { get; set; }
        public string CountryCode { get; set; }
        public string Currency { get; set; }
    }
}
