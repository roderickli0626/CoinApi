using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Shared;

namespace CoinApi.Services.SubstanceGroupService
{
    public interface ISubstanceGroupService : IService<tblSubstanceGroup>
    {
        Task<ApiResponse> AddGroup(GroupInfoDto groupInfoDto);
        Task<ApiResponse> UpdateGroup(GroupInfoDto groupInfoDto);
        Task<ApiResponse> DeleteGroup(int id);
        Task<ApiResponse> GetGroupInfoById(int id);
        Task<ApiResponse> GetAllGroups(string search, string order, string orderDir, int startRec, int pageSize, bool isAll, string languageNumber, string searchValue = null);


    }
}
