
using System.Collections.Generic;

namespace BlackOPS.Models.PromoLaunch
{
    public class ActiveComborPromo
    {
        public List<ActiveComboPriceDetails> ComboPriceDetails { get; set; }
        public decimal IR_RegularPrice { get; set; }
        public decimal IR_PromoPrice { get; set; }
        public decimal Retail_RegPrice { get; set; }
        public decimal Retail_PromoPrice { get; set; }

    }

    public class ActiveComboPriceDetails
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public decimal IRPrice { get; set; }
        public decimal IRPromoPrice { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal RetailPromoPrice { get; set; }
    }
}
