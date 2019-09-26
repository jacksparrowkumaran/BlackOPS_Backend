using System;
using System.Collections.Generic;
using System.Text;

namespace BlackOPS.Models.PromoLaunch
{
    public class UpdatePromoInfo
    {
        public int PriceSchemeID { get; set; }
        public string EndDate { get; set; }

        public string RegularPrice { get; set; }

        public string PromoPrice { get; set; }

        public string Currency { get; set; }

        public string CountryCode { get; set; }
    }
}
