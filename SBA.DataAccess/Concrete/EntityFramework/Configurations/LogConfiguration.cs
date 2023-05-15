using Core.Entities.Concrete;
using Core.Resources.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Path).HasMaxLength(500);

            builder.Property(x => x.CreatedBy).HasMaxLength(200).HasDefaultValue("System.Admin");
            builder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);

            builder.Property(x=>x.Type).HasDefaultValue(LogType.None);
            builder.Property(x=>x.Importance).HasDefaultValue(LogImportance.Info);
        }
    }
}
