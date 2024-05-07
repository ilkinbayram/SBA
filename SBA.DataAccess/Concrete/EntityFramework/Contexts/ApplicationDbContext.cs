using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using SBA.DataAccess.Concrete.EntityFramework.Configurations;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MatchBetConfiguration());
            modelBuilder.ApplyConfiguration(new PerformanceOverallConfig());
            modelBuilder.ApplyConfiguration(new FilterResultConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-4AP15T3;Initial Catalog=SBAnalyserDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }


        public DbSet<MatchBet> MatchBets { get; set; }
        public DbSet<PerformanceOverall> PerformanceOveralls { get; set; }
        public DbSet<FilterResult> FilterResults { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
