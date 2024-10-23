using System.Collections.Generic;
using System.Linq;

namespace ZeroTier.Utils
{
    public static class NullController
    {
        public static bool IsCollectionValid<T>(IEnumerable<T> collection)
        {
            return collection != null && collection.Any();
        }

        public static string GetSafeValue<T>(T value, string defaultValue = "")
        {
            return value?.ToString() ?? defaultValue;
        }
    }
}
