using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var elves = Input.Blocks;
        long max = 0;
        foreach (var elf in elves)
        {
            long total = 0;
            foreach (var calorie in elf.Lines)
            {
                total += int.Parse(calorie);
            }

            if (total > max) max = total;
        }
        return max;
    }

    protected override object InternalPart2()
    {
        var elves = Input.Blocks;
        var totals = new List<long>();
        foreach (var elf in elves)
        {
            long total = 0;
            foreach (var calorie in elf.Lines)
            {
                total += int.Parse(calorie);
            }

            totals.Add(total);
        }

        var sum = totals.OrderByDescending(x => x).Take(3).Sum();
        return sum;
    }
}
