namespace CoinApi.DB_Models
{
    public partial class tblUser
    {
        public int UserID { get; set; }   
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Adress { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? DeviceNumber { get; set; }
        public int? LanguageNumber { get; set; }
        public bool? ActiveAcount { get; set; }


    }
}
