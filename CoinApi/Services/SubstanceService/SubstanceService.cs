using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Mindbox.Data.Linq;
using System.Data.SqlClient;
using Dapper;
using CoinApi.Shared;
using Newtonsoft.Json;
using static CoinApi.Shared.ApiFunctions;
using CoinApi.Services.FileStorageService;
using CoinApi.Enums;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Text;
using CoinApi.Helpers;
using CoinApi.Services.CSVService;

namespace CoinApi.Services.SubstanceService
{
    public class SubstanceService : Service<tblSubstance>, ISubstanceService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly ICSVService _csvService;
        private readonly ExcelTypeHelper _excelTypeHeader;
        private readonly ExcelBaseHelper _excelBaseHelper;
        public SubstanceService(CoinApiContext context, IFileStorageService fileStorageService, ExcelTypeHelper excelTypeHelper, ExcelBaseHelper excelBaseHelper, ICSVService csvService) : base(context)
        {
            _fileStorageService = fileStorageService;
            _excelTypeHeader = excelTypeHelper;
            _excelBaseHelper = excelBaseHelper;
            _csvService = csvService;
        }
        public override tblSubstance Create(tblSubstance entity)
        {
            //tblLanguage? language;
            //using (context)
            //{
            //    language = context.tblLanguage.FromSqlRaw("Insert into tblLanguage values (" + entity.languageNumber + ",'" + entity.description + "')").FirstOrDefault();
            //    //context.Database.ExecuteSqlRaw("Insert into tblLanguage values (2, 'second')");
            //}
            entity.DateCreated = DateTime.Now;
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
                $"SELECT s.SubstanceID, g.GroupNumber, s.Hidde, s.DateCreated as DefaultDateTime, s.Duration, s.StandardYesNo, s.WavFile, u.*, gt.Description as GroupName, st.Description as Substance, l.description as Language " +
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

        public List<Object> loadAllDB(DbSyncRequest data)
        {
            string query =
                $"SELECT s.SubstanceID, g.GroupNumber, s.Hidde, s.DateCreated as DefaultDateTime, s.Duration, s.StandardYesNo, s.WavFile, gt.Description as GroupName, st.Description as Substance, l.description as Language " +
                $"FROM [tblSubstance] AS s INNER JOIN tblSubstanceForGroup AS sfg ON sfg.SubstanceID = s.SubstanceID " +
                $"INNER JOIN tblSubstanceGroup AS g ON g.GroupNumber = sfg.GroupNumber " +
                $"join tblSubstanceGroupText gt on gt.GroupNumber= g.GroupNumber " +
                $"join tblSubstanceText st on st.SubstanceID=s.SubstanceID " +
                $"join tblLanguage l on gt.Language= l.languageNumber  and st.Language=l.LanguageNumber " +
                $"WHERE (s.StandardYesNo = 1) AND (g.StandardYesNO = 1) and l.languageNumber = {data.languageid} ";
            //var result = context.Database.ExecuteSqlRaw(query);

            using (SqlConnection conn = new SqlConnection(context.Database.GetConnectionString()))
            {
                List<Object> result = conn.Query<Object>(query).ToList();
                return result;
            }
        }

        public async Task<ApiResponse> AddSubStanceGroup(SubStanceGroupInfoVM subStanceGroupInfo)
        {
            if (subStanceGroupInfo == null)
                return ApiErrorResponse("SubStanceGroup not found.");


            var chkExist = context.tblSubstanceGroupText.FirstOrDefault(s => s.Description.Trim() == subStanceGroupInfo.SubStanceName.Trim());
            if (chkExist != null)
                return ApiErrorResponse("Please enter unique substance group name.");

            if (!string.IsNullOrEmpty(subStanceGroupInfo.WavFile))
            {
                var saveProfileImageFileName = _fileStorageService.SaveFileFromBase64String(DirectoryEnum.SubStanceDirectoryName, subStanceGroupInfo.WavFile, ".wav");
                subStanceGroupInfo.WavFile = saveProfileImageFileName;
            }

            tblSubstance tblSubstance = new tblSubstance()
            {
                StandardYesNo = subStanceGroupInfo.IsStandard,
                Hidde = subStanceGroupInfo.IsHide,
                DateCreated = string.IsNullOrEmpty(subStanceGroupInfo.Date) ? DateTime.UtcNow : DateTime.ParseExact(subStanceGroupInfo.Date, new string[] { "dd/MM/yyyy", "yyyy-MM-dd" }, CultureInfo.InvariantCulture, DateTimeStyles.None).Date,
                Duration = subStanceGroupInfo.Duration,
                //WavFile = subStanceGroupInfo.WavFile
            };
            await context.tblSubstance.AddAsync(tblSubstance);
            await context.SaveChangesAsync();

            tblSubstanceText tblSubstanceText = new tblSubstanceText()
            {
                SubstanceID = tblSubstance.SubstanceID,
                Description = subStanceGroupInfo.SubStanceName,
                Language = subStanceGroupInfo.LanguageId
            };
            await context.tblSubstanceText.AddAsync(tblSubstanceText);
            await context.SaveChangesAsync();
            tblSubstanceForGroup tblSubstanceForGroup = new tblSubstanceForGroup()
            {
                SubstanceID = tblSubstance.SubstanceID,
                GroupNumber = subStanceGroupInfo.GroupNumber
            };
            await context.tblSubstanceForGroup.AddAsync(tblSubstanceForGroup);
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "SubstanceGroup create successfully.");
        }
        public async Task<ApiResponse> UpdateSubStanceGroup(SubStanceGroupInfoVM subStanceGroupInfo)
        {
            string fileName = string.Empty;
            if (subStanceGroupInfo == null)
                return ApiErrorResponse("SubstanceGroup not found.");


            var chkSubstancegroup = context.tblSubstance.FirstOrDefault(s => s.SubstanceID == subStanceGroupInfo.Id);
            if (chkSubstancegroup == null)
                return ApiErrorResponse("SubstanceGroup not found.");

            if (!string.IsNullOrEmpty(subStanceGroupInfo.WavFile))
            {
                fileName = _fileStorageService.SaveFileFromBase64String(DirectoryEnum.SubStanceDirectoryName, subStanceGroupInfo.WavFile, ".jpeg");
            }

            chkSubstancegroup.DateCreated = string.IsNullOrEmpty(subStanceGroupInfo.Date) ? DateTime.UtcNow : DateTime.ParseExact(subStanceGroupInfo.Date, new string[] { "dd/MM/yyyy", "yyyy-MM-dd" }, CultureInfo.InvariantCulture, DateTimeStyles.None).Date;
            chkSubstancegroup.StandardYesNo = subStanceGroupInfo.IsStandard;
            chkSubstancegroup.Duration = subStanceGroupInfo.Duration;
            chkSubstancegroup.Hidde = subStanceGroupInfo.IsHide;
            //chkSubstancegroup.WavFile = string.IsNullOrEmpty(fileName) ? chkSubstancegroup.WavFile : fileName;
            await context.SaveChangesAsync();

            var chkSubstancegrouptext = context.tblSubstanceText.FirstOrDefault(s => s.SubstanceID == subStanceGroupInfo.Id);
            if (chkSubstancegrouptext != null)
            {
                chkSubstancegrouptext.Description = subStanceGroupInfo.SubStanceName;
                chkSubstancegrouptext.Language = subStanceGroupInfo.LanguageId;
                await context.SaveChangesAsync();
            }
            var chkSubstanceforgroup = context.tblSubstanceForGroup.FirstOrDefault(s => s.SubstanceID == subStanceGroupInfo.Id);
            if (chkSubstanceforgroup != null)
            {
                chkSubstanceforgroup.GroupNumber = subStanceGroupInfo.GroupNumber;
                await context.SaveChangesAsync();
            }
            return ApiSuccessResponses(null, "SubStanceGroup update successfully.");
        }

        public async Task<ApiResponse> AddImportGroupSubStance(IFormFileCollection file, bool isGroup)
        {
            try
            {
                if (isGroup)
                {
                    var groupInfoDto = _csvService.ReadCSV<ImportGroupInfo>(file[0].OpenReadStream()).ToList();
                    foreach (var item in groupInfoDto)
                    {
                        await AddImportGroup(item.GroupName, item.Language, item.Standard, item.Hide);

                    }
                }
                else
                {
                    var subStanceGroupInfoDto = _csvService.ReadCSV<ImportSubStanceGroupInfo>(file[0].OpenReadStream()).ToList();
                    foreach (var item in subStanceGroupInfoDto)
                    {
                        int? groupId = 0;
                        int languageId = 0;
                        if (!string.IsNullOrEmpty(item.Group))
                        {
                            var chkExistGroup = await context.tblSubstanceGroupText.FirstOrDefaultAsync(s => s.Description.Trim().ToLower() == item.Group.Trim().ToLower());
                            if (chkExistGroup == null)
                            {
                                groupId = await AddImportGroup(item.Group, item.Language, item.Standard, item.Hide);
                            }
                            else
                            {
                                groupId = chkExistGroup.GroupNumber;
                            }

                            if (!string.IsNullOrEmpty(item.Language))
                            {
                                var chkExistLanguage = await context.tblLanguage.FirstOrDefaultAsync(s => s.description.Trim().ToLower() == item.Language.Trim().ToLower());
                                if (chkExistLanguage == null)
                                {
                                    tblLanguage tblLanguage = new tblLanguage()
                                    {
                                        description = item.Language.Trim()
                                    };
                                    await context.tblLanguage.AddAsync(tblLanguage);
                                    await context.SaveChangesAsync();
                                    languageId = tblLanguage.languageNumber;
                                }
                                else
                                {
                                    languageId = chkExistLanguage.languageNumber;
                                }
                            }
                            tblSubstance tblSubstance = new tblSubstance()
                            {
                                StandardYesNo = item.Standard,
                                Hidde = item.Hide,
                                DateCreated = string.IsNullOrEmpty(item.DateCreate) ? DateTime.UtcNow : DateTime.ParseExact(item.DateCreate, new string[] { "dd/MM/yyyy", "yyyy-MM-dd" }, CultureInfo.InvariantCulture, DateTimeStyles.None).Date,
                                Duration = item.Duration,
                            };
                            await context.tblSubstance.AddAsync(tblSubstance);
                            await context.SaveChangesAsync();

                            tblSubstanceText tblSubstanceText = new tblSubstanceText()
                            {
                                SubstanceID = tblSubstance.SubstanceID,
                                Description = item.SubStanceName,
                                Language = languageId
                            };
                            await context.tblSubstanceText.AddAsync(tblSubstanceText);
                            await context.SaveChangesAsync();
                            tblSubstanceForGroup tblSubstanceForGroup = new tblSubstanceForGroup()
                            {
                                SubstanceID = tblSubstance.SubstanceID,
                                GroupNumber = groupId
                            };
                            await context.tblSubstanceForGroup.AddAsync(tblSubstanceForGroup);
                            await context.SaveChangesAsync();
                        }



                    }
                }
                return ApiSuccessResponses(null, isGroup ? "Import group file successfully." : "Import substance group file successfully.");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<int> AddImportGroup(string groupName, string? language, bool? standard, bool? hide)
        {
            int groupNumber = 0;
            try
            {
                int languageId = 0;
                if (!string.IsNullOrEmpty(groupName))
                {
                    if (!string.IsNullOrEmpty(language))
                    {
                        var chkExistLanguage = await context.tblLanguage.FirstOrDefaultAsync(s => s.description.Trim().ToLower() == language.Trim().ToLower());
                        if (chkExistLanguage == null)
                        {
                            tblLanguage tblLanguage = new tblLanguage()
                            {
                                description = language.Trim()
                            };
                            await context.tblLanguage.AddAsync(tblLanguage);
                            await context.SaveChangesAsync();
                            languageId = tblLanguage.languageNumber;
                        }
                        else
                        {
                            languageId = chkExistLanguage.languageNumber;
                        }
                    }
                    tblSubstanceGroup tblSubstanceGroup = new tblSubstanceGroup()
                    {
                        UserID = 0,
                        StandardYesNo = standard,
                        ViewYesNo = hide
                    };
                    await context.tblSubstanceGroup.AddAsync(tblSubstanceGroup);
                    await context.SaveChangesAsync();

                    tblSubstanceGroupText tblSubstanceGroupText = new tblSubstanceGroupText()
                    {
                        GroupNumber = tblSubstanceGroup.GroupNumber,
                        Language = languageId,
                        Description = groupName
                    };
                    await context.tblSubstanceGroupText.AddAsync(tblSubstanceGroupText);
                    await context.SaveChangesAsync();
                    groupNumber = tblSubstanceGroup.GroupNumber;
                }
            }
            catch (Exception ex)
            {
                groupNumber = 0;
            }
            return groupNumber;
        }
        public List<ImportGroupInfo> ParseListingReport(string result)
        {
            var products = new List<ImportGroupInfo>();

            if (string.IsNullOrEmpty(result))
            {
                return products;
            }

            var nameMaxLength = 500;

            var lines = result.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            var rowCount = 0;

            foreach (var line in lines)
            {
                rowCount++;

                var columns = line.Split('\t');

                // skip headers or incomplete items
                if (rowCount == 1 || columns[28] == "Incomplete")
                {
                    continue;
                }

                decimal qty;
                var qtyParsed = decimal.TryParse(columns[5], out qty);

                //var timeZone = columns[6].Substring(20, 3);

                var product = new ImportGroupInfo()
                {
                    //TODO - parse correct create time
                    //CreatedAt = DateTime.ParseExact(columns[6], "yyyy-MM-dd HH:mm:ss" + timeZone, CultureInfo.InvariantCulture),
                    GroupName = columns[2],
                };

                products.Add(product);
            }

            return products;

        }
        public async Task<ApiResponse> GetSubStanceGroupInfoById(int id)
        {
            var getGroupInfo = await (from tsg in context.tblSubstance
                                      join tsgt in context.tblSubstanceText on tsg.SubstanceID equals tsgt.SubstanceID
                                      join tsfg in context.tblSubstanceForGroup on tsg.SubstanceID equals tsfg.SubstanceID
                                      join tl in context.tblLanguage on tsgt.Language equals tl.languageNumber into Language
                                      from tl in Language.DefaultIfEmpty()
                                      where tsg.SubstanceID == id
                                      select new SubStanceGroupInfoVM
                                      {
                                          Id = tsg.SubstanceID,
                                          SubStanceName = tsgt.Description,
                                          Language = tl.description,
                                          IsStandard = tsg.StandardYesNo,
                                          IsHide = tsg.Hidde,
                                          LanguageId = tsgt.Language,
                                          DateCreate = tsg.DateCreated,
                                          Duration = tsg.Duration,
                                          GroupNumber = tsfg.GroupNumber,
                                          //WavFile = tsg.WavFile
                                      }).FirstOrDefaultAsync();
            if (getGroupInfo == null)
                return ApiErrorResponse("Please enter valid substancegroup.");

            return ApiSuccessResponses(getGroupInfo, "Get successfully ");
        }
        public async Task<ApiResponse> DeleteSubStanceGroup(int id)
        {
            var getSubstanceGroupInfo = await context.tblSubstance.FirstOrDefaultAsync(s => s.SubstanceID == id);
            if (getSubstanceGroupInfo == null)
                return ApiErrorResponse("Please enter valid group.");

            var getSubGroupInfo = await context.tblSubstanceText.FirstOrDefaultAsync(s => s.SubstanceID == id);
            var getSubforGroupInfo = await context.tblSubstanceForGroup.FirstOrDefaultAsync(s => s.SubstanceID == id);
            if (getSubGroupInfo != null)
                context.tblSubstanceText.Remove(getSubGroupInfo);

            if (getSubforGroupInfo != null)
                context.tblSubstanceForGroup.Remove(getSubforGroupInfo);

            if (getSubstanceGroupInfo != null)
                context.tblSubstance.Remove(getSubstanceGroupInfo);

            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "SubstanceGroup successfully deleted.");
        }

        public async Task<ApiResponse> GetAllSubstanceGroups(string search = null, string order = "0", string orderDir = "asc", int startRec = 0, int pageSize = 10, bool isAll = false)
        {
            var data = new List<SubStanceGroupInfoVM>();
            try
            {

                data = await (from tsg in context.tblSubstance
                              join tsgt in context.tblSubstanceText on tsg.SubstanceID equals tsgt.SubstanceID
                              join tl in context.tblLanguage on tsgt.Language equals tl.languageNumber into Language
                              from tl in Language.DefaultIfEmpty()
                              select new SubStanceGroupInfoVM
                              {
                                  Id = tsg.SubstanceID,
                                  SubStanceName = tsgt.Description,
                                  Language = tl.description,
                                  IsStandard = tsg.StandardYesNo,
                                  IsHide = tsg.Hidde,
                                  Duration = tsg.Duration,
                                  DateCreate = tsg.DateCreated
                              }).ToListAsync();

                int totalRecords = data.Count;
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => (p.SubStanceName != null && p.SubStanceName.ToLower().Contains(search.ToLower())) ||
                    (p.Language != null && p.Language.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.Duration != null && p.Duration.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.IsStandard != null && p.IsStandard.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.IsHide != null && p.IsHide.ToString().ToLower().Contains(search.ToLower()))).ToList();
                }
                data = SortTableSubStanceGroupList(order, orderDir, data);
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
        private List<SubStanceGroupInfoVM> SortTableSubStanceGroupList(string order, string orderDir, List<SubStanceGroupInfoVM> data)
        {
            List<SubStanceGroupInfoVM> stateList = new List<SubStanceGroupInfoVM>();
            try
            {
                switch (order)
                {
                    case "0":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Id).ToList() : data.OrderBy(p => p.Id).ToList();
                        break;
                    case "1":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.SubStanceName).ToList() : data.OrderBy(p => p.SubStanceName).ToList();
                        break;
                    case "2":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Language).ToList() : data.OrderBy(p => p.Language).ToList();
                        break;
                    case "3":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.IsStandard).ToList() : data.OrderBy(p => p.IsStandard).ToList();
                        break;
                    case "4":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.IsHide).ToList() : data.OrderBy(p => p.IsHide).ToList();
                        break;
                    case "5":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.DateCreate).ToList() : data.OrderBy(p => p.DateCreate).ToList();
                        break;
                    case "6":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Duration).ToList() : data.OrderBy(p => p.Duration).ToList();
                        break;
                    default:
                        stateList = data.OrderByDescending(p => p.Id).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return stateList;
        }
    }
}
