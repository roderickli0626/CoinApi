namespace CoinApi.Shared
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public dynamic ResponseData { get; set; }
        public ApiErrorCode ErrorCode { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
