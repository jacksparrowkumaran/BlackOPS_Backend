﻿using BlackOPS.Models;
using BlackOPS.Models.PromoLaunch;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackOPS.Interface.Promotion.Services
{
   public interface IPromoLaunchService
    {
        List<CountryList> GetCountryList(string prefix);
        List<ProductCodeList> GetProductCodeInfo(string prefix);
        List<PricePlanInfo> GetPricePlanInfo(string prefix);
        List<AcitvePromoInfo> GetActivePromoInfo(SearchPromo searchPromo);
        APIResponse AddNewPromotion(AddNewPromoInfo addNewPromoInfo);
    }
}