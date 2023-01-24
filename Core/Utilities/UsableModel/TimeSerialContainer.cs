namespace Core.Utilities.UsableModel
{
    public class TimeSerialContainer
    {
        public TimeSerialContainer()
        {
            IsAnalysed = false;
            IsSelected = false;
        }
        public string Time { get; set; }
        public string Serial { get; set; }
        public bool IsSelected { get; set; }
        public bool IsAnalysed { get; set; }
    }
}
