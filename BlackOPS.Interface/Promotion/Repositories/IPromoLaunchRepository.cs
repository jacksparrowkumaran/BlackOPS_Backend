using BlackOPS.Models;
using BlackOPS.Models.PromoLaunch;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackOPS.Interface.Promotion.Repositories
{
    public interface IPromoLaunchRepository
    {
        List<CountryList> GetCountryList(string prefix);
        List<ProductCodeList> GetProductCodeInfo(string prefix);
        List<PricePlanInfo> GetPricePlanInfo(string prefix);
        List<AcitvePromoInfo> GetActivePromoInfo(SearchPromo searchPromo);
        APIResponse AddNewPromotion(AddNewPromoInfo addNewPromoInfo);

        APIResponse UpdatePromoInfo(UpdatePromoInfo updatePromoInfo);

        SelectedPromoInfo GetSelectedPromo(int schemeId);

    }
}
