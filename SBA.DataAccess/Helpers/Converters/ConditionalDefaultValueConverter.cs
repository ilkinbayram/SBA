using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SBA.DataAccess.Helpers.Converters
{
    public class ConditionalDefaultValueConverter : ValueConverter<int, int>
    {
        public ConditionalDefaultValueConverter(bool condition)
            : base(
                v => condition ? v : -1, // Convert from model to provider (database) representation
                v => v) // Convert from provider to model representation
        {
        }
    }
}
