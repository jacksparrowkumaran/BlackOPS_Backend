

namespace BlackOPS.Models.PromoLaunch
{
    public class AddNewPromoInfo
    {
        public string ProductCode { get; set; }
        public int PricePlanId { get; set; }
        public string PriceScheme { get; set; }
        public decimal IRRegularPrice { get; set; }
        public decimal RetailRegularPrice { get; set; }
        public decimal RetailPromoPrice { get; set; }
        public decimal IRPromoPrice { get; set; }
        public string StartDate { get; set; }
        public string EndtDate { get; set; }
        public string OldPromoDate { get; set; }
        public int CUV { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
    }
}
