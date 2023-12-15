using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quotation.Application.Interfaces;
using Quotation.Application.Models;

namespace Quotation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAuthenticationService _authService;

        public AccountController(IUserAuthenticationService authService) => _authService = authService;

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest request) => Ok(await _authService.RegisterAsync(request));

        [HttpPost("login")]
        public async Task<ActionResult> Authenticate(AuthRequest request) => Ok(await _authService.LoginAsync(request));

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken(TokenModel tokenModel) => Ok(await _authService.RefreshToken(tokenModel));
    }
}
