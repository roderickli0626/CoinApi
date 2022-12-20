﻿using CoinApi.DB_Models;
using CoinApi.Request_Models;

namespace CoinApi.Services.LanguageService
{
    public interface ILanguageService : IService<tblLanguage>
    {
        List<Object> loadDB(DbSyncRequest data);
    }
}
