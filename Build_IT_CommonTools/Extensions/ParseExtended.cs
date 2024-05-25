using System.Globalization;

namespace Build_IT_CommonTools.Extensions
{
    public static class ParseExtended
    {
        public static double GetDouble(this string value)
        {
            // Try parsing in the current culture
            if (!double.TryParse(value, NumberStyles.Any,
                CultureInfo.CurrentCulture, out double result) &&
                // Then try in US english
                !double.TryParse(value, NumberStyles.Any,
                CultureInfo.GetCultureInfo("en-US"), out result) &&
                // Then in neutral language
                !double.TryParse(value, NumberStyles.Any,
                CultureInfo.InvariantCulture, out result))
            {
                result = default;
            }
            return result;
        }
    }
}
