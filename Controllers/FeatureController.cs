using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRequesLifeCycle.Contracts;
using AspNetCoreRequesLifeCycle.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AspNetCoreRequesLifeCycle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        public IActionResult Get()
        {
            throw new Exception("Unhandled Error.");

        }

        [Route("/api/[controller]/[action]", Name = "Email")]
        public IActionResult Email()
        {
            return Ok("Email Module is active.");
        }

        [Route("/api/[controller]/[action]", Name = "Sms")]
        public IActionResult Sms()
        {
            return Ok("Sms Module inactive, but you won't see this message.");
        }

        [MobileRedirectFilter(Controller = "Features", Action = "MobileInfo")]
        [Route("/info",Name ="info")]
        public IActionResult Info()
        {
            return Ok("Web Request");
        }
        [Route("/MobileInfo", Name = "MobileInfo")]
        public IActionResult MobileInfo()
        {
            return Ok("Mobile Request");
        }

        [Route("/products", Name = "Products")]
        [HttpPost]
        public IActionResult Post(List<Product> orders)
        {
            return Ok(orders);
        }


    }



}
