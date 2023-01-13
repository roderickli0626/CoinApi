using CoinApi.Request_Models;
using CoinApi.Shared;

namespace CoinApi.Services.QuestionService
{
    public interface IQuestionService
    {
        Task<ApiResponse> AddQuestion(QuestionInfoVM questionInfoVM);
        Task<ApiResponse> GetAllQuestion(string search, string order, string orderDir, int startRec, int pageSize, bool isAll);
        Task<ApiResponse> DeleteQuestion(int id);
        Task<ApiResponse> GetQuestionInfoById();
    }
}
