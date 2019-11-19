
namespace BlackOPS.Models.PromoLaunch
{
    public class AddComboPromoInfo
    {
        public string MainProductCode { get; set; }
        public string PriceSchemeIds { get; set; }
        public int PricePlanId { get; set; }
        public string[] CountryCode { get; set; }
        public ComboPriceDetails[] ComboPriceDetails { get; set; }
    }

    public class ComboPriceDetails
    {
        public int Id { get; set; }
        public int PricePlanId { get; set; }
        public string ProductCode { get; set; }
        public decimal IRPrice { get; set; }
        public decimal  IRPromoPrice { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal RetailPromoPrice { get; set; }
    }
}
