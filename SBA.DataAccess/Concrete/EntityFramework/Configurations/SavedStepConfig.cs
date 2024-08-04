using Core.Entities.Concrete.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBA.DataAccess.Concrete.EntityFramework.Configurations.BaseConfig;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class SavedStepConfig : BaseEntityTypeConfig<SavedStep>
    {
        public void Configure(EntityTypeBuilder<SavedStep> builder)
        {
            builder.ToTable("SavedSteps");

            builder.Property(x => x.TotalBalance).HasPrecision(7, 2);
            builder.HasOne(p => p.System).WithMany(x => x.SavedSteps).HasForeignKey(x => x.BetSystemId);
        }
    }
}
