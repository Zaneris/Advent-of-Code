using AdventOfCodeSupport;

namespace AdventOfCode._2018;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        return Input.Lines
            .Select(int.Parse)
            .Aggregate<int, long>(0, (current, num) => current + num);
    }

    protected override object InternalPart2()
    {
        long frequency = 0;
        var occurrences = new HashSet<long>();
        while (true)
        {
            foreach (var line in Input.Lines)
            {
                var num = int.Parse(line);
                frequency += num;
                if (!occurrences.Add(frequency))
                {
                    return frequency;
                }
            }
        }
    }
}
