using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mondial;
using MVC_AuthorizationBeispiel.Models;

namespace MVC_AuthorizationBeispiel.Controllers
{
  //[Authorize(Roles ="Admin")]
  //[Authorize(Policy = "AdministratorOnly")]
  //[Authorize(Policy = "HasChildrenSet")]
  [Authorize(Policy = "BornBefore1970")]

  public class HomeController : Controller
  {
    private readonly World world;

    public HomeController(World world)
    {
      this.world = world;
    }

    public async Task< IActionResult> Index()
    {
      return View(world.GetContinents());
    }

    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }

    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
