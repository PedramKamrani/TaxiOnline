using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Snap.Core.Interface;
using Snap.Core.ViewModels;
using Snap.Data.Layer.Entities;

namespace Snap.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
       

        public AccountController(IAccountService service)
        {
            _service = service;
           
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                User? use = await _service.RegisterAsync(registerViewModel);
                if (use != null)
                {
                    return RedirectToAction("Active",new { userName=registerViewModel.Username });
                }
            }
            return View(registerViewModel);
        }
        [HttpGet]
        public IActionResult Driver()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Driver(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                User? use = await _service.RegisterDriverAsync(registerViewModel);
                if (use != null)
                {
                    return RedirectToAction("Active", new { userName = registerViewModel.Username });
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Active(string userName)
        {
            ViewBag.IsError = false;
            ActiveCodeViewModel viewModel=new ActiveCodeViewModel
            {
                UserName = userName
            };
           return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Active(ActiveCodeViewModel codeViewModel)
        {
            var user =await _service.ActiveUser(codeViewModel);
            if (user != null)
            {
                ViewBag.IsError = false;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    
                };
                await HttpContext.SignInAsync(principal, properties);
                return RedirectToAction("Dashboard","Panel");
            }
            ViewBag.IsError = true;
            return View();
        }


    }
}
