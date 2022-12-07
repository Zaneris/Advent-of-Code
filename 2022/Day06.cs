using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day06 : AdventBase
{
    protected override object InternalPart1()
    {
        return FindFirstNonRepeating(4);
    }

    protected override object InternalPart2()
    {
        return FindFirstNonRepeating(14);
    }

    private int FindFirstNonRepeating(int size)
    {
        for (var i = 0; i < Input.Text.Length - size; i++)
        {
            var sub = Input.Text.Substring(i, size);
            var duplicate = sub.Any(c => sub.Count(x => x == c) != 1);
            if (duplicate) continue;
            Console.WriteLine(sub);
            return i + size;
        }

        throw new Exception();
    }
}
