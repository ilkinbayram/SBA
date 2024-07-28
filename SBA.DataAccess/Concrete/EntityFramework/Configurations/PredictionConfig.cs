using Core.Entities.Concrete.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class PredictionConfig : IEntityTypeConfiguration<Prediction>
    {
        public void Configure(EntityTypeBuilder<Prediction> builder)
        {
            builder.ToTable("Predictions");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(DateTime.Now);

            builder.Property(x => x.Odd).HasPrecision(7, 2);

            builder.HasMany(p => p.ComboBetPredictions).WithOne(x => x.Prediction).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
