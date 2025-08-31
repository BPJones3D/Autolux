using Autolux.Identity.Domain.Entities;
using Autolux.Identity.Models.Roles;
using Autolux.Identity.Models.Users;
using AutoMapper;

namespace Autolux.Identity.Api.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<Role, RoleNameDto>(MemberList.Destination);

        CreateMap<CreateUserDto, User>()
            .ConvertUsing((source, dest) =>
            {
                if (dest is not null)
                {
                    throw new ArgumentNullException(nameof(dest), "No existing object should be provided.");
                }

                return new User(source.Email, source.FirstName, source.LastName, source.IsStaff);
            });

        CreateMap<UpdateUserDto, User>()
            .ConvertUsing((source, dest) =>
            {
                if (dest is User user)
                {
                    user.Update(source.FirstName, source.LastName, source.IsStaff);
                    return user;
                }

                throw new ArgumentNullException(nameof(dest), "No existing object is provided.");
            });
    }
}