using AdventOfCodeSupport;

namespace AdventOfCode._2018;

public class Day01 : AdventBase
{
    protected override void InternalPart1()
    {
        long frequency = 0;
        foreach (var line in InputLines)
        {
            var op = line[0];
            var num = int.Parse(line[1..]);
            if (op == '+') frequency += num;
            else frequency -= num;
        }
        Console.WriteLine(frequency);
    }

    protected override void InternalPart2()
    {
        long frequency = 0;
        var occurrences = new HashSet<long>();
        while (true)
        {
            foreach (var line in InputLines)
            {
                var op = line[0];
                var num = int.Parse(line[1..]);
                if (op == '+') frequency += num;
                else frequency -= num;
                if (occurrences.Contains(frequency))
                {
                    Console.WriteLine(frequency);
                    return;
                }

                occurrences.Add(frequency);
            }
        }
    }
}
