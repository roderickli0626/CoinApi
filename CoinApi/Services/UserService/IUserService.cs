using CoinApi.DB_Models;

namespace CoinApi.Services.UserService
{
    public interface IUserService : IService<tblUser>
    {
        tblUser GetByEmail(string email);
    }
}
