using Core.Entities.Concrete.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBA.DataAccess.Concrete.EntityFramework.Configurations.BaseConfig;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class BetSystemConfig : BaseEntityTypeConfig<BetSystem>
    {
        public void Configure(EntityTypeBuilder<BetSystem> builder)
        {
            builder.ToTable("BetSystems");

            string uniqueName = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();

            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(100).HasDefaultValue($"BetSystem-{uniqueName}");
            builder.Property(x => x.AcceptedOdd).HasPrecision(7, 2);
            builder.Property(x => x.StartingAmount).HasPrecision(7, 2);

            builder.HasMany(p => p.Steps).WithOne(x => x.System).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.SavedSteps).WithOne(x => x.System).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Bundles).WithOne(x => x.System).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
