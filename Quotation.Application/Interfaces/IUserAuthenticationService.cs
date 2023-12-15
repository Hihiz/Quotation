using Quotation.Application.Models;

namespace Quotation.Application.Interfaces
{
    public interface IUserAuthenticationService
    {
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IBaseStatus> LoginAsync(AuthRequest request);

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IBaseStatus> RegisterAsync(RegisterRequest request);

        /// <summary>
        /// Обновить токен пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IBaseStatus> RefreshToken(TokenModel model);
    }
}
