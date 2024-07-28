using Core.Entities.Concrete;
using Core.Entities.Concrete.System;
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

            modelBuilder.ApplyConfiguration(new BetSystemConfig());
            modelBuilder.ApplyConfiguration(new StepConfig());
            modelBuilder.ApplyConfiguration(new SavedStepConfig());
            modelBuilder.ApplyConfiguration(new BundleConfig());
            modelBuilder.ApplyConfiguration(new ComboBetConfig());
            modelBuilder.ApplyConfiguration(new PredictionConfig());
            modelBuilder.ApplyConfiguration(new ComboBetPredictionConfig());

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

        public DbSet<BetSystem> BetSystems { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<SavedStep> SavedSteps { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<ComboBet> ComboBets { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<ComboBetPrediction> ComboBetPredictions { get; set; }
    }
}
