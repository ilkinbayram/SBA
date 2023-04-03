namespace SBA.WebAPI.Resources.Models
{
    public class TimerMatchModel
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public string MatchTime { get; set; }
        public List<int> Serials { get; set; }
    }
}
