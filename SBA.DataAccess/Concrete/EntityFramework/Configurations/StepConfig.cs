using Core.Entities.Concrete.System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class StepConfig : IEntityTypeConfiguration<Step>
    {
        public void Configure(EntityTypeBuilder<Step> builder)
        {
            builder.ToTable("Steps");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(DateTime.Now);

            builder.Property(x => x.IsSuccess).HasDefaultValue(false);

            builder.Property(x => x.InsuredBetAmount).HasPrecision(7, 2);
            builder.Property(x => x.TotalBalance).HasPrecision(7, 2);

            builder.HasOne(p => p.System).WithMany(x => x.Steps).HasForeignKey(x => x.BetSystemId);
            builder.HasMany(p => p.Bundles).WithOne(x => x.Step).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
