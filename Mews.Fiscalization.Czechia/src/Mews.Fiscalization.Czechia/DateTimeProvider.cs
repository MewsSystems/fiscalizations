using System;

namespace Mews.Eet
{
    public class DateTimeProvider
    {
        public static DateTimeWithTimeZone Now
        {
            get { return new DateTimeWithTimeZone(DateTime.UtcNow, TimeZoneInfo.Utc); }
        }
    }
}
