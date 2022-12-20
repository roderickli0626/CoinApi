using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Request_Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace CoinApi.Services.LanguageGUIService
{
    public class LanguageGUIService : Service<tblLanguageGUI>, ILanguageGUIService
    {
        public LanguageGUIService(CoinApiContext context) : base(context)
        {
        }
        public override tblLanguageGUI Create(tblLanguageGUI entity)
        {
            tblLanguageGUI languageGUI = context.tblLanguageGUI.Add(entity).Entity;
            context.SaveChanges();
            return languageGUI;
        }
        public override bool Delete(int id)
        {
            context.tblLanguageGUI.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblLanguageGUI> GetAll()
        {
            return context.tblLanguageGUI.ToList();
        }
        public override List<tblLanguageGUI> GetBy(Func<tblLanguageGUI, bool> predicate)
        {
            return context.tblLanguageGUI
                .Where(predicate)
                .ToList();
        }
        public override tblLanguageGUI GetById(int id)
        {
            return context.tblLanguageGUI
                .FirstOrDefault(x => x.Id == id);
        }
        public override bool Update(tblLanguageGUI entity)
        {
            if (entity == null) return false;
            tblLanguageGUI? languageGUI = context.tblLanguageGUI.FirstOrDefault(x => x.Id == entity.Id);
            if (languageGUI == null) return false;
            
            //TODO (Update Items)

            context.tblLanguageGUI.Update(languageGUI);
            context.SaveChanges();
            return true;
        }

        public List<Object> loadDB(DbSyncRequest data)
        {
            string query = "SELECT * FROM [tblLanguageGUI]";

            using (SqlConnection conn = new SqlConnection(context.Database.GetConnectionString()))
            {
                List<Object> result = conn.Query<Object>(query).ToList();
                return result;
            }

        }
    }
}
