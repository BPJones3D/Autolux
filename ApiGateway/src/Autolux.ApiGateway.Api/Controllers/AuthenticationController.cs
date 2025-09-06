using Autolux.Identity.Api.Features.Users;
using Autolux.Identity.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autolux.ApiGateway.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IUserService userService) : ControllerBase
{
    ///// <summary>
    ///// Authenticate and generate a new JWT
    ///// </summary>
    ///// <returns>JWT</returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTokenAsync([FromBody] AuthenticationModel authenticationModel, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (authenticationModel == null) return BadRequest($"{nameof(authenticationModel)} is required");

        var tokenString = await userService.Authenticate(authenticationModel.Username, authenticationModel.Password, ApiGatewaySettings.Instance.JWTEncryptionKey, cancellationToken);

        return Ok(new { Token = tokenString });
    }
}
