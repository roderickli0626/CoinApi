﻿using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Response_Models;

namespace CoinApi.Services.SubstanceService
{
    public interface ISubstanceService : IService<tblSubstance>
    {
        List<Object> loadDB(DbSyncRequest data);
    }
}
