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
            modelBuilder.ApplyConfiguration(new FilterResultConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());
        }


        public DbSet<MatchBet> MatchBets { get; set; }
        public DbSet<FilterResult> FilterResults { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
