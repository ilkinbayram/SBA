using Core.Entities.Concrete.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBA.DataAccess.Concrete.EntityFramework.Configurations.BaseConfig;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class PredictionConfig : BaseEntityTypeConfig<Prediction>
    {
        public void Configure(EntityTypeBuilder<Prediction> builder)
        {
            builder.ToTable("Predictions");

            builder.Property(x => x.Odd).HasPrecision(7, 2);

            builder.HasMany(p => p.ComboBetPredictions).WithOne(x => x.Prediction).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
