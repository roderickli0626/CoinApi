using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CoinApi.Services.UserService
{
    public interface IUserService : IService<tblUser>
    {
        Task<tblUser> GetByEmailAsync(string email);
        Task<ApiResponse> CreateUser(tblUser entity);
        Task<ApiResponse> UpdatePassword(ChangePasswordDto changePassword);
        Task<ApiResponse> GetUserInfoById(int id);
        Task<ApiResponse> UpdateUser(tblUser entity);
        Task<ApiResponse> GetAllUsers(string search, string order, string orderDir, int startRec, int pageSize, bool isAll);
        Task<ApiResponse> DeleteUser(int id);
        Task<ApiResponse> SetEnableLogin(int id);
    }
}
