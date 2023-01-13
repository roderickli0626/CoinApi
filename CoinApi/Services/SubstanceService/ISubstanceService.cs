using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Shared;

namespace CoinApi.Services.SubstanceService
{
    public interface ISubstanceService : IService<tblSubstance>
    {
        List<Object> loadDB(DbSyncRequest data);
        Task<ApiResponse> AddSubStanceGroup(SubStanceGroupInfoVM subStanceGroupInfo);
        Task<ApiResponse> UpdateSubStanceGroup(SubStanceGroupInfoVM subStanceGroupInfo);
        Task<ApiResponse> AddImportGroupSubStance(IFormFileCollection file,bool isGroup);
        Task<ApiResponse> DeleteSubStanceGroup(int id);
        Task<ApiResponse> GetSubStanceGroupInfoById(int id);
        Task<ApiResponse> GetAllSubstanceGroups(string search, string order, string orderDir, int startRec, int pageSize, bool isAll);
    }
}
