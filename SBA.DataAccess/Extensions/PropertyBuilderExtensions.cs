using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBA.DataAccess.Helpers.Converters;

namespace SBA.DataAccess.Extensions
{
    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<int> HasDefaultValueWhen(
            this PropertyBuilder<int> propertyBuilder, bool condition)
        {
            var converter = new ConditionalDefaultValueConverter(condition);
            propertyBuilder.HasConversion(converter);

            return propertyBuilder;
        }
    }
}
