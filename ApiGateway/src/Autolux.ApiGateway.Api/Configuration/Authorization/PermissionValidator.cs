using Autolux.ApiGateway.Api.Models.Identity;
using Autolux.Identity.Models.Roles;
using System.Collections.Concurrent;

namespace Autolux.ApiGateway.Api.Configuration.Authorization;

public class PermissionValidator : IPermissionValidator
{
    public static PermissionValidator Instance { get; } = new PermissionValidator();
    private PermissionValidator() { }

    private readonly ConcurrentDictionary<string, List<int>> _roles = new ConcurrentDictionary<string, List<int>>();

    public bool ValidateForRoles(int requiredPermissionKey, string[] roles)
    {
        foreach (var role in roles)
        {
            if (_roles.TryGetValue(role, out var permissionKeys))
            {
                if (permissionKeys.Contains(requiredPermissionKey))
                {
                    return true;
                }
            }
        }

        return false;
    }
    public bool ValidateForClaims(int requiredPermissionKey, string[] claims)
    {
        return claims.Contains(requiredPermissionKey.ToString());
    }
    public void UpdateRoles(List<RoleDto> roles)
    {
        _roles.Clear();

        foreach (var role in roles)
        {
            // Do not use AddOrUpdate and GetOrAdd methods. Delegates for these methods are called outside the locks and are not thread safe.

            _roles.TryAdd(role.Name, role.Permissions.Select(x => x.PermissionId).ToList());
        }
    }
    public void UpdateRoles(List<RoleModel> roles)
    {
        _roles.Clear();

        foreach (var role in roles)
        {
            // Do not use AddOrUpdate and GetOrAdd methods. Delegates for these methods are called outside the locks and are not thread safe.

            _roles.TryAdd(role.Name, role.Permissions.Where(x => x.Value == true).Select(x => x.Id).ToList());
        }
    }
}