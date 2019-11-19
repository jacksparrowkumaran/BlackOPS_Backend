using System;
using System.Collections.Generic;
using System.Text;

namespace BlackOPS.Models.PromoLaunch
{
    public class SearchPromo
    {
        public string ProductCode { get; set; }

        public string[] CountryCode { get; set; }

        public int PricePlanId { get; set; }
    }
}
