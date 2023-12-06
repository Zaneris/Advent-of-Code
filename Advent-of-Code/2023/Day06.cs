using AdventOfCode.Helpers;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public class Day06 : AdventBase
{
    protected override object InternalPart1()
    {
        var times = Input.Lines[0].ParseLongs().ToArray();
        var distances = Input.Lines[1].ParseLongs().ToArray();

        long total = 1;
        for (var i = 0; i < times.Length; i++)
        {
            var exceeds = 0;
            for (var time = 1; time < times[i]; time++)
            {
                if (time * (times[i] - time) > distances[i]) exceeds++;
            }

            total *= exceeds;
        }

        return total;
    }

    protected override object InternalPart2()
    {
        var maxTime = Input.Lines[0].Replace(" ", "").ParseLongs().ToArray()[0];
        var maxDistance = Input.Lines[1].Replace(" ", "").ParseLongs().ToArray()[0];
        var exceeds = 0;
        for (long time = 1; time < maxTime; time++)
        {
            if (time * (maxTime - time) > maxDistance) exceeds++;
        }

        return exceeds;
    }
}
