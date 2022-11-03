using CoinApi.DB_Models;
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

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblLanguage>().HasKey(L => L.languageNumber);
            modelBuilder.Entity<tblSubstance>().HasKey(s => s.SubstanceID);
            modelBuilder.Entity<tblSubstanceForGroup>().HasKey(s => s.Id);
            modelBuilder.Entity<tblSubstanceGroup>().HasKey(s => s.GroupNumber);
            modelBuilder.Entity<tblSubstanceGroupText>().HasKey(s => s.Id);
            modelBuilder.Entity<tblSubstanceText>().HasKey(s => s.Id);
            modelBuilder.Entity<tblUser>().HasKey(u => u.UserID);
            base.OnModelCreating(modelBuilder);
        }
    }
}
