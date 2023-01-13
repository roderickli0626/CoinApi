namespace CoinApi.DB_Models
{
    public partial class tblSubstance
    {
        public int SubstanceID { get; set; }
        public bool? Hidde { get; set; }
        public byte[]? WavFile { get; set; }
        //public string? WavFile { get; set; }
        public bool? StandardYesNo { get; set; }
        public DateTime DateCreated { get; set; }
        public string Duration { get; set; }
    }
}
