using AdventOfCodeSupport;
using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

public class Day03 : AdventBase
{
    private long GetTotal(bool doDont)
    {
        var pattern = @"mul\(([0-9]{1,3}),([0-9]{1,3})\)" + (doDont ? @"|don't\(\)|do\(\)" : "");
        var matches = Regex.Matches(Input.Text, pattern).ToList();
        long total = 0;
        var enabled = true;
        foreach (var match in matches)
        {
            switch (match.Value)
            {
                case "don't()":
                    enabled = false;
                    continue;
                case "do()":
                    enabled = true;
                    continue;
            }

            if (!enabled) continue;
            var captureGroups = match.Groups;
            var left = captureGroups[1].Value;
            var right = captureGroups[2].Value;
            total += int.Parse(left) * int.Parse(right);
        }

        return total;
    }

    protected override object InternalPart1()
    {
        return GetTotal(false);
    }

    protected override object InternalPart2()
    {
        return GetTotal(true);
    }
}
