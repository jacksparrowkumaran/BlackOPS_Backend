using BlackOPS.Interface.Promotion.Repositories;
using BlackOPS.Interface.Promotion.Services;
using BlackOPS.Models;
using BlackOPS.Models.PromoLaunch;
using System.Collections.Generic;

namespace BlackOPS.Service
{
    public class PromoLaunchService : IPromoLaunchService
    {
        private IPromoLaunchRepository iPromoLaunchRepository = null;
        public PromoLaunchService(IPromoLaunchRepository IPromoRepository)
        {
            iPromoLaunchRepository = IPromoRepository;
        }

        public List<CountryList> GetCountryList(string prefix)
        {
            return iPromoLaunchRepository.GetCountryList(prefix);
        }

        public List<ProductCodeList> GetProductCodeInfo(string prefix)
        {
            return iPromoLaunchRepository.GetProductCodeInfo(prefix);
        }

        public List<PricePlanInfo> GetPricePlanInfo(string prefix)
        {
            return iPromoLaunchRepository.GetPricePlanInfo(prefix);
        }

        public List<AcitvePromoInfo> GetActivePromoInfo(SearchPromo searchPromo)
        {
            return iPromoLaunchRepository.GetActivePromoInfo(searchPromo);
        }

        public APIResponse AddNewPromotion(AddNewPromoInfo addNewPromoInfo)
        {
            return iPromoLaunchRepository.AddNewPromotion(addNewPromoInfo);
        }

        public APIResponse UpdatePromoInfo(UpdatePromoInfo updatePromoInfo)
        {
            return iPromoLaunchRepository.UpdatePromoInfo(updatePromoInfo);
        }

        public SelectedPromoInfo GetSelectedPromo(int priceSchemeId)
        {
            return iPromoLaunchRepository.GetSelectedPromo(priceSchemeId);
        }

        public APIResponse AddCombotPromo(AddComboProduct updatePromoInfo)
        {
            return iPromoLaunchRepository.AddCombotPromo(updatePromoInfo);
        }
    }
}
