using Microsoft.AspNetCore.Http;
using Sana.Sim.Business;
using Sana.Sim.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Mvc.Middlewares
{
    public class ExecutionContextInitializerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExecutionContextInitializerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ExecutionContext executionContext, ExecutionContextKeyHelper keyHelper)
        {
            var projectId = keyHelper.GetExecutionContextId();
            if (projectId.HasValue)
            {
                var project = Framework.Projects.Get(projectId.Value);
                if (project != null)
                {
                    executionContext.ProjectId = projectId;
                    executionContext.Project = project;
                }
            }

            await _next.Invoke(context);
        }
    }
}
