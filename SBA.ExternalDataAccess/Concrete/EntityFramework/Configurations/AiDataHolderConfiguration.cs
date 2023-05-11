using Core.Entities.Concrete.ExternalDbEntities;
using Core.Resources.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class AiDataHolderConfiguration : IEntityTypeConfiguration<AiDataHolder>
    {
        public void Configure(EntityTypeBuilder<AiDataHolder> builder)
        {
            builder.ToTable("AiDataHolders");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.DataType).HasDefaultValue(AiDataType.None);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(DateTime.Now);
        }
    }
}
