using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sana.Sim.Mvc.Helpers
{
    public class ExecutionContextKeyHelper
    {
        private static readonly string ExecutionContextStorageKey = "ExecutionContextId";

        private readonly IHttpContextAccessor contextAccessor;

        public ExecutionContextKeyHelper(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public Guid? GetExecutionContextId()
        {
            var value = contextAccessor.HttpContext.Request.Cookies[ExecutionContextStorageKey];
            Guid executionContextId;
            if (Guid.TryParse(value, out executionContextId))
                return executionContextId;
            return null;
        }

        public void SetExecutionContextId(Guid id)
        {
            contextAccessor.HttpContext.Response.Cookies.Append(ExecutionContextStorageKey, id.ToString());
        }

        public void ClearExecutionContextId()
        {
            contextAccessor.HttpContext.Response.Cookies.Delete(ExecutionContextStorageKey);
        }
    }
}
