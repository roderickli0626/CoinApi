using CoinApi.DB_Models;
using CoinApi.Request_Models;

namespace CoinApi.Services.SubstanceTextService
{
    public interface ISubstanceTextService : IService<tblSubstanceText>
    {
        List<Object> loadDB(DbSyncRequest data);
    }
}
