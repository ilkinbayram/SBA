using Core.Entities.Concrete.ComplexModels.SqlModelHelpers;

namespace Core.Entities.Concrete.ComplexModels.RequestModelHelpers
{
    public class PerformanceResponseModel : PerformanceModelDto
    {
        public string TeamName { get; set; }
    }
}
