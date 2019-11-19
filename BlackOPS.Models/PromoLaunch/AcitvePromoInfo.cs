using System;
using System.Collections.Generic;
using System.Text;

namespace BlackOPS.Models.PromoLaunch
{
    public class AcitvePromoInfo
    {
        public int PriceSchemeId { get; set; }
        public string ProductCode { get; set; }
        public string ProductDesc { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal RegularPrice { get; set; }
        public decimal PromoPrice { get; set; }
        public string SchemeName { get; set; }
        public bool IsRetail { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }

        public string PriceSchemeIds { get; set; }
    }
}
