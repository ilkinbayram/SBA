using Core.Entities.Concrete;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Concrete.SqlEntities.FunctionViewProcModels;
using Microsoft.EntityFrameworkCore;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts
{
    public class ExternalAppDbContext : DbContext
    {
        public ExternalAppDbContext(DbContextOptions<ExternalAppDbContext> options) : base(options)
        {
        }

        public ExternalAppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MatchForecastFM>().HasNoKey();

            modelBuilder.ApplyConfiguration(new ComparisonStatisticsHolderConfig());
            modelBuilder.ApplyConfiguration(new TeamPerformanceStatisticsHolderConfig());
            modelBuilder.ApplyConfiguration(new AverageStatisticsHolderConfig());
            modelBuilder.ApplyConfiguration(new LeagueStatisticsHolderConfig());
            modelBuilder.ApplyConfiguration(new MatchIdentifierConfig());
            modelBuilder.ApplyConfiguration(new MatchOddsHolderConfig());
            modelBuilder.ApplyConfiguration(new StatisticInfoHolderConfig());
            modelBuilder.ApplyConfiguration(new AiDataHolderConfiguration());
            modelBuilder.ApplyConfiguration(new ForecastConfig());
            modelBuilder.ApplyConfiguration(new PossibleForecastConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=SQL6031.site4now.net;Initial Catalog=db_aa6d61_sbaproset;User Id=db_aa6d61_sbaproset_admin;Password=System234327");
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-4AP15T3;Initial Catalog=SbaPromo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        public DbSet<MatchForecastFM> MatchForecastsFM { get; set; }



        public DbSet<MatchIdentifier> MatchIdentifiers { get; set; }
        public DbSet<ComparisonStatisticsHolder> ComparisonStatisticsHolders { get; set; }
        public DbSet<AverageStatisticsHolder> AverageStatisticsHolders { get; set; }
        public DbSet<TeamPerformanceStatisticsHolder> TeamPerformanceStatisticsHolders { get; set; }
        public DbSet<LeagueStatisticsHolder> LeagueStatisticsHolders { get; set; }
        public DbSet<MatchOddsHolder> MatchOddsHolders { get; set; }
        public DbSet<StatisticInfoHolder> StatisticInfoes { get; set; }
        public DbSet<AiDataHolder> AiDataHolders { get; set; }
        public DbSet<Forecast> Forecasts { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<PossibleForecast> PossibleForecasts { get; set; }
    }
}
