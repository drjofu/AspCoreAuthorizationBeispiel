using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mondial;
using MVC_AuthorizationBeispiel.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_AuthorizationBeispiel.Controllers
{
  //[Authorize(Roles ="Admin")]
  //[Authorize(Policy = "AdministratorOnly")]
  //[Authorize(Policy = "HasChildrenSet")]
  [Authorize(Policy = "BornBefore1970")]

  public class HomeController : Controller
  {
    private readonly World world;
    private readonly IAuthorizationService authorizationService;

    public HomeController(World world, IAuthorizationService authorizationService)
    {
      this.world = world;
      this.authorizationService = authorizationService;
    }

    public IActionResult Index()
    {
      return View(world.GetContinents());
    }


    public async Task<IActionResult> Countries(string continentId)
    {
      var continent = world.GetContinents().FirstOrDefault(c => c.Id == continentId);

      var authorizationResult = await authorizationService.AuthorizeAsync(User, continent, new RestrictToContinentRequirement());
      if (authorizationResult.Succeeded)
        return View(world.GetCountriesByContinentId(continentId));

      return new ForbidResult();
    }
  }
}
