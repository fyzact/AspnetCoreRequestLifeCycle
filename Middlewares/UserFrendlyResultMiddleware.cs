using AspNetCoreRequesLifeCycle.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCoreRequesLifeCycle.Middlewares
{
    public class UserFrendlyResultMiddleware
    {
        RequestDelegate _next;
        ILogger<UserFrendlyResultMiddleware> _logger;

        public UserFrendlyResultMiddleware(RequestDelegate next,ILogger<UserFrendlyResultMiddleware> logger )
        {
            _next = next;
            _logger = logger;
            _logger.LogInformation("UserFrendlyResultMiddleware added pipeline");
        }

        public  async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
               await WriteUserFriendlyExceptionAsync(httpContext, "An unexpected error has occurred. Please try again later.");
            }
           
        }

        private Task WriteUserFriendlyExceptionAsync(HttpContext context, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ApiResponse(message)));
        }
    }
}
