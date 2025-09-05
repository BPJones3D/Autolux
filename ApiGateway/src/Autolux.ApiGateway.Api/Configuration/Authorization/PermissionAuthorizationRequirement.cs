using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Autolux.ApiGateway.Api.Configuration.Authorization;

public class PermissionAuthorizationRequirement : AuthorizationHandler<PermissionAuthorizationRequirement>, IAuthorizationRequirement
{
    public int RequiredPermissionKey { get; }

    public PermissionAuthorizationRequirement(int requiredPermissionKey)
    {
        RequiredPermissionKey = requiredPermissionKey;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
    {
        if (context.User is not null)
        {
            var rolesClaim = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Role) || c.Type.Equals(ClaimTypes.NameIdentifier)).ToList();

            if (rolesClaim is not null)
            {
                var roles = rolesClaim.Select(c => c.Value).ToArray();

                if (PermissionValidator.Instance.ValidateForClaims(RequiredPermissionKey, roles))
                {
                    context.Succeed(requirement);
                }
            }
        }

        return Task.CompletedTask;
    }
}