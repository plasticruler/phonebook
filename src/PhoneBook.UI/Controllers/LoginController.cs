using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhoneBook.UI.Infrastructure;
using PhoneBook.UI.Infrastructure.Messager;
using PhoneBook.UI.Models;
using PhoneBook.UI.Models.DTO;
using PhoneBook.UI.Utilities;

namespace PhoneBook.UI.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IPhoneBookRepository _repository;

        public LoginController(IPhoneBookRepository repository, IConfiguration configuration, 
            IMessager messager, IHttpContextAccessor contextAccessor) :base(configuration, messager, contextAccessor)
        {
            _repository = repository;         
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel, string returnUrl="/")
        {         
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }
            
            var authenticatedUser = _repository.LoginUser(loginModel.EmailAddress, PasswordFunctions.GetSHA256(loginModel.Password));
            if (authenticatedUser!=null && !string.IsNullOrWhiteSpace(authenticatedUser.JsonToken))
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, loginModel.EmailAddress));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, authenticatedUser.FirstName));
                identity.AddClaim(new Claim(ClaimTypes.Sid, authenticatedUser.JsonToken));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, authenticatedUser.Id.ToString()));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("Authenitcation Error", "Password or Email address is invalid.");
                return View();
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(UserController.Index), "User");

        }

    }
}