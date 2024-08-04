using Core.Entities.Concrete.System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SBA.DataAccess.Concrete.EntityFramework.Configurations.BaseConfig;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class ComboBetConfig : BaseEntityTypeConfig<ComboBet>
    {
        public void Configure(EntityTypeBuilder<ComboBet> builder)
        {
            builder.ToTable("ComboBets");

            builder.Property(x => x.IsInsuredBet).HasDefaultValue(false);
            builder.Property(x => x.TotalOdd).HasPrecision(7, 2);

            builder.HasOne(p => p.Bundle).WithMany(x => x.ComboBets).HasForeignKey(x => x.BundleId);
            builder.HasMany(p => p.ComboBetPredictions).WithOne(x => x.ComboBet).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
