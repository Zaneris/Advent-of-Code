using System.Text.RegularExpressions;

namespace AdventOfCode.Helpers;

public static partial class StringExtensions
{
    public static IEnumerable<long> ParseLongs(this string s)
    {
        return DigitsRegex().Matches(s).Select(x => long.Parse(x.Value));
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex DigitsRegex();
}
