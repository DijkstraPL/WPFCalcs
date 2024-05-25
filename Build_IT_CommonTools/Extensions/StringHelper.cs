using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Build_IT_CommonTools.Extensions
{
    public static class StringHelper
    {
        public static IEnumerable<string> EverythingBetween(this string source, string start, string end)
        {
            string pattern = string.Format(
                "{0}({1}){2}",
                Regex.Escape(start),
                ".+?",
                 Regex.Escape(end));

            return GetRegexMatches(source, pattern);
        }

        private static IEnumerable<string> GetRegexMatches(string source, string pattern)
        {
            foreach (Match m in Regex.Matches(source, pattern))
                yield return m.Groups[1].Value;
        }
    }
}
