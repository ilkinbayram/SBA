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
