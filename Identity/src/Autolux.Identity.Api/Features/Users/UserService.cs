using Autolux.Identity.Infrastructure.Authentication;
using Autolux.Identity.Infrastructure.Respositories.Users;
using Autolux.Identity.Models.Roles;
using Autolux.Identity.Models.Users;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Autolux.Identity.Api.Features.Users;

public class UserService(IMapper mapper, IUserRepository userRepository, IPasswordHasher passwordHasher) : IUserService
{
    public async Task<string> Authenticate(string username, string password, string jwtEncryptionKey, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(username, cancellationToken);
        if (user == null || !passwordHasher.Verify(password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid username or password.");

        var userDto = mapper.Map<UserDto>(user);
        if (!userDto.IsAuthenticated)
            throw new UnauthorizedAccessException("Invalid username or password.");

        var roles = new List<Claim>();
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, $"{user.Id}"),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };

        foreach (var role in user.Roles)
        {
            var roleDto = mapper.Map<RoleDto>(role);
            roles.Add(new Claim(ClaimTypes.Role, roleDto.Name));
            claims.AddRange(from permission in roleDto.Permissions
                            where !claims.Any(c => c.Value == $"{permission.PermissionId}")
                            select new Claim(ClaimTypes.NameIdentifier, $"{permission.PermissionId}"));
        }
        Claim[] allClaims = [.. roles, .. claims];

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtEncryptionKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(allClaims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}
