using Core.Entities.Concrete.System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SBA.DataAccess.Concrete.EntityFramework.Configurations.BaseConfig;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class StepConfig : BaseEntityTypeConfig<Step>
    {
        public void Configure(EntityTypeBuilder<Step> builder)
        {
            builder.ToTable("Steps");

            builder.Property(x => x.IsSuccess).HasDefaultValue(false);

            builder.Property(x => x.InsuredBetAmount).HasPrecision(7, 2);
            builder.Property(x => x.TotalBalance).HasPrecision(7, 2);

            builder.HasOne(p => p.System).WithMany(x => x.Steps).HasForeignKey(x => x.BetSystemId);
        }
    }
}
