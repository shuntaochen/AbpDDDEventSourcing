using Microsoft.AspNetCore.Antiforgery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Query.WebApi.Controllers
{
    public class AntiForgeryController : QueryControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
