using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Shared;

namespace CoinApi.Services.ModuleService
{
    public interface IModuleService
    {
        Task<ApiResponse> AddModule(AddModuleVM entity);
        Task<ApiResponse> UpdateModule(AddModuleVM entity);
        Task<ApiResponse> GetModuleInfoById(int id);
        Task<ApiResponse> GetAllModules(string search, string order, string orderDir, int startRec, int pageSize, bool isAll);
        Task<ApiResponse> DeleteModule(int id);
        Task<ApiResponse> GetGroupInfo();
        Task<ApiResponse> GetGroupDescriptionInfo();
    }
}
