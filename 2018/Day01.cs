using AdventOfCodeSupport;

namespace AdventOfCode._2018;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        long frequency = 0;
        foreach (var line in Input.Lines)
        {
            var op = line[0];
            var num = int.Parse(line[1..]);
            if (op == '+') frequency += num;
            else frequency -= num;
        }
        return frequency;
    }

    protected override object InternalPart2()
    {
        long frequency = 0;
        var occurrences = new HashSet<long>();
        while (true)
        {
            foreach (var line in Input.Lines)
            {
                var op = line[0];
                var num = int.Parse(line[1..]);
                if (op == '+') frequency += num;
                else frequency -= num;
                if (occurrences.Contains(frequency))
                {
                    return frequency;
                }

                occurrences.Add(frequency);
            }
        }
    }
}
