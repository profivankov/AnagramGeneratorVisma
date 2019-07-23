using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.EF.CodeFirst.Contracts;

namespace AnagramSolver.WebApp.Services
{
    public class HttpManagerService : IHttpManagerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpManagerService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetIP()
        {
            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
