using Core.Entities.Concrete.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class ComboBetPredictionConfig : IEntityTypeConfiguration<ComboBetPrediction>
    {
        public void Configure(EntityTypeBuilder<ComboBetPrediction> builder)
        {
            builder.ToTable("ComboBetPredictions");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(p => p.ComboBet).WithMany(x => x.ComboBetPredictions).HasForeignKey(x => x.ComboBetId);
            builder.HasOne(p => p.Prediction).WithMany(x => x.ComboBetPredictions).HasForeignKey(x => x.PredictionId);
        }
    }
}
