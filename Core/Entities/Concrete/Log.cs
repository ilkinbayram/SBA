using Core.Resources.Enums;

namespace Core.Entities.Concrete
{
    public class Log : IEntity
    {
        public Log()
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            CreatedDate = azerbaycanTime;
            CreatedBy = "System.Admin";
        }
        public int Id { get; set; }
        public string Path { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public LogType Type { get; set; }
        public LogImportance Importance { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
