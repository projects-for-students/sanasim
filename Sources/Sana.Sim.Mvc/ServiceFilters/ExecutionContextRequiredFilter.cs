using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sana.Sim.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Mvc.ServiceFilters
{
    public class ExecutionContextRequiredFilter : ActionFilterAttribute
    {
        private ExecutionContext ExecutionContext { get; set; }

        public ExecutionContextRequiredFilter(ExecutionContext executionContext)
        {
            this.ExecutionContext = executionContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!ExecutionContext.Exists)
                context.Result = new RedirectResult("/");

            base.OnActionExecuting(context);
        }
    }
}
