using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class FilterResultConfiguration : IEntityTypeConfiguration<FilterResult>
    {
        public void Configure(EntityTypeBuilder<FilterResult> builder)
        {
            builder.ToTable("FilterResults");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.HasIndex(x => x.SerialUniqueID).IsClustered(false).HasName("IX_SerialUniqueID");
        }
    }
}
