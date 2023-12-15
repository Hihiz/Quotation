using Microsoft.AspNetCore.Identity;

namespace Quotation.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
