using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sana.Sim.Business;
using Sana.Sim.Mvc.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Mvc.Extensions
{
    public static  class IoCApplicationBuilderExtensions
    {
        public static void UseIoC(this IApplicationBuilder app)
        {
            app.UseMiddleware<IoCInitializerMiddleware>();

            //We need initial initialization of IoC to be able to use IoC during startup stage
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                IoC.Init(serviceScope.ServiceProvider, null);
            }
        }
    }
}
