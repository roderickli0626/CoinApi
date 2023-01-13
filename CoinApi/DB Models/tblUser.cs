using System.ComponentModel.DataAnnotations.Schema;

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
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public int? PostCode { get; set; }
        public string Location { get; set; }
        public int? CountryId { get; set; }
        public string? Company { get; set; }
        public string? Department { get; set; }
        public string? Salutation { get; set; }
        public string? AddressSalutation { get; set; }
        public string? AddressTitle { get; set; }
        public string? AddressFirstName { get; set; }
        public string? AddressLastName { get; set; }
        public bool IsAdmin { get; set; }
        public bool? IsEnableLogin { get; set; }
        [ForeignKey("CategoryId")]
        public virtual tblCategory? tblCategory { get; set; }
        [ForeignKey("CountryId")]
        public virtual tblCountry? tblCountry { get; set; }
    }
}
