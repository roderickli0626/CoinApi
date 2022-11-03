using CoinApi.Request_Models;
using CoinApi.Response_Models;

namespace CoinApi.Services.AuthService
{
    public interface IAuthService
    {
        AuthenticatedResponse? Login(LoginModel model);
        AuthenticatedResponse? Register(RegisterModel model);
    }
}
