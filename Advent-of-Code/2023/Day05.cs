using System.Text.RegularExpressions;
using AdventOfCode.Helpers;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public partial class Day05 : AdventBase
{
    private readonly List<List<Range>> _maps = [];
    private long[][] _seeds = null!;

    private record struct Range(long Destination, long Source, long Count);
    private record struct Result(long Location, long Consumed);

    protected override void InternalOnLoad()
    {
        for (var i = 1; i < 8; i++)
        {
            var map = new List<Range>();
            _maps.Add(map);
            var block = Input.Blocks[i];
            foreach (var line in block.Lines.Skip(1))
            {
                var numbers = line.ParseLongs().ToArray();
                map.Add(new Range
                {
                    Destination = numbers[0],
                    Source = numbers[1],
                    Count = numbers[2]
                });
            }
        }
    }

    protected override object InternalPart1()
    {
        _seeds = Input.Blocks[0].Text.ParseLongs().Select(x => new[] {x, 0}).ToArray();

        var seedLocations = new long[_seeds.Length];

        for (var seed = 0; seed < _seeds.Length; seed++)
        {
            var current = _seeds[seed][0];
            foreach (var map in _maps)
            {
                long next = -1;
                foreach (var range in map)
                {
                    if (current < range.Source || current >= range.Source + range.Count)
                        continue;
                    var diff = current - range.Source;
                    next = range.Destination + diff;
                    break;
                }

                if (next >= 0) current = next;
            }

            seedLocations[seed] = current;
        }

        return seedLocations.Min();
    }

    private Result EatSeeds(long seed)
    {
        var current = seed;
        var consumed = long.MaxValue;
        foreach (var map in _maps)
        {
            long next = -1;
            foreach (var range in map)
            {
                if (current < range.Source || current >= range.Source + range.Count) continue;

                var diff = current - range.Source;
                next = range.Destination + diff;
                if (consumed > diff) consumed = diff;
                break;
            }

            if (next >= 0) current = next;
        }

        return new Result(current, consumed);
    }

    protected override object InternalPart2()
    {
        _seeds = Input.Blocks[0].Text.ParseLongs().Chunk(2).OrderBy(x => x[0]).ToArray();

        var seedLocations = new List<long>();

        long current = -1;
        foreach (var seedRange in _seeds)
        {
            for (long seed = seedRange[0]; seed < seedRange[0] + seedRange[1]; seed++)
            {
                if (current > seed) continue;
                var result = EatSeeds(seed);
                current = seed + result.Consumed;
                seedLocations.Add(result.Location);
                if (current >= seedRange[0] + seedRange[1]) break;
            }
        }

        return seedLocations.Min();
    }
}
