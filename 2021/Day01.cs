using AdventOfCodeSupport;

namespace AdventOfCode._2021;

public class Day01 : AdventBase
{
    protected override void InternalPart1()
    {
        int? lastNum = null;
        var increased = 0;
        
        foreach (var line in InputLines)
        {
            var num = int.Parse(line);
            if (num > lastNum) increased++;
            lastNum = num;
        }
        Console.WriteLine(increased);
    }

    protected override void InternalPart2()
    {
        int? lastNum = null;
        var increased = 0;
        
        for (var i = 0; i < InputLines.Length - 2; i++)
        {
            var num = int.Parse(InputLines[i]) + int.Parse(InputLines[i + 1]) + int.Parse(InputLines[i + 2]);
            if (num > lastNum) increased++;
            lastNum = num;
        }
        Console.WriteLine(increased);
    }
}
