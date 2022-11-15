namespace CoinApi.Response_Models
{
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
        //public string role { get; set; }
        public string? refreshToken { get; set; }
        public bool? result { get; set; }
        public int userId { get; set; }
        public int? languageNumber { get; set; }
        public string? deviceNumber { get; set; }
        public string? userName { get; set; }
    }
}
