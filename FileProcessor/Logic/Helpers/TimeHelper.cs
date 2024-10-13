namespace FileProcessor.Logic.Helpers;

public static class TimeHelper
{
    public static long ToTimeStamp(this DateTime dateTime) => 
        (long)dateTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
    
    public static DateTime FromTimeStamp(long timestamp) => 
        new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
    
    public static DateTime FromTimeStampMilliseconds(long timestamp) => 
        new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timestamp);
}