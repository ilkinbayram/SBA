using Core.Entities.Concrete.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBA.DataAccess.Concrete.EntityFramework.Configurations.BaseConfig;

namespace SBA.DataAccess.Concrete.EntityFramework.Configurations
{
    public class BundleConfig : BaseEntityTypeConfig<Bundle>
    {
        public void Configure(EntityTypeBuilder<Bundle> builder)
        {
            builder.ToTable("Bundles");

            builder.HasOne(p => p.System).WithMany(x => x.Bundles).HasForeignKey(x => x.SystemId);
            builder.HasMany(p => p.ComboBets).WithOne(x => x.Bundle).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
