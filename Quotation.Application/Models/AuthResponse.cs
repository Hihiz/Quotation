using Quotation.Application.Interfaces;

namespace Quotation.Application.Models
{
    public class AuthResponse : IBaseStatus
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
