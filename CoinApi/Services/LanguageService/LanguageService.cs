using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Shared;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.SqlClient;
using static CoinApi.Shared.ApiFunctions;

namespace CoinApi.Services.LanguageService
{
    public class LanguageService : Service<tblLanguage>, ILanguageService
    {
        public LanguageService(CoinApiContext context) : base(context)
        {
        }
        public override tblLanguage Create(tblLanguage entity)
        {
            //tblLanguage? language;
            //using (context)
            //{
            //    language = context.tblLanguage.FromSqlRaw("Insert into tblLanguage values (" + entity.languageNumber + ",'" + entity.description + "')").FirstOrDefault();
            //    //context.Database.ExecuteSqlRaw("Insert into tblLanguage values (2, 'second')");
            //}

            tblLanguage language = context.tblLanguage.Add(entity).Entity;
            context.SaveChanges();
            return language;
        }
        public override bool Delete(int id)
        {
            context.tblLanguage.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblLanguage> GetAll()
        {
            return context.tblLanguage.ToList();
        }
        public async Task<ApiResponse> GetAllLanguages()
        {
            var getLanguageInfo = context.tblLanguage.Select(s => new
            {
                s.languageNumber,
                s.description
            }).ToList();
            return ApiSuccessResponses(getLanguageInfo, "Get language successfully.");
        }
        public override List<tblLanguage> GetBy(Func<tblLanguage, bool> predicate)
        {
            return context.tblLanguage
                .Where(predicate)
                .ToList();
        }
        public override tblLanguage GetById(int id)
        {
            return context.tblLanguage
                .FirstOrDefault(x => x.languageNumber == id);
        }
        public override bool Update(tblLanguage entity)
        {
            if (entity == null) return false;
            tblLanguage? language = context.tblLanguage.FirstOrDefault(x => x.languageNumber == entity.languageNumber);
            if (language == null) return false;
            language.description = entity.description;

            context.tblLanguage.Update(language);
            context.SaveChanges();
            return true;
        }
        public async Task<ApiResponse> AddLanguage(tblLanguage tblLanguage)
        {
            if (tblLanguage == null)
                return ApiErrorResponse("Language not found.");

            var chkExist = context.tblLanguage.FirstOrDefault(s => s.description.Trim().ToLower() == tblLanguage.description.Trim().ToLower());
            if (chkExist != null)
                return ApiErrorResponse("Please enter unique language name.");

            await context.tblLanguage.AddAsync(tblLanguage);
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "Language create successfully.");
        }
        public async Task<ApiResponse> UpdateLanguage(tblLanguage tblLanguage)
        {
            if (tblLanguage == null)
                return ApiErrorResponse("Language not found.");

            tblLanguage? languageInfo = context.tblLanguage.FirstOrDefault(x => x.languageNumber == tblLanguage.languageNumber);
            if (languageInfo == null)
                return ApiErrorResponse("Language not found.");

            tblLanguage? checkLanguage = context.tblLanguage.FirstOrDefault(x => x.description.Trim().ToLower() == tblLanguage.description.Trim().ToLower() && x.languageNumber != tblLanguage.languageNumber);
            if (checkLanguage != null)
                return ApiErrorResponse("Please enter unique language name.");

            languageInfo.description = tblLanguage.description;
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "Language update successfully.");
        }
        public async Task<ApiResponse> GetLanguageInfoById(int id)
        {
            tblLanguage languegInfo = await context.tblLanguage.FirstOrDefaultAsync(s => s.languageNumber == id);
            if (languegInfo == null)
                return ApiErrorResponse("Please enter valid language.");

            return ApiSuccessResponse(languegInfo);
        }
        public async Task<ApiResponse> DeleteLanguage(int id)
        {
            var getLanguageInfo = await context.tblLanguage.FirstOrDefaultAsync(s => s.languageNumber == id);
            if (getLanguageInfo == null)
                return ApiErrorResponse("Please enter valid language.");

            bool chkSubstanceExist = await context.tblSubstanceText.AnyAsync(s => s.Language == id);
            bool chkSubstanceGroupExist = await context.tblSubstanceGroupText.AnyAsync(s => s.Language == id);
            if (chkSubstanceExist || chkSubstanceGroupExist)
            {
                return ApiErrorResponse("This language is already in used so you can't delete it.");
            }

            context.tblLanguage.Remove(getLanguageInfo);
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "Language successfully deleted.");
        }
        public async Task<ApiResponse> GetAllLanguages(string search = null, string order = "0", string orderDir = "asc", int startRec = 0, int pageSize = 10, bool isAll = false)
        {
            var data = new List<tblLanguage>();
            try
            {
                data = await context.tblLanguage.Select(s => new tblLanguage
                {
                    languageNumber = s.languageNumber,
                    description = s.description
                }).ToListAsync();


                int totalRecords = data.Count;
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => (p.description != null && p.description.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.languageNumber != null && p.languageNumber.ToString().ToLower().Contains(search.ToLower()))).ToList();
                }
                data = SortTableLanguageList(order, orderDir, data);
                int recFilter = data.Count;
                data = isAll ? data.ToList() : data.Skip(startRec).Take(pageSize).ToList();
                DataTableResponseVM model = new DataTableResponseVM
                {
                    RecFilter = recFilter,
                    TotalRecords = totalRecords,
                    Response = JsonConvert.SerializeObject(data)
                };
                return new ApiResponse
                {
                    IsSuccess = true,
                    Data = JsonConvert.SerializeObject(model)
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        private List<tblLanguage> SortTableLanguageList(string order, string orderDir, List<tblLanguage> data)
        {
            List<tblLanguage> stateList = new List<tblLanguage>();
            try
            {
                switch (order)
                {
                    case "0":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.languageNumber).ToList() : data.OrderBy(p => p.languageNumber).ToList();
                        break;
                    case "1":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.description).ToList() : data.OrderBy(p => p.description).ToList();
                        break;
                    default:
                        stateList = data.OrderByDescending(p => p.languageNumber).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return stateList;
        }

        public List<Object> loadDB(DbSyncRequest data)
        {
            string query = "SELECT * FROM [tblLanguage]";

            using (SqlConnection conn = new SqlConnection(context.Database.GetConnectionString()))
            {
                List<Object> result = conn.Query<Object>(query).ToList();
                return result;
            }

        }
    }
}
