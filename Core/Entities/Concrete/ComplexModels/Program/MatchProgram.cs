namespace Core.Entities.Concrete.ComplexModels.Program
{
    public class MatchProgram
    {
        public string Country { get; set; }
        public string League { get; set; }

        public List<Match> Matches { get; set; }
    }
}
