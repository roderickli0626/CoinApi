using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Enums;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.FileStorageService;
using CoinApi.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using static CoinApi.Shared.ApiFunctions;

namespace CoinApi.Services.ModuleService
{
    public class ModuleService : IModuleService
    {
        private readonly CoinApiContext context;
        private readonly IFileStorageService _fileStorageService;

        public ModuleService(CoinApiContext context, IFileStorageService fileStorageService)
        {
            this.context = context;
            _fileStorageService = fileStorageService;
        }
        public async Task<ApiResponse> AddModule(AddModuleVM entity)
        {
            try
            {
                byte[] bytes = null;
                if (entity == null)
                    return ApiErrorResponse("Module not found.");

                var chkExist = context.tblModules.FirstOrDefault(s => s.NameModule.Trim().ToLower() == entity.NameModule.Trim().ToLower());
                if (chkExist != null)
                    return ApiErrorResponse("Please enter unique module name.");

                if (!string.IsNullOrEmpty(entity.File))
                {
                    var saveProfileImageFileName = _fileStorageService.SaveFileFromBase64String(DirectoryEnum.ProfileDirectoryName, entity.File, ".jpeg");
                    bytes = Encoding.ASCII.GetBytes(entity.File);
                    //entity.File = saveProfileImageFileName;
                }

                tblModules tblModules = new tblModules()
                {
                    GroupNumberID = entity.GroupNumberID,
                    NameModule = entity.NameModule,
                    Description = entity.Description,
                    Price = entity.Price,
                    ProductNumber = entity.ProductNumber,
                    File = entity.File,
                    SubscriptionDescription = entity.SubscriptionDescription,
                    IsSubscription = entity.IsSubscription,
                    Color = entity.Color,
                    CreatedDatetime = DateTime.UtcNow,
                };
                context.tblModules.Add(tblModules);
                await context.SaveChangesAsync();
                if (!string.IsNullOrEmpty(entity.ModulePoints))
                {
                    List<ModulePointVM> pointInfo = JsonConvert.DeserializeObject<List<ModulePointVM>>(entity.ModulePoints);
                    foreach (var item in pointInfo)
                    {
                        tblModulePoints tblModulePoints = new tblModulePoints()
                        {
                            Point = item.Point,
                            ModuleId = tblModules.ModuleID,
                            GroupNumber = item.GroupNumber
                        };
                        context.tblModulePoints.AddRange(tblModulePoints);
                        await context.SaveChangesAsync();
                    }
                }
                if (!string.IsNullOrEmpty(entity.ModuleSubscriptionPoints))
                {
                    List<ModulePointVM> subpointInfo = JsonConvert.DeserializeObject<List<ModulePointVM>>(entity.ModuleSubscriptionPoints);
                    foreach (var item in subpointInfo)
                    {
                        tblModuleSubScriptionPoint tblModulesubPoints = new tblModuleSubScriptionPoint()
                        {
                            Point = item.Point,
                            Description = item.Description,
                            ModuleId = tblModules.ModuleID
                        };
                        context.tblModuleSubScriptionPoint.AddRange(tblModulesubPoints);
                        await context.SaveChangesAsync();
                    }
                }
                return ApiSuccessResponses(null, "Module create successfully.");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public string ToVarbinary(byte[] data)
        {
            var sb = new StringBuilder((data.Length * 2) + 2);
            sb.Append("0x");

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("X2"));
            }

            return sb.ToString();
        }
        public async Task<ApiResponse> UpdateModule(AddModuleVM entity)
        {
            try
            {

                byte[] bytes = null;
                List<int> modulePointIds = new List<int>();
                List<int> moduleSubPointIds = new List<int>();
                string profilePhoto = string.Empty;
                if (entity == null)
                    return ApiErrorResponse("Module not found.");

                tblModules? moduleInfo = context.tblModules.FirstOrDefault(x => x.ModuleID == entity.ModuleID);
                if (moduleInfo == null)
                    return ApiErrorResponse("Module not found.");

                tblModules? checkModule = context.tblModules.FirstOrDefault(x => x.NameModule.Trim().ToLower() == entity.NameModule.Trim().ToLower() && x.ModuleID != entity.ModuleID);
                if (checkModule != null)
                    return ApiErrorResponse("Please enter unique module name.");

                if (!string.IsNullOrEmpty(entity.File))
                {
                    profilePhoto = _fileStorageService.SaveFileFromBase64String(DirectoryEnum.ProfileDirectoryName, entity.File, ".jpeg");
                    bytes = Encoding.ASCII.GetBytes(entity.File);
                }

                moduleInfo.GroupNumberID = entity.GroupNumberID;
                moduleInfo.NameModule = entity.NameModule;
                moduleInfo.Description = entity.Description;
                moduleInfo.Price = entity.Price;
                moduleInfo.ProductNumber = entity.ProductNumber;
                moduleInfo.IsSubscription = entity.IsSubscription;
                moduleInfo.SubscriptionDescription = "";
                moduleInfo.File = (string.IsNullOrEmpty(entity.File) ? moduleInfo.File : entity.File);
                moduleInfo.Color = entity.Color;
                moduleInfo.UpdatedDatetime = DateTime.UtcNow;
                moduleInfo.CreatedDatetime = moduleInfo.CreatedDatetime == null ? DateTime.UtcNow : moduleInfo.CreatedDatetime;
                context.tblModules.Update(moduleInfo);
                await context.SaveChangesAsync();


                if (!string.IsNullOrEmpty(entity.ModulePoints))
                {
                    List<ModulePointVM> pointInfo = JsonConvert.DeserializeObject<List<ModulePointVM>>(entity.ModulePoints);
                    foreach (var item in pointInfo)
                    {
                        var chkExist = context.tblModulePoints.FirstOrDefault(s => s.Id == item.Id);
                        if (chkExist == null)
                        {
                            tblModulePoints tblModulePoints = new tblModulePoints()
                            {
                                Point = item.Point,
                                ModuleId = entity.ModuleID,
                                GroupNumber = item.GroupNumber
                            };
                            context.tblModulePoints.AddRange(tblModulePoints);
                            await context.SaveChangesAsync();
                            modulePointIds.Add(tblModulePoints.Id);
                        }
                        else
                        {
                            chkExist.Point = item.Point;
                            chkExist.GroupNumber = item.GroupNumber;
                            await context.SaveChangesAsync();
                            modulePointIds.Add(item.Id);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(entity.ModuleSubscriptionPoints))
                {
                    List<ModulePointVM> subpointInfo = JsonConvert.DeserializeObject<List<ModulePointVM>>(entity.ModuleSubscriptionPoints);
                    foreach (var item in subpointInfo)
                    {
                        var chkExist = context.tblModuleSubScriptionPoint.FirstOrDefault(s => s.Id == item.Id);
                        if (chkExist == null)
                        {
                            tblModuleSubScriptionPoint tblModulesubPoints = new tblModuleSubScriptionPoint()
                            {
                                Point = item.Point,
                                Description = item.Description,
                                ModuleId = entity.ModuleID
                            };
                            context.tblModuleSubScriptionPoint.AddRange(tblModulesubPoints);
                            await context.SaveChangesAsync();
                            moduleSubPointIds.Add(tblModulesubPoints.Id);
                        }
                        else
                        {
                            chkExist.Point = item.Point;
                            chkExist.Description = item.Description;
                            await context.SaveChangesAsync();
                            moduleSubPointIds.Add(item.Id);
                        }

                    }
                }
                var removePoints = await context.tblModulePoints.Where(s => !modulePointIds.Contains(s.Id) && s.ModuleId == entity.ModuleID).ToListAsync();
                context.tblModulePoints.RemoveRange(removePoints);
                await context.SaveChangesAsync();

                var removesubPoints = await context.tblModuleSubScriptionPoint.Where(s => !moduleSubPointIds.Contains(s.Id) && s.ModuleId == entity.ModuleID).ToListAsync();
                context.tblModuleSubScriptionPoint.RemoveRange(removesubPoints);
                await context.SaveChangesAsync();
                return ApiSuccessResponses(null, "Module successfully updated.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ApiResponse> GetModuleInfoById(int id)
        {

            ModuleDataVM moduleDataVM = new ModuleDataVM();
            List<int?> substanceId = new List<int?>();
            moduleDataVM.ModuleInfo = await context.tblModules.FirstOrDefaultAsync(s => s.ModuleID == id);
            if (moduleDataVM.ModuleInfo == null)
                return ApiErrorResponse("Please enter valid module.");


            //moduleDataVM.ModuleInfo.File = moduleDataVM.ModuleInfo.ModuleFile != null ? ("data:image/png;base64," + Convert.ToBase64String(moduleDataVM.ModuleInfo.ModuleFile)) : "";
            moduleDataVM.GroupName = context.tblSubstanceGroupText.FirstOrDefault(s => s.GroupNumber == moduleDataVM.ModuleInfo.GroupNumberID)?.Description;
            substanceId = context.tblSubstanceForGroup.Where(s => s.GroupNumber == moduleDataVM.ModuleInfo.GroupNumberID).Select(s => s.SubstanceID).ToList();
            moduleDataVM.SubStanceGroupName = String.Join(",", context.tblSubstanceText.Where(s => substanceId.Contains(s.SubstanceID)).Select(s => s.Description).ToList());
            //moduleDataVM.ModuleInfo.File = (string.IsNullOrEmpty(moduleDataVM.ModuleInfo.File)) ? "" : _fileStorageService.GetFullImageUrl(DirectoryEnum.ProfileDirectoryName, moduleDataVM.ModuleInfo.File);
            moduleDataVM.ModulePoints = await context.tblModulePoints.Where(s => s.ModuleId == moduleDataVM.ModuleInfo.ModuleID).Select(s => new ModulePointVM
            {
                Id = s.Id,
                Point = s.Point,
                GroupNumber = s.GroupNumber
            }).ToListAsync();
            moduleDataVM.ModuleSubPoints = await context.tblModuleSubScriptionPoint.Where(s => s.ModuleId == moduleDataVM.ModuleInfo.ModuleID).Select(s => new ModulePointVM
            {
                Id = s.Id,
                Point = s.Point,
                Description = s.Description ?? ""
            }).ToListAsync();

            return ApiSuccessResponse(moduleDataVM);
        }
        public async Task<ApiResponse> DeleteModule(int id)
        {
            var getModuleInfo = await context.tblModules.FirstOrDefaultAsync(s => s.ModuleID == id);
            if (getModuleInfo == null)
                return ApiErrorResponse("Please enter valid module.");

            var modulePoints = await context.tblModulePoints.Where(s => s.ModuleId == id).ToListAsync();
            var moduleSubPoints = await context.tblModuleSubScriptionPoint.Where(s => s.ModuleId == id).ToListAsync();
            context.tblModuleSubScriptionPoint.RemoveRange(moduleSubPoints);
            context.tblModulePoints.RemoveRange(modulePoints);
            context.tblModules.Remove(getModuleInfo);
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "Module successfully deleted.");
        }
        public async Task<ApiResponse> GetGroupInfo()
        {
            var getModuleInfo = await context.tblSubstanceGroup.ToListAsync();
            return ApiSuccessResponse(getModuleInfo);
        }
        public async Task<ApiResponse> GetGroupDescriptionInfo()
        {
            var getModuleInfo = await context.tblSubstanceGroupText.ToListAsync();
            return ApiSuccessResponse(getModuleInfo);
        }
        public async Task<ApiResponse> GetAllModules(string search = null, string order = "0", string orderDir = "asc", int startRec = 0, int pageSize = 10, bool isAll = false)
        {
            var data = new List<AllModuleDataVM>();
            try
            {
                var getmodulePoint = await context.tblModulePoints.Select(s => new
                {
                    Id = s.Id,
                    Point = s.Point,
                    ModuleId = s.ModuleId
                }).ToListAsync();
                var getmodulesubPoint = await context.tblModuleSubScriptionPoint.Select(s => new
                {
                    Id = s.Id,
                    Point = s.Point,
                    Description = s.Description,
                    ModuleId = s.ModuleId
                }).ToListAsync();
                data = await (from tm in context.tblModules
                              join tst in context.tblSubstanceGroupText on tm.GroupNumberID equals tst.Id into Group
                              from tst in Group.DefaultIfEmpty()
                              select new AllModuleDataVM
                              {
                                  ModuleID = tm.ModuleID,
                                  GroupNumberID = tm.GroupNumberID,
                                  NameModule = tm.NameModule,
                                  Description = tm.Description,
                                  Price = tm.Price,
                                  ProductNumber = tm.ProductNumber,
                                  File = tm.File,
                                  SubscriptionDescription = tm.SubscriptionDescription,
                                  IsSubscription = tm.IsSubscription,
                                  Color = tm.Color,
                                  GroupDescription = tst.Description
                              }).ToListAsync();
                //data.ForEach(s => s.File = (string.IsNullOrEmpty(s.File)) ? "" : _fileStorageService.GetFullImageUrl(DirectoryEnum.ProfileDirectoryName, s.File));
                data.ForEach(s => s.ModulePoints = getmodulePoint.Where(a => a.ModuleId == s.ModuleID).Select(s => new ModulePointVM
                {
                    Id = s.Id,
                    Point = s.Point
                }).ToList());
                data.ForEach(s => s.ModuleSubPoints = getmodulesubPoint.Where(a => a.ModuleId == s.ModuleID).Select(s => new ModulePointVM
                {
                    Id = s.Id,
                    Point = s.Point,
                    Description = s.Description
                }).ToList());


                int totalRecords = data.Count;
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => (p.GroupNumberID != null && p.GroupNumberID.Value.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.NameModule != null && p.NameModule.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.GroupDescription != null && p.GroupDescription.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.Description != null && p.Description.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.Price != null && p.Price.ToString().ToLower().Contains(search.ToLower()))).ToList();
                }
                data = SortTableModuleList(order, orderDir, data);
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
        private List<AllModuleDataVM> SortTableModuleList(string order, string orderDir, List<AllModuleDataVM> data)
        {
            List<AllModuleDataVM> stateList = new List<AllModuleDataVM>();
            try
            {
                switch (order)
                {
                    case "0":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.NameModule).ToList() : data.OrderBy(p => p.NameModule).ToList();
                        break;
                    case "1":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.GroupDescription).ToList() : data.OrderBy(p => p.GroupDescription).ToList();
                        break;
                    case "2":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Description).ToList() : data.OrderBy(p => p.Description).ToList();
                        break;
                    case "3":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Price).ToList() : data.OrderBy(p => p.Price).ToList();
                        break;
                    default:
                        stateList = data.OrderByDescending(p => p.ModuleID).ToList();
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
