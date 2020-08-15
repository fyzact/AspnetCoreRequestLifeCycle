using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreRequesLifeCycle.Filters
{

    public class MobileRedirectFilter : ActionFilterAttribute
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Headers.Keys.Contains("x-mobile"))
            {
                context.Result = new RedirectToActionResult(Action, Controller, context.RouteData, true);
            }
        }
    }


}
