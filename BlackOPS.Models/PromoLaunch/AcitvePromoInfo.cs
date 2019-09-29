using System;
using System.Collections.Generic;
using System.Text;

namespace BlackOPS.Models.PromoLaunch
{
    public class AcitvePromoInfo
    {
        public string ProductCode { get; set; }
        public string ProductDesc { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public decimal RegularPrice { get; set; }

        public decimal PromoPrice { get; set; }
        public string SchemeName { get; set; }
        public bool IsRetail { get; set; }
    }
}
