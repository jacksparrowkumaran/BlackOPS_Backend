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

        SelectedPromoInfo GetSelectedPromo(ComboSearchInfo searchPromo);

        APIResponse AddCombotPromo(AddComboPromoInfo updatePromoInfo);
        APIResponse ValidatePromoForCombo(AddComboPromoInfo addComboPromoInfo);
        ActiveComborPromo GetComboPromoInfo(ComboSearchInfo searchPromo);
        APIResponse UpdateCombotPromo(AddComboPromoInfo addComboPromo);

    }
}
