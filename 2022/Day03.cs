using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day03 : AdventBase
{
    protected override object InternalPart1()
    {
        long sum = 0;
        foreach (var line in Input.Lines)
        {
            var part1 = line.Substring(0, line.Length / 2);
            var part2 = line.Substring(line.Length / 2, line.Length / 2);
            if (part1.Length != part2.Length) throw new Exception();
            var match = part1.First(x => part2.Contains(x));
            sum += Priority(match);
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        long sum = 0;
        for (var i = 0; i < Input.Lines.Length; i += 3)
        {
            var part1 = Input.Lines[i];
            var part2 = Input.Lines[i + 1];
            var part3 = Input.Lines[i + 2];
            var match = part1.First(x => part2.Contains(x) && part3.Contains(x));
            sum += Priority(match);
        }

        return sum;
    }

    private int Priority(char c)
    {
        if (c >= 'a') return c - 'a' + 1;
        return c - 'A' + 27;
    }
}
