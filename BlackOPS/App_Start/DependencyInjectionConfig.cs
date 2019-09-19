using BlackOPS.Interface.Promotion.Repositories;
using BlackOPS.Interface.Promotion.Services;
using BlackOPS.Repository;
using BlackOPS.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackOPS.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void AddScope(IServiceCollection services)

        {
            services.AddScoped<IPromoLaunchService, PromoLaunchService>();
            services.AddScoped<IPromoLaunchRepository, PromoLaunchRepository>();

        }
    }
}
