using Core.Entities.Concrete;
using Core.Resources.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
