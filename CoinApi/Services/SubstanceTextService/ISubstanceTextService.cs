using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Shared;

namespace CoinApi.Services.SubstanceTextService
{
    public interface ISubstanceTextService : IService<tblSubstanceText>
    {
        Task<ApiResponse> GetSubStanceByGroupId(int id);
        List<Object> loadDB(DbSyncRequest data);
    }
}
