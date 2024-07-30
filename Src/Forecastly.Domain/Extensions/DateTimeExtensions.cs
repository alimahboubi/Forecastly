namespace Forecastly.Domain.Extensions;

public static class DateTimeExtensions
{
    public static DateTime FromUnixTimeSeconds(this long seconds)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(seconds);
        return dateTimeOffset.UtcDateTime;
    }

    public static DateTime FromUnixTimeMilliseconds(this long milliseconds)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
        return dateTimeOffset.UtcDateTime;
    }

    public static long ToUnixTimeSeconds(this DateTime dateTime)
    {
        DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime);
        return dateTimeOffset.ToUnixTimeSeconds();
    }

    public static long ToUnixTimeMilliseconds(this DateTime dateTime)
    {
        DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime);
        return dateTimeOffset.ToUnixTimeMilliseconds();
    }
}