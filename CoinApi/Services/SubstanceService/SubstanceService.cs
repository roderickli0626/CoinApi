using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mindbox.Data.Linq;
using System.Data.SqlClient;
using Dapper;

namespace CoinApi.Services.SubstanceService
{
    public class SubstanceService : Service<tblSubstance>, ISubstanceService
    {
        public SubstanceService(CoinApiContext context) : base(context)
        {
        }
        public override tblSubstance Create(tblSubstance entity)
        {
            //tblLanguage? language;
            //using (context)
            //{
            //    language = context.tblLanguage.FromSqlRaw("Insert into tblLanguage values (" + entity.languageNumber + ",'" + entity.description + "')").FirstOrDefault();
            //    //context.Database.ExecuteSqlRaw("Insert into tblLanguage values (2, 'second')");
            //}

            tblSubstance subGroup = context.tblSubstance.Add(entity).Entity;
            context.SaveChanges();
            return subGroup;
        }
        public override bool Delete(int id)
        {
            context.tblSubstance.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblSubstance> GetAll()
        {
            return context.tblSubstance.ToList();
        }
        public override List<tblSubstance> GetBy(Func<tblSubstance, bool> predicate)
        {
            return context.tblSubstance
                .Where(predicate)
                .ToList();
        }
        public override tblSubstance GetById(int id)
        {
            return context.tblSubstance
                .FirstOrDefault(x => x.SubstanceID == id);
        }
        public override bool Update(tblSubstance entity)
        {
            if (entity == null) return false;
            tblSubstance? substance = context.tblSubstance.FirstOrDefault(x => x.SubstanceID == entity.SubstanceID);
            if (substance == null) return false;
            substance.Hidde = entity.Hidde;
            substance.WavFile = entity.WavFile;

            context.tblSubstance.Update(substance);
            context.SaveChanges();
            return true;
        }

        public List<Object> loadDB(DbSyncRequest data)
        {
            string query =
                $"SELECT s.SubstanceID, s.Hidde, s.StandardYesNo, s.WavFile, u.*, gt.Description as GroupName, st.Description as Substance, l.description as Language " +
                $"FROM [tblSubstance] AS s INNER JOIN tblSubstanceForGroup AS sfg ON sfg.SubstanceID = s.SubstanceID " +
                $"INNER JOIN tblSubstanceGroup AS g ON g.GroupNumber = sfg.GroupNumber " +
                $"Inner Join tblUser as u on u.UserID = g.UserID " +
                $"join tblSubstanceGroupText gt on gt.GroupNumber= g.GroupNumber " +
                $"join tblSubstanceText st on st.SubstanceID=s.SubstanceID " +
                $"join tblLanguage l on gt.Language= l.languageNumber  and u.LanguageNumber=l.languageNumber and st.Language=l.LanguageNumber " +
                $"WHERE (s.StandardYesNo = 1) AND (g.StandardYesNO = 1) " +
                $"and u.UserID={data.userid} and l.languageNumber ={data.languageid} " +
                $"and u.DeviceNumber='{data.devicenum}' and u.ActiveAcount=1";
            //var result = context.Database.ExecuteSqlRaw(query);

            using (SqlConnection conn = new SqlConnection(context.Database.GetConnectionString()))
            {
                List<Object> result = conn.Query<Object>(query).ToList();
                return result;
            }

        }
    }
}
