using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Autolux.ApiGateway.Api.Configuration.Authorization;

public static class JwtAuthenticationConfiguration
{
    /// <summary>
    /// Read and decrypt the incoming jason web token
    /// </summary>
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));

        var jwtEncryptionKey = ApiGatewaySettings.Instance.JWTEncryptionKey;
        if (jwtEncryptionKey == null || jwtEncryptionKey.Length == 0)
            throw new InvalidOperationException("JWT Encryption Key is not configured.");

        var key = Encoding.ASCII.GetBytes(jwtEncryptionKey);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    // Validate the user
                    var name = context.Principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name);
                    if (name is null) context.Fail("UnAuthorized");

                    var roles = context.Principal.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                    if (roles.Count == 0) context.Fail("UnAuthorized");

                    // Set the user roles
                    var additionalClaims = new List<Claim>();
                    foreach (var role in roles)
                    {
                        additionalClaims.Add(new Claim("Role", role.Value));
                    }

                    var newIdentity = new ClaimsIdentity(context.Principal.Identity, additionalClaims, "pwd", "name", "role");
                    context.Principal = new ClaimsPrincipal(newIdentity);

                    return Task.CompletedTask;
                }
            };
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
}