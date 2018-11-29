using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_AuthorizationBeispiel.Models
{
  public class IsExpertHandler : AuthorizationHandler<IsExperiencedRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsExperiencedRequirement requirement)
    {
      if (context.User.HasClaim("IsExpert", "True"))
        context.Succeed(requirement);
      return Task.CompletedTask;
    }
  }
}
