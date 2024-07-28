using Core.Entities.Concrete;
using Core.Entities.Concrete.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class BetSystemConfig : IEntityTypeConfiguration<BetSystem>
    {
        public void Configure(EntityTypeBuilder<BetSystem> builder)
        {
            builder.ToTable("BetSystems");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(DateTime.Now);

            string uniqueName = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();

            builder.Property(x => x.Name).HasMaxLength(100).HasDefaultValue($"BetSystem-{uniqueName}");
            builder.Property(x => x.AcceptedOdd).HasPrecision(7, 2);
            builder.Property(x => x.StartingAmount).HasPrecision(7, 2);


            builder.HasMany(p => p.Steps).WithOne(x => x.System).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
