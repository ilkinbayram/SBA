using Core.Entities;
using Core.Resources.Enums;



namespace Core.Entities.Dtos.Base
{
    public class BaseDto : IDto
    {
        public long Id { get; set; }
        public ProjectModelType ModelType { get; set; }
    }
}
