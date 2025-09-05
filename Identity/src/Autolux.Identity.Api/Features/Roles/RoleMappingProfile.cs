using Autolux.Identity.Domain.Roles;
using Autolux.Identity.Models.Roles;
using AutoMapper;

namespace Autolux.Identity.Api.Features.Roles;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.RolePermissions.Select(p => new RolePermissionDto
            {
                RoleId = p.RoleId,
                PermissionId = p.Permission.Key.Value
            }).ToList()));
    }
}