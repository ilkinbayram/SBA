using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBA.ExternalDataAccess.Concrete.EntityFramework.Configurations
{
    public class MatchIdentifierConfig : IEntityTypeConfiguration<MatchIdentifier>
    {
        public void Configure(EntityTypeBuilder<MatchIdentifier> builder)
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            builder.ToTable("MatchIdentifiers");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CreatedBy).HasMaxLength(100).HasDefaultValue("System.Admin");
            builder.Property(x => x.ModifiedBy).HasMaxLength(100).HasDefaultValue("System.Admin");

            builder.Property(x => x.HT_Result).HasMaxLength(20);
            builder.Property(x => x.FT_Result).HasMaxLength(20);

            builder.Property(x => x.CreatedDateTime).HasDefaultValue(azerbaycanTime.Date);
            builder.Property(x => x.ModifiedDateTime).HasDefaultValue(azerbaycanTime.Date);

            builder.Property(x => x.Serial).HasDefaultValue(0);
            builder.Property(x => x.MatchDateTime).HasDefaultValue(azerbaycanTime.Date);
            builder.Property(p => p.HomeTeam).HasMaxLength(100);
            builder.Property(p => p.AwayTeam).HasMaxLength(100);

            // Unique index for Serial column
            builder.HasMany(p => p.Forecasts).WithOne(x => x.MatchIdentifier).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => x.Serial).IsUnique();
        }
    }
}
