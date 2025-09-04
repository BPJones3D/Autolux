using Autolux.ApiGateway.Api.Models.Identity;

namespace Autolux.ApiGateway.Api.Configuration.Authorization;

public interface IPermissionValidator
{
    void UpdateRoles(List<RoleModel> roles);
    bool ValidateForRoles(int requiredPermissionKey, string[] roles);
}