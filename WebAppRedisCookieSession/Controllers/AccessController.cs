using Microsoft.AspNetCore.Mvc;

namespace WebAppRedisCookieSession.Controllers
{    
    public class AccessController : Controller
    {
        public IActionResult Denied()
        {
            return View();
        }
    }
}
