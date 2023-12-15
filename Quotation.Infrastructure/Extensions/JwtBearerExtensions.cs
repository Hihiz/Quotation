using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quotation.Infrastructure.Identity;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Quotation.Infrastructure.Extensions
{
    public static class JwtBearerExtensions
    {
        public static List<Claim> CreateClaims(this ApplicationUser user, List<IdentityRole<long>> roles)
        {
            List<Claim> claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, string.Join(" ", roles.Select(x => x.Name)))
            };

            return claims;
        }

        public static SigningCredentials CreateSigningCredentials(this IConfiguration configuration)
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)
                    ),
                SecurityAlgorithms.HmacSha256
                );
        }

        public static JwtSecurityToken CreateJwtToken(this IEnumerable<Claim> claims, IConfiguration configuration)
        {
            int expire = configuration.GetSection("Jwt:Expire").Get<int>();

            return new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(expire),
                signingCredentials: configuration.CreateSigningCredentials()
                );
        }

        public static JwtSecurityToken CreateToken(this IConfiguration configuration, List<Claim> authClaims)
        {
            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!));
            int tokenValidityInMinutes = configuration.GetSection("Jwt:TokenValidityInMinutes").Get<int>();

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public static string GenerateRefreshToken(this IConfiguration configuration)
        {
            byte[] randomNumber = new byte[64];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public static ClaimsPrincipal? GetPrincipalFromExpiredToken(this IConfiguration configuration, string? token)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
