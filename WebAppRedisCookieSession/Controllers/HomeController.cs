using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebAppRedisCookieSession.Models;

namespace WebAppRedisCookieSession.Controllers
{    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            _logger.LogInformation("Pagina Home");
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            _logger.LogInformation("Pagina Privacidade");
            return View();
        }
       
        [Authorize(Roles = "Suporte_0000")]
        public IActionResult Suporte0()
        {
            return View();
        }

        [Authorize(Roles = "Suporte_0001")]
        public IActionResult Suporte1()
        {
            return View();
        }

        [Authorize(Roles = "Suporte_0002")]
        public IActionResult Suporte2()
        {
            return View();
        }
    }
}
