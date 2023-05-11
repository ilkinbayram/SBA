using Core.Entities.Concrete.Base;
using Core.Resources.Enums;

namespace Core.Entities.Concrete.ExternalDbEntities
{
    public class AiDataHolder : Identifier, IEntity
    {
        public int Serial { get; set; }

        public AiDataType DataType { get; set; }

        public string JsonTextContent { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
