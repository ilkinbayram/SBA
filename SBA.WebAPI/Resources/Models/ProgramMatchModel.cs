using Core.Entities.Concrete.ComplexModels.Program;

namespace SBA.WebAPI.Resources.Models
{
    public class ProgramMatchModel : Match
    {
        public int MatchHour { get; set; }
        public int MatchMinute { get; set; }
    }
}
