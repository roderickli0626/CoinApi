using CoinApi.DB_Models;
using CoinApi.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace CoinApi.Context
{
    public partial class CoinApiContext : DbContext
    {
        public CoinApiContext(DbContextOptions<CoinApiContext> options)
            : base(options)
        {
        }

        public DbSet<tblLanguage> tblLanguage { get; set; }
        public DbSet<tblSubstance> tblSubstance { get; set; }
        public DbSet<tblSubstanceForGroup> tblSubstanceForGroup { get; set; }
        public DbSet<tblSubstanceGroup> tblSubstanceGroup { get; set; }
        public DbSet<tblSubstanceGroupText> tblSubstanceGroupText { get; set; }
        public DbSet<tblSubstanceText> tblSubstanceText { get; set; }
        public DbSet<tblUser> tblUser { get; set; }
        public DbSet<tblCountry> tblCountry { get; set; }
        public DbSet<tblCategory> tblCategory { get; set; }
        public DbSet<tblModules> tblModules { get; set; }
        public DbSet<tblModulePoints> tblModulePoints { get; set; }
        public DbSet<tblModuleSubScriptionPoint> tblModuleSubScriptionPoint { get; set; }
        public DbSet<tblGroupQuestionInfo> tblGroupQuestionInfo { get; set; }
        public DbSet<tblQuestions> tblQuestions { get; set; }
        public DbSet<tblGroupQuestions> tblGroupQuestions { get; set; }
        public DbSet<tblCoupons> tblCoupons { get; set; }
        public DbSet<tblOrders> tblOrders { get; set; }
        public DbSet<tblLanguageGUI> tblLanguageGUI { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblUser>().HasData(
                new tblUser()
                {
                    UserID = -1,
                    Email = "superadmin@gmail.com",
                    Password = PasswordHelper.Encrypt("Admin@123"),
                    IsAdmin = true,
                    Location = string.Empty,
                    Phone = string.Empty,
                    Title = string.Empty,
                    SurName = string.Empty
                }
                );

            modelBuilder.Entity<tblLanguage>().HasKey(L => L.languageNumber);
            modelBuilder.Entity<tblSubstance>().HasKey(s => s.SubstanceID);
            modelBuilder.Entity<tblSubstanceForGroup>().HasKey(s => s.Id);
            modelBuilder.Entity<tblSubstanceGroup>().HasKey(s => s.GroupNumber);
            modelBuilder.Entity<tblSubstanceGroupText>().HasKey(s => s.Id);
            modelBuilder.Entity<tblSubstanceText>().HasKey(s => s.Id);
            modelBuilder.Entity<tblUser>().HasKey(u => u.UserID);
            modelBuilder.Entity<tblModulePoints>().HasKey(u => u.Id);
            modelBuilder.Entity<tblModuleSubScriptionPoint>().HasKey(u => u.Id);
            modelBuilder.Entity<tblQuestions>().HasKey(u => u.Id);
            modelBuilder.Entity<tblGroupQuestions>().HasKey(u => u.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
