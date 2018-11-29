using Microsoft.AspNetCore.Authorization;
using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC_AuthorizationBeispiel.Models
{
  public class BornBefore1970Requirement : IAuthorizationRequirement { }
  public class BornBefore1970Handler : AuthorizationHandler<BornBefore1970Requirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BornBefore1970Requirement requirement)
    {
      if (context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
      {
        var dateOfBirth = DateTime.Parse(context.User.FindFirstValue(ClaimTypes.DateOfBirth), CultureInfo.InvariantCulture);
        if (dateOfBirth < new DateTime(1970, 1, 1))
          context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
}
