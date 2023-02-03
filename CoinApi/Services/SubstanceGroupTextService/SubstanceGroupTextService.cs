using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Request_Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace CoinApi.Services.SubstanceGroupTextService
{
    public class SubstanceGroupTextService : Service<tblSubstanceGroupText>, ISubstanceGroupTextService
    {
        public SubstanceGroupTextService(CoinApiContext context) : base(context)
        {
        }
        public override tblSubstanceGroupText Create(tblSubstanceGroupText entity)
        {
            //tblLanguage? language;
            //using (context)
            //{
            //    language = context.tblLanguage.FromSqlRaw("Insert into tblLanguage values (" + entity.languageNumber + ",'" + entity.description + "')").FirstOrDefault();
            //    //context.Database.ExecuteSqlRaw("Insert into tblLanguage values (2, 'second')");
            //}

            tblSubstanceGroupText subGroup = context.tblSubstanceGroupText.Add(entity).Entity;
            context.SaveChanges();
            return subGroup;
        }
        public override bool Delete(int id)
        {
            context.tblSubstanceGroupText.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblSubstanceGroupText> GetAll()
        {
            return context.tblSubstanceGroupText.ToList();
        }
        public List<tblSubstanceGroupText> GetAllGroupsByLanguageId(int languageId)
        {
            if (languageId != 0)
            {
                return context.tblSubstanceGroupText.Where(s => s.Language == languageId).ToList();
            }
            else
            {
                return context.tblSubstanceGroupText.ToList();
            }

        }
        public override List<tblSubstanceGroupText> GetBy(Func<tblSubstanceGroupText, bool> predicate)
        {
            return context.tblSubstanceGroupText
                .Where(predicate)
                .ToList();
        }
        public override tblSubstanceGroupText GetById(int id)
        {
            return context.tblSubstanceGroupText
                .FirstOrDefault(x => x.Id == id);
        }
        public override bool Update(tblSubstanceGroupText entity)
        {
            if (entity == null) return false;
            tblSubstanceGroupText? substanceGroupText = context.tblSubstanceGroupText.FirstOrDefault(x => x.Id == entity.Id);
            if (substanceGroupText == null) return false;
            substanceGroupText.GroupNumber = entity.GroupNumber;
            substanceGroupText.Description = entity.Description;
            substanceGroupText.Language = entity.Language;

            context.tblSubstanceGroupText.Update(substanceGroupText);
            context.SaveChanges();
            return true;
        }

        public List<Object> loadDB(DbSyncRequest data)
        {
            string query =
                $"SELECT gt.* " +
                $"FROM [tblSubstanceGroupText] AS gt INNER JOIN tblSubstanceGroup AS g ON g.GroupNumber = gt.GroupNumber " +
                $"Inner Join tblUser as u on u.UserID = g.UserID " +
                $"WHERE (g.StandardYesNO = 1) " +
                $"and u.UserID={data.userid}";

            using (SqlConnection conn = new SqlConnection(context.Database.GetConnectionString()))
            {
                List<Object> result = conn.Query<Object>(query).ToList();
                return result;
            }

        }
    }
}
