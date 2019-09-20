using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackOPS.Models;
using BlackOPS.Models.PromoLaunch;
using BlackOPS.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlackOPS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class HomeController : Controller
    {
        private IOptions<ConfigurationManager> settings;
        private PromoLaunchService promoLaunchService = null;

        public HomeController(IOptions<ConfigurationManager> settings)
        {
            this.settings = settings;
            promoLaunchService = new PromoLaunchService(this.settings);
        }

     
        public LoginModel Login()
        {
            LoginModel loginModel = new LoginModel();
            return loginModel;
        }

        [HttpPost("GetCountryList")]
        public ActionResult GetCountryList([FromBody] ProductSearch searchInfo)
        {
            return Json(promoLaunchService.GetCountryList(searchInfo.Prefix));
        }

        [HttpPost("GetProducts")]
        public ActionResult GetProductCodeInfo([FromBody] ProductSearch prefix)
        {
            return Json(promoLaunchService.GetProductCodeInfo(prefix.Prefix));
        }

        [HttpPost("GetPricePlanInfo")]
        public ActionResult GetPricePlanInfo(string prefix)
        {
            return Json(promoLaunchService.GetPricePlanInfo(prefix));
        }

        [HttpPost("GetActivePromo")]
        public ActionResult GetActivePromoInfo([FromBody] SearchPromo searchPromo)
        {
            return Json(promoLaunchService.GetActivePromoInfo(searchPromo));
        }

        [HttpPost("AddNewPromo")]
        public ActionResult AddNewPromo([FromBody] AddNewPromoInfo addNewPromo)
        {
            return Json(promoLaunchService.AddNewPromotion(addNewPromo));
        }
    }

   
}