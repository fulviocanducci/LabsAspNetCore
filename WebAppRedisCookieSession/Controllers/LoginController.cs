using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAppRedisCookieSession.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet()]
        [AllowAnonymous()]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        [AllowAnonymous()]
        public async Task<ActionResult> Index(string name)
        {
            var claims = new List<Claim>()
            {
                new Claim("FirstName", "Fúlvio"),
                new Claim("LastName", "C. C. Dias"),
                new Claim(ClaimTypes.GivenName, "Fúlvio C. C. Dias"),
                new Claim(ClaimTypes.Name, "fulviocanducci@hotmail.com"),
                new Claim(ClaimTypes.Email, "fulviocanducci@hotmail.com"),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString("N")),
                new Claim(ClaimTypes.IsPersistent, "false"),
            };
            for(int i = 0; i < 100; i++)
            {
                claims.Add(new Claim(ClaimTypes.Role, $"Suporte_{i:0000}"));
            }
            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties authProperties = new()
            {
                AllowRefresh = true,
                IsPersistent = false,
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            return RedirectToAction("Index", "Home");
        }

        [Authorize()]
        [HttpGet()]
        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
