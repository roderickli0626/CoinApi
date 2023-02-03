using CoinApi.DB_Models;
using CoinApi.Request_Models;

namespace CoinApi.Services.SubstanceGroupTextService
{
    public interface ISubstanceGroupTextService : IService<tblSubstanceGroupText>
    {
        List<tblSubstanceGroupText> GetAllGroupsByLanguageId(int languageId);
        List<Object> loadDB(DbSyncRequest data);
    }
}
