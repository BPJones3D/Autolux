using Autolux.SharedKernel.SharedObjects;
using Microsoft.AspNetCore.Authorization;

namespace Autolux.ApiGateway.Api.Configuration.Authorization;

public static class OAuthConfiguration
{
    public const string ScopeAccessAsUser = "access_as_user";

    public static void AddPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            foreach (var permissionKey in PermissionKey.List)
            {
                options.AddPolicy(permissionKey.Name, policy => policy.RequirePermission(permissionKey.Value));
            }
        });
    }
}
