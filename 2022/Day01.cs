using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day01 : AdventBase
{
    protected override void InternalPart1()
    {
        var elves = InputText.Trim().Split("\n\n");
        long max = 0;
        foreach (var elf in elves)
        {
            long total = 0;
            foreach (var calorie in elf.Split('\n'))
            {
                total += int.Parse(calorie);
            }

            if (total > max) max = total;
        }
        Console.WriteLine(max);
    }

    protected override void InternalPart2()
    {
        var elves = InputText.Trim().Split("\n\n");
        var totals = new List<long>();
        foreach (var elf in elves)
        {
            long total = 0;
            foreach (var calorie in elf.Split('\n'))
            {
                total += int.Parse(calorie);
            }

            totals.Add(total);
        }

        var sum = totals.OrderByDescending(x => x).Take(3).Sum();
        Console.WriteLine(sum);
    }
}
