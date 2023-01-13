using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Shared;

namespace CoinApi.Services.AuthService
{
    public interface IAuthService
    {
        Task<AuthenticatedResponse?> Login(LoginModel model);
        Task<ApiResponse> CheckLogin(LoginModel model);
        Task<AuthenticatedResponse?> Register(RegisterModel model);
        Task<ApiResponse> SendResetPasswordRequest(string email);
        
    }
}
