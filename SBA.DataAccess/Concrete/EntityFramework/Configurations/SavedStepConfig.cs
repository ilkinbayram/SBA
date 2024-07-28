using Core.Entities.Concrete.System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class SavedStepConfig : IEntityTypeConfiguration<SavedStep>
    {
        public void Configure(EntityTypeBuilder<SavedStep> builder)
        {
            builder.ToTable("SavedSteps");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(DateTime.Now);

            builder.Property(x => x.TotalBalance).HasPrecision(7, 2);
        }
    }
}
