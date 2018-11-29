using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVC_AuthorizationBeispiel.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC_AuthorizationBeispiel.Controllers
{
  [AllowAnonymous]
  public class AccountController : Controller
  {
    private readonly UserRepo userRepo;

    public AccountController(UserRepo userRepo)
    {
      this.userRepo = userRepo;
    }

    public IActionResult Login(string returnUrl = null)
    {
      return View(new LoginModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
      var user = userRepo.Users.FirstOrDefault(u => u.Name.ToLower() == loginModel.Username.ToLower());
      if (user == null) return View();

      const string Issuer = "https://mondial.com";
      var claims = new List<Claim>();
      claims.Add(new Claim(ClaimTypes.Name, user.Name, ClaimValueTypes.String, Issuer));
      foreach (var role in user.Roles)
      {
        claims.Add(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String, Issuer));
      }

      if (user.HasChildren != null)
        claims.Add(new Claim("HasChildren", user.HasChildren.Value.ToString()));

      string dob = user.DateOfBirth.ToString(CultureInfo.InvariantCulture);
      claims.Add(new Claim(ClaimTypes.DateOfBirth, dob, ClaimValueTypes.Date, Issuer));

      var userIdentity = new ClaimsIdentity("SuperSecureLogin");
      userIdentity.AddClaims(claims);
      var userPrincipal = new ClaimsPrincipal(userIdentity);

      await HttpContext.SignInAsync(
          CookieAuthenticationDefaults.AuthenticationScheme,
          userPrincipal,
                new AuthenticationProperties
                {
                  ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                  IsPersistent = false,
                  AllowRefresh = false
                });

      if (Url.IsLocalUrl(loginModel.ReturnUrl))
      {
        return Redirect(loginModel.ReturnUrl);
      }

      return RedirectToAction("Index", "Home");


    }

    public async Task<IActionResult>LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }



    public IActionResult Forbidden()
    {
      return View();
    }
  }
}

