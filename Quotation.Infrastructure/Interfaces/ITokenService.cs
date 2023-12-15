using Microsoft.AspNetCore.Identity;
using Quotation.Infrastructure.Identity;

namespace Quotation.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// Создание токена
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        string CreateToken(ApplicationUser user, List<IdentityRole<long>> role);
    }
}
