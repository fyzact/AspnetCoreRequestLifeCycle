using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreRequesLifeCycle.Contracts
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public ApiResponse(string message)
        {
            Message = message;
        }
    }
}
