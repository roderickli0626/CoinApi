namespace CoinApi.DB_Models
{
    public partial class tblSubstanceText
    {
        public int Id { get; set; }
        public int? SubstanceID { get; set; }
        public string? Description { get; set; }
        public int? Language { get; set; }

    }
}
