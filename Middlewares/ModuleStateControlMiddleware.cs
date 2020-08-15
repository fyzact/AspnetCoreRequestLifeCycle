
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreRequesLifeCycle.Middlewares
{
    public class ModuleStateControlMiddleware
    {
        RequestDelegate _next;
        public ModuleStateControlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IConfiguration config)
        {

            RouteAttribute endpoint = httpContext.GetEndpoint()?.Metadata.GetMetadata<RouteAttribute>();

            if (endpoint is null)
            {

                await _next(httpContext);
                return;
            }

            var featureSection = $"Features:{endpoint.Name}";
            var feature = config[featureSection];

            if (feature is null)
            {
                await _next(httpContext);
                return;
            }
            var fetureEnabled = config.GetValue<bool>(featureSection);
            if (!fetureEnabled)
            {
                httpContext.SetEndpoint(new Endpoint(async (context) =>
                {
                    await Task.Run(() =>
                     {
                         context.Response.StatusCode = StatusCodes.Status404NotFound;
                     });
                   
                }, EndpointMetadataCollection.Empty, "Feature not faund"));
            }


            await _next(httpContext);

        }
    }
}
