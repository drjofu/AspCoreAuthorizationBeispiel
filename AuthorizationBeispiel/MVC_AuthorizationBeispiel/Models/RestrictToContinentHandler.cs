using Microsoft.AspNetCore.Authorization;
using Mondial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_AuthorizationBeispiel.Models
{
  public class RestrictToContinentRequirement : IAuthorizationRequirement { }
  public class RestrictToContinentHandler : AuthorizationHandler<RestrictToContinentRequirement, Continent>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RestrictToContinentRequirement requirement, Continent resource)
    {
      if (!context.User.HasClaim(c => c.Type == "RestrictToContinent"))
        context.Succeed(requirement);
      else
      {
        if (context.User.HasClaim("RestrictToContinent", resource.Id))
          context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
}
