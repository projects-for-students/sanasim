using Microsoft.AspNetCore.Http;
using Sana.Sim.Business;
using Sana.Sim.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Mvc.Middlewares
{
    public class IoCInitializerMiddleware
    {
        private readonly RequestDelegate _next;

        public IoCInitializerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            IoC.Init(context.RequestServices, context.Session);

            await _next.Invoke(context);
        }
    }
}
