using System;

namespace Mews.Fiscalization.Austria.Dto
{
    public sealed class LocalDateTime
    {
        public LocalDateTime(DateTime dateTime, TimeZoneInfo timeZoneInfo)
        {
            DateTime = dateTime;
            TimeZoneInfo = timeZoneInfo;
        }

        public static LocalDateTime Now
        {
            get { return new LocalDateTime(DateTime.UtcNow, TimeZoneInfo.Utc); }
        }

        public DateTime DateTime { get; }

        public TimeZoneInfo TimeZoneInfo { get; }

        public string ToString(TimeZoneInfo timeZone)
        {
            var dateTimeUtc = TimeZoneInfo.ConvertTimeToUtc(DateTime, TimeZoneInfo);
            var dateTimeTimeZone = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, timeZone);
            var date = DateTime.SpecifyKind(new DateTime(dateTimeTimeZone.Ticks - dateTimeTimeZone.Ticks % TimeSpan.TicksPerSecond), DateTimeKind.Local);
            return date.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public static LocalDateTime Parse(string value, TimeZoneInfo timeZone)
        {
            if (DateTime.TryParse(value, out DateTime val))
            {
                return new LocalDateTime(val, timeZone);
            }
            throw new ArgumentException($"Value '{value}' is not a valid DateTime.");
        }
    }
}
