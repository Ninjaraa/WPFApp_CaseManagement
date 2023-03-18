using Microsoft.EntityFrameworkCore;
using System.IO;
using WPFApp_CaseManagement.Models;
using WPFApp_CaseManagement.Models.Entities;

namespace WPFApp_CaseManagement.Context
{
    internal class DataContext : DbContext
    {
        // Private variable for the connection string to make it more safe
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ninja\Documents\CSharp\WPFApp_CM\WPFApp_CaseManagement\Context\local_db.mdf;Integrated Security=True;Connect Timeout=30";

        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        #region overrides
        // Configuration for the db conn
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //}
        #endregion

        #region entities
        // Exact structure as the db - All entites should be added here
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<SLAEntity> SLAs { get; set; } = null!;
        public DbSet<CaseEntity> Cases { get; set; } = null!;
        public DbSet<CaseDescriptionEntity> CaseDescriptions { get; set; } = null!;
        public DbSet<EmployeeEntity> Employees { get; set; } = null!;

        #endregion
    }
}
