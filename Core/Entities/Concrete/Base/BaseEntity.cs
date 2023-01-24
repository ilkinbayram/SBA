using Core.Abstract;
using Core.Entities.Abstract;
using Core.Resources.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Concrete.Base
{
    public class BaseEntity : IBaseEntity, IIdentifier
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int Id { get; set; }

        [BsonElement("modelType")]
        public ProjectModelType ModelType { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }
    }
}
