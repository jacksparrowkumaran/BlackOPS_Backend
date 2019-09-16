using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackOPS.Models;
using BlackOPS.Models.PromoLaunch;
using BlackOPS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlackOPS.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private IOptions<ConfigurationManager> settings;
        private PromoLaunchService promoLaunchService = null;

        public HomeController(IOptions<ConfigurationManager> settings)
        {
            this.settings = settings;
        }

     
        public LoginModel Login()
        {
            LoginModel loginModel = new LoginModel();
            return loginModel;
        }

        [HttpGet("GetCountryList/{id}")]
        public ActionResult GetCountryList(string prefix)
        {
            promoLaunchService = new PromoLaunchService(this.settings);
            return Json(promoLaunchService.GetCountryList(prefix));
        }

        [HttpPost("GetProducts")]
        public ActionResult GetProductCodeInfo([FromBody] ProductSearch prefix)
        {
            promoLaunchService = new PromoLaunchService(this.settings);
            return Json(promoLaunchService.GetProductCodeInfo(prefix.Prefix));
        }

        [HttpPost("GetPricePlanInfo")]
        public ActionResult GetPricePlanInfo(string prefix)
        {
            promoLaunchService = new PromoLaunchService(this.settings);
            return Json(promoLaunchService.GetPricePlanInfo(prefix));
        }

        [HttpPost("GetActivePromo")]
        public ActionResult GetActivePromoInfo(SearchPromo searchPromo)
        {
            promoLaunchService = new PromoLaunchService(this.settings);
            return Json(promoLaunchService.GetActivePromoInfo(searchPromo));
        }
    }

   
}