using Core.Concrete.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations.BaseConfig
{
    public abstract class BaseEntityTypeConfig<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {

            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(azerbaycanTime);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(azerbaycanTime);
        }
    }
}
