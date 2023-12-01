using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var lines = Input.Lines;

        long sum = 0;
        foreach (var line in lines)
        {
            var numText = "";
            numText += Part1Search(line, +1);
            numText += Part1Search(line, -1);

            sum += int.Parse(numText);
        }
        return sum;
    }

    private static char Part1Search(string line, int increment)
    {
        var start = increment > 0 ? 0 : line.Length - 1;
        var end = increment > 0 ? line.Length : -1;
        for (var i = start; i != end; i += increment)
        {
            if (line[i] >= '0' && line[i] <= '9')
                return line[i];
        }

        throw new Exception();
    }

    protected override object InternalPart2()
    {
        var lines = Input.Lines;

        long sum = 0;
        foreach (var line in lines)
        {
            var numText = "";
            numText += Part2Search(line, +1);
            numText += Part2Search(line, -1);

            sum += int.Parse(numText);
        }
        return sum;
    }

    private static char Part2Search(string line, int increment)
    {
        var start = increment > 0 ? 0 : line.Length - 1;
        var end = increment > 0 ? line.Length : -1;
        for (var i = start; i != end; i += increment)
        {
            if (line[i] >= '0' && line[i] <= '9')
                return line[i];
            var wordNum = TextToNumber(line[i..]);
            if (wordNum is not null) return (char)wordNum;
        }

        throw new Exception();
    }

    private static char? TextToNumber(string word)
    {
        if (word.StartsWith("one")) return '1';
        if (word.StartsWith("two")) return '2';
        if (word.StartsWith("three")) return '3';
        if (word.StartsWith("four")) return '4';
        if (word.StartsWith("five")) return '5';
        if (word.StartsWith("six")) return '6';
        if (word.StartsWith("seven")) return '7';
        if (word.StartsWith("eight")) return '8';
        if (word.StartsWith("nine")) return '9';
        return null;
    }
}
