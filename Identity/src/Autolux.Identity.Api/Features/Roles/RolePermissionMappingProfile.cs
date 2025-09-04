using Autolux.Identity.Domain.Roles;
using Autolux.Identity.Models.Roles;
using AutoMapper;

namespace Autolux.Identity.Api.Features.Roles;

public class RolePermissionMappingProfile : Profile
{
    public RolePermissionMappingProfile()
    {
        CreateMap<RolePermission, RolePermissionDto>();
    }
}
