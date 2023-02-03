using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Shared;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static CoinApi.Shared.ApiFunctions;

namespace CoinApi.Services.SubstanceTextService
{
    public class SubstanceTextService : Service<tblSubstanceText>, ISubstanceTextService
    {
        public SubstanceTextService(CoinApiContext context) : base(context)
        {
        }
        public override tblSubstanceText Create(tblSubstanceText entity)
        {
            //tblLanguage? language;
            //using (context)
            //{
            //    language = context.tblLanguage.FromSqlRaw("Insert into tblLanguage values (" + entity.languageNumber + ",'" + entity.description + "')").FirstOrDefault();
            //    //context.Database.ExecuteSqlRaw("Insert into tblLanguage values (2, 'second')");
            //}

            tblSubstanceText subGroupText = context.tblSubstanceText.Add(entity).Entity;
            context.SaveChanges();
            return subGroupText;
        }
        public override bool Delete(int id)
        {
            context.tblSubstanceText.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblSubstanceText> GetAll()
        {
            return context.tblSubstanceText.ToList();
        }
        public override List<tblSubstanceText> GetBy(Func<tblSubstanceText, bool> predicate)
        {
            return context.tblSubstanceText
                .Where(predicate)
                .ToList();
        }
        public override tblSubstanceText GetById(int id)
        {
            return context.tblSubstanceText
                .FirstOrDefault(x => x.Id == id);
        }
        public override bool Update(tblSubstanceText entity)
        {
            if (entity == null) return false;
            tblSubstanceText? substanceText = context.tblSubstanceText.FirstOrDefault(x => x.Id == entity.Id);
            if (substanceText == null) return false;
            substanceText.SubstanceID = entity.SubstanceID;
            substanceText.Description = entity.Description;
            substanceText.Language = entity.Language;

            context.tblSubstanceText.Update(substanceText);
            context.SaveChanges();
            return true;
        }
        public async Task<ApiResponse> GetSubStanceByGroupId(int id, int languageId)
        {
            //var getUserInfo = await context.tblUser.FirstOrDefaultAsync(s => s.UserID == id);
            var getUserInfo = await (from tm in context.tblSubstanceText
                                     join tc in context.tblSubstanceForGroup on tm.SubstanceID equals tc.SubstanceID into Group
                                     from tc in Group.DefaultIfEmpty()
                                     where tc.GroupNumber == id
                                     select new
                                     {
                                         SubstanceID = tm.SubstanceID,
                                         Description = tm.Description,
                                         LanguageId = tm.Language
                                     }).ToListAsync();

            if (languageId != 0)
            {
                getUserInfo = getUserInfo.Where(s => s.LanguageId == languageId).ToList();
            }
            if (getUserInfo == null)
                return ApiErrorResponse("Please enter valid group.");

            return ApiSuccessResponse(getUserInfo);
        }

        public List<Object> loadDB(DbSyncRequest data)
        {
            string query =
                $"SELECT st.* " +
                $"FROM [tblSubstanceText] AS st INNER JOIN tblSubstance AS s ON s.SubstanceID = st.SubstanceID " +
                $"INNER JOIN tblSubstanceForGroup AS sfg ON sfg.SubstanceID = s.SubstanceID " +
                $"INNER JOIN tblSubstanceGroup AS g ON g.GroupNumber = sfg.GroupNumber " +
                $"Inner Join tblUser as u on u.UserID = g.UserID " +
                $"WHERE (s.StandardYesNo = 1) AND (g.StandardYesNO = 1) " +
                $"and u.UserID={data.userid}";

            using (SqlConnection conn = new SqlConnection(context.Database.GetConnectionString()))
            {
                List<Object> result = conn.Query<Object>(query).ToList();
                return result;
            }

        }
    }
}
