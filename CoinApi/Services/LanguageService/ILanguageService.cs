using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Shared;

namespace CoinApi.Services.LanguageService
{
    public interface ILanguageService : IService<tblLanguage>
    {
        Task<ApiResponse> GetAllLanguages();
        Task<ApiResponse> AddLanguage(tblLanguage tblLanguage);
        Task<ApiResponse> UpdateLanguage(tblLanguage tblLanguage);
        Task<ApiResponse> GetLanguageInfoById(int id);
        Task<ApiResponse> GetAllLanguages(string search, string order, string orderDir, int startRec, int pageSize, bool isAll);
        Task<ApiResponse> DeleteLanguage(int id);
        List<Object> loadDB(DbSyncRequest data);

    }
}
