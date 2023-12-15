using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quotation.Application.Interfaces;
using Quotation.Application.Models;
using Quotation.Domain.Consts;
using Quotation.Domain.Enums;
using Quotation.Infrastructure.Data;
using Quotation.Infrastructure.Extensions;
using Quotation.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Quotation.Infrastructure.Identity
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public UserAuthenticationService(UserManager<ApplicationUser> userManager, ApplicationDbContext db, ITokenService tokenService, IConfiguration configuration) =>
            (_userManager, _db, _tokenService, _configuration) = (userManager, db, tokenService, configuration);

        public async Task<IBaseStatus> LoginAsync(AuthRequest request)
        {
            ApplicationUser managedUser = await _userManager.FindByEmailAsync(request.Email);

            if (managedUser == null)
            {
                return new StatusResponse()
                {
                    StatusCode = (int)ErrorCode.BadCredentials,
                    Message = "Bad Credentials"
                };
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

            if (!isPasswordValid)
            {
                return new StatusResponse()
                {
                    StatusCode = (int)ErrorCode.BadCredentials,
                    Message = "Bad Credentials"
                };
            }

            ApplicationUser user = _db.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user is null)
            {
                return new StatusResponse()
                {
                    StatusCode = (int)ErrorCode.Unauthorized,
                    Message = ErrorCode.Unauthorized.ToString()
                };
            }

            List<long> roleIds = await _db.UserRoles.Where(r => r.UserId == user.Id).Select(x => x.RoleId).ToListAsync();
            List<IdentityRole<long>> roles = _db.Roles.Where(x => roleIds.Contains(x.Id)).ToList();

            string accessToken = _tokenService.CreateToken(user, roles);
            user.RefreshToken = _configuration.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());

            await _db.SaveChangesAsync();

            return new AuthResponse()
            {
                Username = user.UserName!,
                Email = user.Email!,
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken,
                StatusCode = (int)ErrorCode.OK,
                Message = ErrorCode.OK.ToString()
            };
        }

        public async Task<IBaseStatus> RegisterAsync(RegisterRequest request)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new StatusResponse()
                {
                    StatusCode = (int)ErrorCode.BadCredentials,
                    Message = "Bad Credentials"
                };
            }

            ApplicationUser findUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (findUser == null)
            {
                throw new Exception($"User {request.Email} not found");
            }

            await _userManager.AddToRoleAsync(findUser, RoleConst.Member);

            return new StatusResponse()
            {
                StatusCode = (int)ErrorCode.OK,
                Message = ErrorCode.OK.ToString()
            };
        }

        public async Task<IBaseStatus> RefreshToken(TokenModel model)
        {
            if (model is null)
            {
                return new StatusResponse()
                {
                    StatusCode = (int)ErrorCode.BadCredentials,
                    Message = "Invalid client request"
                };
            }

            string accessToken = model.AccessToken;
            string refreshToken = model.RefreshToken;

            ClaimsPrincipal principal = _configuration.GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                return new StatusResponse()
                {
                    StatusCode = (int)ErrorCode.BadCredentials,
                    Message = "Invalid access token or refresh token"
                };
            }

            string username = principal.Identity!.Name;
            ApplicationUser user = await _userManager.FindByNameAsync(username!);

            if (user == null || user.RefreshToken != refreshToken/* || user.RefreshTokenExpiryTime <= DateTime.UtcNow*/)
            {
                return new StatusResponse()
                {
                    StatusCode = (int)ErrorCode.BadCredentials,
                    Message = ErrorCode.BadCredentials.ToString()
                };
            }

            JwtSecurityToken newAccessToken = _configuration.CreateToken(principal.Claims.ToList());
            string newRefreshToken = _configuration.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            //return new ResultResponse()
            //{
            //    AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            //    RefreshToken = newRefreshToken
            //};

            return new AuthResponse()
            {
                Username = user.UserName,
                Email = user.Email,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                StatusCode = (int)ErrorCode.OK,
                Message = ErrorCode.OK.ToString()
            };
        }
    }
}
