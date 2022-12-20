using CoinApi.DB_Models;
using CoinApi.Request_Models;

namespace CoinApi.Services.LanguageGUIService
{
    public interface ILanguageGUIService : IService<tblLanguageGUI>
    {
        List<Object> loadDB(DbSyncRequest data);
    }
}
