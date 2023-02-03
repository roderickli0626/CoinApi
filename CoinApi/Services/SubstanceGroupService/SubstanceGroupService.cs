using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Enums;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.FileStorageService;
using CoinApi.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using static CoinApi.Shared.ApiFunctions;

namespace CoinApi.Services.SubstanceGroupService
{
    public class SubstanceGroupService : Service<tblSubstanceGroup>, ISubstanceGroupService
    {
        private readonly IFileStorageService _fileStorageService;
        public SubstanceGroupService(CoinApiContext context, IFileStorageService fileStorageService) : base(context)
        {
            _fileStorageService = fileStorageService;
        }
        public override tblSubstanceGroup Create(tblSubstanceGroup entity)
        {
            //tblLanguage? language;
            //using (context)
            //{
            //    language = context.tblLanguage.FromSqlRaw("Insert into tblLanguage values (" + entity.languageNumber + ",'" + entity.description + "')").FirstOrDefault();
            //    //context.Database.ExecuteSqlRaw("Insert into tblLanguage values (2, 'second')");
            //}

            tblSubstanceGroup subGroup = context.tblSubstanceGroup.Add(entity).Entity;
            context.SaveChanges();
            return subGroup;
        }
        public override bool Delete(int id)
        {
            context.tblSubstanceGroup.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblSubstanceGroup> GetAll()
        {
            return context.tblSubstanceGroup.ToList();
        }
        public override List<tblSubstanceGroup> GetBy(Func<tblSubstanceGroup, bool> predicate)
        {
            return context.tblSubstanceGroup
                .Where(predicate)
                .ToList();
        }
        public override tblSubstanceGroup GetById(int id)
        {
            return context.tblSubstanceGroup
                .FirstOrDefault(x => x.GroupNumber == id);
        }
        public override bool Update(tblSubstanceGroup entity)
        {
            if (entity == null) return false;
            tblSubstanceGroup? substanceGroup = context.tblSubstanceGroup.FirstOrDefault(x => x.GroupNumber == entity.GroupNumber);
            if (substanceGroup == null) return false;
            substanceGroup.UserID = entity.UserID;
            substanceGroup.ViewYesNo = entity.ViewYesNo;

            context.tblSubstanceGroup.Update(substanceGroup);
            context.SaveChanges();
            return true;
        }
        public async Task<ApiResponse> AddGroup(GroupInfoDto groupInfo)
        {
            if (groupInfo == null)
                return ApiErrorResponse("Group not found.");


            var chkExist = context.tblSubstanceGroupText.FirstOrDefault(s => s.Description.Trim() == groupInfo.GroupName.Trim());
            if (chkExist != null)
                return ApiErrorResponse("Please enter unique group name.");


            tblSubstanceGroup tblSubstanceGroup = new tblSubstanceGroup()
            {
                UserID = groupInfo.UserId,
                StandardYesNo = groupInfo.IsStandard,
                ViewYesNo = groupInfo.IsHide
            };
            await context.tblSubstanceGroup.AddAsync(tblSubstanceGroup);
            await context.SaveChangesAsync();

            tblSubstanceGroupText tblSubstanceGroupText = new tblSubstanceGroupText()
            {
                GroupNumber = tblSubstanceGroup.GroupNumber,
                Language = groupInfo.LanguageId,
                Description = groupInfo.GroupName
            };
            await context.tblSubstanceGroupText.AddAsync(tblSubstanceGroupText);
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "Group create successfully.");
        }
        public async Task<ApiResponse> UpdateGroup(GroupInfoDto groupInfo)
        {
            if (groupInfo == null)
                return ApiErrorResponse("Group not found.");


            var chkSubstancegroup = context.tblSubstanceGroup.FirstOrDefault(s => s.GroupNumber == groupInfo.Id);
            if (chkSubstancegroup == null)
                return ApiErrorResponse("Group not found.");

            chkSubstancegroup.UserID = groupInfo.UserId;
            chkSubstancegroup.StandardYesNo = groupInfo.IsStandard;
            chkSubstancegroup.ViewYesNo = groupInfo.IsHide;
            await context.SaveChangesAsync();

            var chkSubstancegrouptext = context.tblSubstanceGroupText.FirstOrDefault(s => s.GroupNumber == groupInfo.Id);
            if (chkSubstancegrouptext != null)
            {
                chkSubstancegrouptext.Language = groupInfo.LanguageId;
                chkSubstancegrouptext.Description = groupInfo.GroupName;
                await context.SaveChangesAsync();
            }
            return ApiSuccessResponses(null, "Group update successfully.");
        }
        public async Task<ApiResponse> GetGroupInfoById(int id)
        {
            var getGroupInfo = await (from tsg in context.tblSubstanceGroup
                                      join tsgt in context.tblSubstanceGroupText on tsg.GroupNumber equals tsgt.GroupNumber
                                      join tl in context.tblLanguage on tsgt.Language equals tl.languageNumber into Language
                                      from tl in Language.DefaultIfEmpty()
                                      where tsg.GroupNumber == id
                                      select new GroupInfoDto
                                      {
                                          Id = tsg.GroupNumber,
                                          GroupName = tsgt.Description,
                                          Language = tl.description,
                                          IsStandard = tsg.StandardYesNo,
                                          IsHide = tsg.ViewYesNo,
                                          LanguageId = tsgt.Language,
                                          UserId = tsg.UserID
                                      }).FirstOrDefaultAsync();
            if (getGroupInfo == null)
                return ApiErrorResponse("Please enter valid group.");

            return ApiSuccessResponses(getGroupInfo, "Group successfully deleted.");
        }
        public async Task<ApiResponse> DeleteGroup(int id)
        {
            var getGroupInfo = await context.tblSubstanceGroup.FirstOrDefaultAsync(s => s.GroupNumber == id);
            if (getGroupInfo == null)
                return ApiErrorResponse("Please enter valid group.");

            var getSubGroupInfo = await context.tblSubstanceGroupText.FirstOrDefaultAsync(s => s.GroupNumber == id);
            context.tblSubstanceGroupText.Remove(getSubGroupInfo);
            context.tblSubstanceGroup.Remove(getGroupInfo);
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "Group successfully deleted.");
        }
        public async Task<ApiResponse> GetAllGroups(string search = null, string order = "0", string orderDir = "asc", int startRec = 0, int pageSize = 10, bool isAll = false, string languageNumber = null, string searchValue = null)
        {
            var data = new List<GroupInfoDto>();
            try
            {

                data = await (from tsg in context.tblSubstanceGroup
                              join tsgt in context.tblSubstanceGroupText on tsg.GroupNumber equals tsgt.GroupNumber
                              join tl in context.tblLanguage on tsgt.Language equals tl.languageNumber into Language
                              from tl in Language.DefaultIfEmpty()
                              select new GroupInfoDto
                              {
                                  Id = tsg.GroupNumber,
                                  GroupName = tsgt.Description,
                                  Language = tl.description,
                                  IsStandard = tsg.StandardYesNo,
                                  IsHide = tsg.ViewYesNo,
                                  LanguageId = tsgt.Language
                              }).ToListAsync();

                if (!string.IsNullOrEmpty(languageNumber) && languageNumber != "null" && languageNumber != "0")
                {
                    data = data.Where(s => s.LanguageId.ToString() == languageNumber).ToList();
                }
                int totalRecords = data.Count;
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrWhiteSpace(searchValue))
                {
                    data = data.Where(p => (p.GroupName != null && p.GroupName.ToLower().Contains(searchValue.ToLower())) ||
                    (p.Id.ToString() != null && p.Id.ToString().ToLower().Contains(searchValue.ToLower()))).ToList();
                }
                data = SortTableGroupList(order, orderDir, data);
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
        private List<GroupInfoDto> SortTableGroupList(string order, string orderDir, List<GroupInfoDto> data)
        {
            List<GroupInfoDto> stateList = new List<GroupInfoDto>();
            try
            {
                switch (order)
                {
                    case "0":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Id).ToList() : data.OrderBy(p => p.Id).ToList();
                        break;
                    case "1":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.GroupName).ToList() : data.OrderBy(p => p.GroupName).ToList();
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
