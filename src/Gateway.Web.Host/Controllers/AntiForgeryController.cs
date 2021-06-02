using Microsoft.AspNetCore.Antiforgery;
using Gateway.Controllers;

namespace Gateway.Web.Host.Controllers
{
    public class AntiForgeryController : GatewayControllerBase
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
