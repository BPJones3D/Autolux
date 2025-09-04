using Autolux.Identity.Api.Features.Users;
using Autolux.Identity.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Autolux.ApiGateway.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Authenticate and generate a new JWT
    /// </summary>
    /// <returns>JWT</returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTokenAsync([FromBody] AuthenticationModel authenticationModel, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (authenticationModel == null) return BadRequest($"{nameof(authenticationModel)} is required");

        var user = await userService.Authenticate(authenticationModel.Username, authenticationModel.Password, cancellationToken);
        if (!user.IsAuthenticated)
            return Unauthorized();

        var roles = new List<Claim>();
        var claims = new List<Claim>();

        foreach (var role in user.Roles)
        {
            roles.Add(new Claim(ClaimTypes.Role, role.Name));
            foreach (var permission in role.Permissions)
            {
                if (!claims.Any(c => c.Value == $"{permission.PermissionId}"))
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, $"{permission.PermissionId}"));
            }
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(ApiGatewaySettings.Instance.JWTEncryptionKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(roles.Concat(claims).AsEnumerable()),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }
}
