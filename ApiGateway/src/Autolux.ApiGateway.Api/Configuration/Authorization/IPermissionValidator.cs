using Autolux.ApiGateway.Api.Models.Identity;
using Autolux.Identity.Models.Roles;

namespace Autolux.ApiGateway.Api.Configuration.Authorization;

public interface IPermissionValidator
{
    void UpdateRoles(List<RoleModel> roles);
    void UpdateRoles(List<RoleDto> roles);
    bool ValidateForRoles(int requiredPermissionKey, string[] roles);
    bool ValidateForClaims(int requiredPermissionKey, string[] claims);
}