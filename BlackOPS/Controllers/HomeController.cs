using BlackOPS.Interface.Promotion.Services;
using BlackOPS.Models;
using BlackOPS.Models.PromoLaunch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlackOPS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class HomeController : Controller
    {
        private IPromoLaunchService iPromoLaunchService = null;

        public HomeController(IPromoLaunchService iPromoService)
        {
            iPromoLaunchService = iPromoService;
        }


        public LoginModel Login()
        {
            LoginModel loginModel = new LoginModel();
            return loginModel;
        }

        [HttpPost("GetCountryList")]
        public ActionResult GetCountryList([FromBody] ProductSearch searchInfo)
        {
            return Json(iPromoLaunchService.GetCountryList(searchInfo.Prefix));
        }

        [HttpPost("GetProducts")]
        public ActionResult GetProductCodeInfo([FromBody] ProductSearch prefix)
        {
            return Json(iPromoLaunchService.GetProductCodeInfo(prefix.Prefix));
        }

        [HttpPost("GetPricePlanInfo")]
        public ActionResult GetPricePlanInfo(string prefix)
        {
            return Json(iPromoLaunchService.GetPricePlanInfo(prefix));
        }

        [HttpPost("GetActivePromo")]
        public ActionResult GetActivePromoInfo([FromBody] SearchPromo searchPromo)
        {
            return Json(iPromoLaunchService.GetActivePromoInfo(searchPromo));
        }

        [HttpPost("AddNewPromo")]
        public ActionResult AddNewPromo([FromBody] AddNewPromoInfo addNewPromo)
        {
            return Json(iPromoLaunchService.AddNewPromotion(addNewPromo));
        }

        [HttpPost("PromoUpdate")]
        public ActionResult PromoUpdate([FromBody] UpdatePromoInfo updatePromo)
        {
            return Json(iPromoLaunchService.UpdatePromoInfo(updatePromo));
        }


        [HttpPost("GetSelectedPromo/{priceSchemeId}")]
        public ActionResult GetSelectedPromo(int priceSchemeId)
        {
            return Json(iPromoLaunchService.GetSelectedPromo(priceSchemeId));
        }

    }


}