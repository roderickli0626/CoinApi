﻿using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.FileStorageService;
using CoinApi.Shared;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.SqlClient;
using static CoinApi.Shared.ApiFunctions;

namespace CoinApi.Services.QuestionService
{
    public class QuestionService : IQuestionService
    {
        private readonly CoinApiContext context;
        private readonly IFileStorageService _fileStorageService;

        public QuestionService(CoinApiContext context, IFileStorageService fileStorageService)
        {
            this.context = context;
            _fileStorageService = fileStorageService;
        }
        public async Task<ApiResponse> AddQuestion(QuestionInfoVM questionInfoVM)
        {
            try
            {
                var getGroupQuestionInfo = await context.tblGroupQuestions.ToListAsync();
                var getQuestionInfo = await context.tblQuestions.ToListAsync();
                var getmainGroupQuestion = await context.tblGroupQuestionInfo.ToListAsync();
                //context.tblGroupQuestions.RemoveRange(getGroupQuestionInfo);
                //context.tblQuestions.RemoveRange(getQuestionInfo);
                //context.tblGroupQuestionInfo.RemoveRange(getmainGroupQuestion);
                await context.SaveChangesAsync();
                List<int> questionIds = new List<int>();
                if (questionInfoVM == null)
                    return ApiErrorResponse("Question not found.");

                if (questionInfoVM.Id == 0)
                {
                    tblGroupQuestionInfo tblGroupQuestionInfo = new tblGroupQuestionInfo()
                    {
                        Title = questionInfoVM.Name
                    };
                    await context.tblGroupQuestionInfo.AddAsync(tblGroupQuestionInfo);
                    await context.SaveChangesAsync();

                    if (questionInfoVM.questionInfos.Count != 0)
                    {
                        foreach (var item in questionInfoVM.questionInfos)
                        {
                            List<int> groupIds = new List<int>();
                            if (!string.IsNullOrEmpty(item.GroupNumber))
                            {
                                groupIds = item.GroupNumber.Split(',').Select(int.Parse).ToList();
                            }

                            tblQuestions tblQuestions = new tblQuestions()
                            {
                                Questions = item.Questions,
                                languageNumber = item.LanguageNumber,
                                GroupQueInfoId = tblGroupQuestionInfo.Id
                            };
                            await context.tblQuestions.AddAsync(tblQuestions);
                            await context.SaveChangesAsync();
                            foreach (var itemGroup in groupIds)
                            {
                                tblGroupQuestions tblGroupQuestions = new tblGroupQuestions()
                                {
                                    QuestionId = tblQuestions.Id,
                                    GroupNumber = itemGroup
                                };
                                await context.tblGroupQuestions.AddAsync(tblGroupQuestions);
                                await context.SaveChangesAsync();
                            }

                        }
                    }
                }
                else
                {
                    foreach (var item in questionInfoVM.questionInfos)
                    {
                        List<int> groupIds = new List<int>();
                        if (!string.IsNullOrEmpty(item.GroupNumber))
                        {
                            groupIds = item.GroupNumber.Split(',').Select(int.Parse).ToList();
                        }
                        var chkExist = context.tblQuestions.FirstOrDefault(s => s.Id == item.Id);
                        if (chkExist != null)
                        {
                            chkExist.Questions = item.Questions;
                        }
                        await context.SaveChangesAsync();
                        var removeGroupInfo = context.tblGroupQuestions.Where(s => s.QuestionId == item.Id).ToList();
                        if (removeGroupInfo.Count != 0)
                        {
                            context.tblGroupQuestions.RemoveRange(removeGroupInfo);
                            await context.SaveChangesAsync();
                        }
                        foreach (var itemGroup in groupIds)
                        {
                            tblGroupQuestions tblGroupQuestions = new tblGroupQuestions()
                            {
                                QuestionId = item.Id,
                                GroupNumber = itemGroup
                            };
                            await context.tblGroupQuestions.AddAsync(tblGroupQuestions);
                            await context.SaveChangesAsync();
                        }

                    }
                    return ApiSuccessResponses(null, "Question update successfully.");
                }

                //foreach (var item in questionInfoVM.GroupNumbers)
                //{
                //    foreach (var QuestionItem in questionIds)
                //    {
                //        tblGroupQuestions tblGroupQuestions = new tblGroupQuestions()
                //        {
                //            GroupNumber = item,
                //            QuestionId = QuestionItem
                //        };
                //        await context.tblGroupQuestions.AddAsync(tblGroupQuestions);
                //        await context.SaveChangesAsync();
                //    }
                //}



                return ApiSuccessResponses(null, "Question create successfully.");
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<ApiResponse> DeleteQuestion(int id)
        {
            //var getGroupInfo = await context.tblGroupQuestionInfo.FirstOrDefaultAsync(s => s.Id == id);
            //if (getGroupInfo == null)
            //    return ApiErrorResponse("Please enter valid module.");

            var getQuestionInfo = await context.tblQuestions.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (getQuestionInfo != null)
            {
                var getGroupQueInfo = await context.tblGroupQuestions.Where(s => s.QuestionId == id).ToListAsync();
                context.tblGroupQuestions.RemoveRange(getGroupQueInfo);
            }

            context.tblQuestions.RemoveRange(getQuestionInfo);
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "Question successfully deleted.");
        }
        public async Task<ApiResponse> GetQuestionInfoById(string? search)
        {
            GroupQuestionInfoVM questionInfo = new GroupQuestionInfoVM();
            List<int> groupQuestionIds = await context.tblGroupQuestionInfo.Select(s => s.Id).ToListAsync();

            List<int> qustionId = new List<int>();
            questionInfo.questionInfos = await context.tblQuestions.OrderByDescending(s => s.Id).Select(s => new QuestionInfo
            {
                Id = s.Id,
                Questions = s.Questions,
                LanguageNumber = s.languageNumber
            }).ToListAsync();
            if (!string.IsNullOrEmpty(search))
            {
                questionInfo.questionInfos = questionInfo.questionInfos.Where(s => s.Questions.ToString().ToLower().Contains(search.ToLower())).ToList();
            }
            //List<int?> questionIds = await context.tblQuestions.Where(s => s.GroupQueInfoId == getQuestionInfo.Id).Select(s => (int?)s.Id).ToListAsync();
            //questionInfo.GroupQuestionInfo = await context.tblGroupQuestions.Where(s => questionIds.Contains(s.QuestionId)).Select(s => new GroupQuestionDataVM
            //{
            //    GroupNumber = s.GroupNumber,
            //    QuestionId = s.QuestionId,

            //}).ToListAsync();
            //questionInfo.Id = getQuestionInfo.Id;
            return ApiSuccessResponse(questionInfo);
        }

        public async Task<ApiResponse> GetQuestionInfoFromId(int id)
        {
            GroupQuestionInfoVM questionInfo = new GroupQuestionInfoVM();
            List<int> groupQuestionIds = await context.tblGroupQuestionInfo.Select(s => s.Id).ToListAsync();


            var groupQuestionInfo = await (from tbq in context.tblGroupQuestions
                                           join tq in context.tblSubstanceGroupText on tbq.GroupNumber equals tq.GroupNumber
                                           where tbq.QuestionId == id
                                           select new
                                           {
                                               Id = tbq.GroupNumber,
                                               GroupName = tq.Description
                                           }).ToListAsync();

            return ApiSuccessResponse(groupQuestionInfo);
        }

        public async Task<ApiResponse> GetAllQuestion(string search = null, string order = "0", string orderDir = "asc", int startRec = 0, int pageSize = 10, bool isAll = false)
        {
            var data = new List<AllQuestionInfoVM>();
            try
            {

                var getQuestionInfo = await context.tblQuestions.ToListAsync();
                data = await (from tm in context.tblGroupQuestionInfo
                              join tst in context.tblGroupQuestions on tm.Id equals tst.QuestionId into Questions
                              from tst in Questions.DefaultIfEmpty()
                              select new AllQuestionInfoVM
                              {
                                  Id = tm.Id,
                                  Title = tm.Title,
                              }).ToListAsync();
                data.ForEach(s => s.QuestionCount = getQuestionInfo.Where(a => a.GroupQueInfoId == s.Id).Count());
                data = data.DistinctBy(s => s.Id).ToList();
                int totalRecords = data.Count;
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => (p.Title != null && p.Title.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.QuestionCount != null && p.QuestionCount.ToString().ToLower().Contains(search.ToLower()))).ToList();
                }
                data = SortTableQuestionist(order, orderDir, data);
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
        private List<AllQuestionInfoVM> SortTableQuestionist(string order, string orderDir, List<AllQuestionInfoVM> data)
        {
            List<AllQuestionInfoVM> stateList = new List<AllQuestionInfoVM>();
            try
            {
                switch (order)
                {
                    case "0":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Title).ToList() : data.OrderBy(p => p.Title).ToList();
                        break;
                    case "1":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.QuestionCount).ToList() : data.OrderBy(p => p.QuestionCount).ToList();
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

        public List<Object> loadDB(DbSyncRequest data)
        {
            string query =
                $"SELECT q.Questions as QuestionContent, q.languageNumber as LanguageNumber, gq.GroupNumber, gqi.Title as QuestionTitle " +
                $"FROM [tblQuestions] AS q INNER JOIN tblGroupQuestions AS gq ON gq.QuestionId = q.Id " +
                $"INNER JOIN tblGroupQuestionInfo AS gqi ON gqi.Id = q.GroupQueInfoId";

            using (SqlConnection conn = new SqlConnection(context.Database.GetConnectionString()))
            {
                List<Object> result = conn.Query<Object>(query).ToList();
                return result;
            }

        }
    }
}
