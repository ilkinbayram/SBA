using Core.Entities.Concrete.System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class ComboBetConfig : IEntityTypeConfiguration<ComboBet>
    {
        public void Configure(EntityTypeBuilder<ComboBet> builder)
        {
            builder.ToTable("ComboBets");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(DateTime.Now);

            builder.Property(x => x.IsInsuredBet).HasDefaultValue(false);
            builder.Property(x => x.TotalOdd).HasPrecision(7, 2);

            builder.HasOne(p => p.Bundle).WithMany(x => x.ComboBets).HasForeignKey(x => x.BundleId);
            builder.HasMany(p => p.ComboBetPredictions).WithOne(x => x.ComboBet).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
