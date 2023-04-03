using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.ComplexModels.RequestModelHelpers
{
    public class ComparisonResponseModel : BaseComparisonDto
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
    }
}
