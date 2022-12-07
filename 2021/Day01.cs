using AdventOfCodeSupport;

namespace AdventOfCode._2021;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        int? lastNum = null;
        var increased = 0;
        
        foreach (var line in Input.Lines)
        {
            var num = int.Parse(line);
            if (num > lastNum) increased++;
            lastNum = num;
        }

        return increased;
    }

    protected override object InternalPart2()
    {
        int? lastNum = null;
        var increased = 0;
        
        for (var i = 0; i < Input.Lines.Length - 2; i++)
        {
            var num = int.Parse(Input.Lines[i]) + int.Parse(Input.Lines[i + 1]) + int.Parse(Input.Lines[i + 2]);
            if (num > lastNum) increased++;
            lastNum = num;
        }
        return increased;
    }
}
