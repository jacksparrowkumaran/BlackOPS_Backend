using BlackOPS.Models;
using BlackOPS.Models.PromoLaunch;
using BlackOPS.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace BlackOPS.Service
{
    public class PromoLaunchService
    {
        private PromoLaunchRepository promoLaunchRepository = null;
        public PromoLaunchService(IOptions<ConfigurationManager> settings)
        {
            promoLaunchRepository = new PromoLaunchRepository(settings);
        }

        public List<CountryList> GetCountryList(string prefix)
        {
            return promoLaunchRepository.GetCountryList(prefix);
        }

        public List<ProductCodeList> GetProductCodeInfo(string prefix)
        {
            return promoLaunchRepository.GetProductCodeInfo(prefix);
        }

        public List<PricePlanInfo> GetPricePlanInfo(string prefix)
        {
            return promoLaunchRepository.GetPricePlanInfo(prefix);
        }

        public List<AcitvePromoInfo> GetActivePromoInfo(string productCode, string schemeName, int pricePlanId)
        {
            return promoLaunchRepository.GetActivePromoInfo(productCode, schemeName, pricePlanId);
        }
    }
}
