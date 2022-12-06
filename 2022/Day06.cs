using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day06 : AdventBase
{
    protected override void InternalPart1()
    {
        FindFirstNonRepeating(4);
    }

    protected override void InternalPart2()
    {
        FindFirstNonRepeating(14);
    }

    private void FindFirstNonRepeating(int size)
    {
        for (var i = 0; i < InputText.Length - size; i++)
        {
            var sub = InputText.Substring(i, size);
            var duplicate = sub.Any(c => sub.Count(x => x == c) != 1);
            if (duplicate) continue;
            Console.WriteLine(sub);
            Console.WriteLine(i + size);
            return;
        }
    }
}
