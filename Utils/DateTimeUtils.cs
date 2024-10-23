using System;

namespace ZeroTier.Utils
{
    public static class DateTimeUtils
    {
        // Convertir un timestamp en DateTime
        public static DateTime FromUnixTimeMilliseconds(long milliseconds)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;
        }

        // Convertir un DateTime en timestamp (millisecondes)
        public static long ToUnixTimeMilliseconds(DateTime dateTime)
        {
            return ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();
        }

        // Convertir un timestamp en DateTime
        public static DateTime FromUnixTimeSeconds(long seconds)
        {
            return DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;
        }

        // Convertir un DateTime en timestamp (secondes)
        public static long ToUnixTimeSeconds(DateTime dateTime)
        {
            return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        }
    }
}
