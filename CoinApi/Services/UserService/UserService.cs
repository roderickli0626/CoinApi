using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Helpers;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static CoinApi.Shared.ApiFunctions;

namespace CoinApi.Services.UserService
{
    public class UserService : Service<tblUser>, IUserService
    {
        public UserService(CoinApiContext context) : base(context)
        {
        }
        public override tblUser Create(tblUser entity)
        {
            tblUser language = context.tblUser.Add(entity).Entity;
            context.SaveChanges();
            return language;
        }
        public async Task<ApiResponse> CreateUser(tblUser entity)
        {
            var chkExist = context.tblUser.FirstOrDefault(s => s.Email.Trim() == entity.Email.Trim());
            if (chkExist != null)
                return ApiErrorResponse("Please enter unique email address.");

            if (!string.IsNullOrEmpty(entity.Password))
                entity.Password = PasswordHelper.Encrypt(entity.Password);

            tblUser language = context.tblUser.Add(entity).Entity;
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "User create successfully.");
        }
        public override bool Delete(int id)
        {
            context.tblUser.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblUser> GetAll()
        {
            return context.tblUser.ToList();
        }
        public override List<tblUser> GetBy(Func<tblUser, bool> predicate)
        {
            return context.tblUser
                .Where(predicate)
                .ToList();
        }
        public override tblUser GetById(int id)
        {
            return context.tblUser
                .FirstOrDefault(x => x.UserID == id);
        }
        public async Task<tblUser> GetByEmailAsync(string email)
        {
            return await context.tblUser.Include(s => s.tblCategory).FirstOrDefaultAsync(u => u.Email == email);
        }
        public override bool Update(tblUser entity)
        {
            if (entity == null) return false;
            tblUser? user = context.tblUser.FirstOrDefault(x => x.UserID == entity.UserID);
            if (user == null) return false;
            user.Adress = entity.Adress;
            user.DeviceNumber = entity.DeviceNumber;
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.ActiveAcount = entity.ActiveAcount;
            user.City = entity.City;
            user.Email = entity.Email;
            user.Country = entity.Country;
            user.LanguageNumber = entity.LanguageNumber;
            user.Password = entity.Password;

            context.tblUser.Update(user);
            context.SaveChanges();
            return true;

        }
        public async Task<ApiResponse> UpdateUser(tblUser entity)
        {
            if (entity == null)
                return ApiErrorResponse("User not found.");

            tblUser? user = context.tblUser.FirstOrDefault(x => x.UserID == entity.UserID);
            if (user == null)
                return ApiErrorResponse("User not found.");

            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Adress = entity.Adress;
            user.CountryId = entity.CountryId;
            user.Location = entity.Location;
            user.Phone = entity.Phone;
            user.PostCode = entity.PostCode;
            user.SurName = entity.SurName;
            user.Title = entity.Title;
            user.Company = entity.Company;
            user.Department = entity.Department;
            user.Salutation = entity.Salutation;
            user.AddressSalutation = entity.AddressSalutation;
            user.AddressTitle = entity.AddressTitle;
            user.AddressFirstName = entity.AddressFirstName;
            user.AddressLastName = entity.AddressLastName;

            context.tblUser.Update(user);
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "User successfully updated.");

        }
        public async Task<ApiResponse> UpdatePassword(ChangePasswordDto changePassword)
        {
            string userId = PasswordHelper.Decrypt(changePassword.Id);
            var userEntity = await context.tblUser.FirstOrDefaultAsync(s => s.UserID.ToString() == userId);
            if (userEntity != null)
            {
                userEntity.Password = PasswordHelper.Encrypt(changePassword.Password);
                context.tblUser.Update(userEntity);
                await context.SaveChangesAsync();
                return ApiSuccessResponses(userEntity, "Password updated successfully.");
            }

            return ApiValidationResponse("Not Found");
        }
        public async Task<ApiResponse> GetUserInfoById(int id)
        {
            //var getUserInfo = await context.tblUser.FirstOrDefaultAsync(s => s.UserID == id);
            var getUserInfo = await (from tm in context.tblUser
                                     join tc in context.tblCountry on tm.CountryId equals tc.CountryId into Country
                                     from tc in Country.DefaultIfEmpty()
                                     where tm.UserID == id
                                     select new
                                     {
                                         UserData = tm,
                                         CountryName = tc.Name
                                     }).FirstOrDefaultAsync();
            if (getUserInfo == null)
                return ApiErrorResponse("Please enter valid user.");

            return ApiSuccessResponse(getUserInfo);
        }
        public async Task<ApiResponse> DeleteUser(int id)
        {
            var getUserInfo = await context.tblUser.FirstOrDefaultAsync(s => s.UserID == id);
            if (getUserInfo == null)
                return ApiErrorResponse("Please enter valid user.");

            context.tblUser.Remove(getUserInfo);
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "User successfully deleted.");
        }
        public async Task<ApiResponse> SetEnableLogin(int id)
        {
            var getUserInfo = await context.tblUser.FirstOrDefaultAsync(s => s.UserID == id);
            if (getUserInfo == null)
                return ApiErrorResponse("Please enter valid user.");

            getUserInfo.IsEnableLogin = Convert.ToBoolean(getUserInfo.IsEnableLogin) ? false : true;
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "User " + (Convert.ToBoolean(getUserInfo.IsEnableLogin) ? "activate" : "deactivate") + " successfully.");
        }
        public async Task<ApiResponse> GetAllUsers(string search = null, string order = "0", string orderDir = "asc", int startRec = 0, int pageSize = 10, bool isAll = false)
        {
            var data = new List<UserVM>();
            try
            {

                data = await (from tu in context.tblUser
                              join tc in context.tblCategory on tu.CategoryId equals tc.CategoryId into Category
                              from tc in Category.DefaultIfEmpty()
                              select new UserVM
                              {
                                  UserID = tu.UserID,
                                  FirstName = tu.FirstName,
                                  SurName = tu.SurName,
                                  LastName = tu.LastName,
                                  Email = tu.Email,
                                  Phone = tu.Phone,
                                  Adress = tu.Adress,
                                  CategoryName = tc.Name,
                                  IsEnable = tu.IsEnableLogin
                              }).ToListAsync();


                int totalRecords = data.Count;
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => (p.FirstName != null && p.FirstName.ToLower().Contains(search.ToLower())) ||
                    (p.SurName != null && p.SurName.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.LastName != null && p.LastName.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.CategoryName != null && p.CategoryName.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.Phone != null && p.Phone.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.Email != null && p.Email.ToString().ToLower().Contains(search.ToLower()))).ToList();
                }
                data = SortTableUserList(order, orderDir, data);
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
        private List<UserVM> SortTableUserList(string order, string orderDir, List<UserVM> data)
        {
            List<UserVM> stateList = new List<UserVM>();
            try
            {
                switch (order)
                {
                    case "0":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UserID).ToList() : data.OrderBy(p => p.UserID).ToList();
                        break;
                    case "1":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.FirstName).ToList() : data.OrderBy(p => p.FirstName).ToList();
                        break;
                    case "2":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Phone).ToList() : data.OrderBy(p => p.Phone).ToList();
                        break;
                    case "3":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Email).ToList() : data.OrderBy(p => p.Email).ToList();
                        break;
                    case "4":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CategoryName).ToList() : data.OrderBy(p => p.CategoryName).ToList();
                        break;
                    default:
                        stateList = data.OrderByDescending(p => p.UserID).ToList();
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
