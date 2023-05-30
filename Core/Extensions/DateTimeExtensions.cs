namespace Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToAzeDate(this DateTime dateTimeUtc)
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, azerbaycanZone);
            return azerbaycanTime.Date;
        }
    }
}
