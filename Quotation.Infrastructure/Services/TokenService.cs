using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Quotation.Infrastructure.Extensions;
using Quotation.Infrastructure.Identity;
using Quotation.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Quotation.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration) => _configuration = configuration;

        public string CreateToken(ApplicationUser user, List<IdentityRole<long>> roles)
        {
            JwtSecurityToken token = user.CreateClaims(roles).CreateJwtToken(_configuration);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
