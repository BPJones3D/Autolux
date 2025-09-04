using Autolux.Identity.Domain.Permissions;
using Autolux.Identity.Domain.Roles;
using Autolux.Identity.Models.Permissions;
using Autolux.Identity.Models.Roles;
using AutoMapper;

namespace Autolux.Identity.Api.Roles;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<Permission, PermissionDto>();
        CreateMap<RolePermission, RolePermissionDto>();

        CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.RolePermissions.Select(p => new RolePermissionDto
            {
                PermissionId = p.Permission.Key.Value
            }).ToList()));
    }
}