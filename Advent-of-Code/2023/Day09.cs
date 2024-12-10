using AdventOfCode.Helpers;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public class Day09 : AdventBase
{
    private long[][] _inputs;

    protected override void InternalOnLoad()
    {
        _inputs = Input.Lines.Select(x => x.ParseLongs().ToArray()).ToArray();
    }

    protected override object InternalPart1()
    {
        long sum = 0;
        foreach (var input in _inputs)
        {
            var longs = new List<List<long>>();
            longs.Add([..input]);
            while (true)
            {
                var lastList = longs[^1];
                var nextList = new List<long>();
                longs.Add(nextList);
                for (var i = 0; i < lastList.Count - 1; i++)
                {
                    nextList.Add(lastList[i + 1] - lastList[i]);
                }
                if (nextList.Any(x => x != 0)) continue;
                break;
            }

            long lastElement = 0;
            for (var i = longs.Count - 2; i >= 0; i--)
            {
                lastElement += longs[i][^1];
            }

            sum += lastElement;
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        long sum = 0;
        foreach (var input in _inputs)
        {
            var longs = new List<List<long>>();
            longs.Add([..input]);
            while (true)
            {
                var lastList = longs[^1];
                var nextList = new List<long>();
                longs.Add(nextList);
                for (var i = 0; i < lastList.Count - 1; i++)
                {
                    nextList.Add(lastList[i + 1] - lastList[i]);
                }
                if (nextList.Any(x => x != 0)) continue;
                break;
            }

            long lastElement = 0;
            for (var i = longs.Count - 2; i >= 0; i--)
            {
                var toSubtract = longs[i][0];
                lastElement = toSubtract - lastElement;
            }

            sum += lastElement;
        }

        return sum;
    }
}
