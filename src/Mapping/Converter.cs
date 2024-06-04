namespace Mapping;

public static class Converter
{
    public static DateTime? UnixTimeStampToDateTime(uint unixTimeStamp)
    {
        return unixTimeStamp > 0 ? DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).UtcDateTime : null;
    }
}