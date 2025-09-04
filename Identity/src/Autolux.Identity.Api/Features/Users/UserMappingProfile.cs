using Autolux.Identity.Domain.Users;
using Autolux.Identity.Models.Users;
using AutoMapper;

namespace Autolux.Identity.Api.Features.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}
