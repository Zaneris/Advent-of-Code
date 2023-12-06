using System.Text.RegularExpressions;
using AdventOfCode.Helpers;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public partial class Day05 : AdventBase
{
    private readonly List<List<DestinationSource>> _maps = [];

    private record struct DestinationSource(long Destination, long Source, long Count);
    private record struct Result(long Value, long FitThrough);
    private record struct Range(long Value, long Count);

    protected override void InternalOnLoad()
    {
        for (var i = 1; i < 8; i++)
        {
            var map = new List<DestinationSource>();
            _maps.Add(map);
            var block = Input.Blocks[i];
            foreach (var line in block.Lines.Skip(1))
            {
                var numbers = line.ParseLongs().ToArray();
                map.Add(new DestinationSource
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
        var seeds = Input.Blocks[0].Text
            .ParseLongs()
            .ToArray();

        var seedLocations = new long[seeds.Length];

        for (var seed = 0; seed < seeds.Length; seed++)
        {
            var current = seeds[seed];
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
        var fitThrough = long.MaxValue;
        foreach (var map in _maps)
        {
            long next = -1;
            foreach (var range in map)
            {
                if (current < range.Source || current >= range.Source + range.Count) continue;

                var diff = current - range.Source;
                next = range.Destination + diff;
                var remaining = range.Source + range.Count - current;
                if (fitThrough > remaining) fitThrough = remaining;
                break;
            }

            if (next >= 0) current = next;
        }

        return new Result(current, fitThrough);
    }

    protected override object InternalPart2()
    {
        var seeds = Input.Blocks[0].Text
            .ParseLongs()
            .Chunk(2)
            .Select(x => new Range(x[0], x[1]))
            .OrderBy(x => x.Value)
            .ToArray();

        //return LocationFirst(seeds);

        var seedLocations = new List<long>();

        long current = -1;
        foreach (var seedRange in seeds)
        {
            for (var seed = seedRange.Value; seed < seedRange.Value + seedRange.Count; seed++)
            {
                if (current > seed) continue;
                var result = EatSeeds(seed);
                current = seed + result.FitThrough;
                seedLocations.Add(result.Value);
                if (current >= seedRange.Value + seedRange.Count) break;
            }
        }

        return seedLocations.Min();
    }

    private long LocationFirst(Range[] seeds)
    {
        long next = 0;
        foreach (var range in _maps[6].OrderBy(x => x.Destination).Select(x => new Range(x.Destination, x.Count)))
        {
            while (next < range.Value + range.Count)
            {
                var location = next < range.Value ? range.Value : next;
                var check = new Range(location, range.Count - (location - range.Value));
                var result = SpitSeeds(check);
                var resultMax = result.Value + result.FitThrough;
                foreach (var seed in seeds)
                {
                    var seedMax = seed.Value + seed.Count;
                    if (result.Value >= seed.Value && seed.Value <= resultMax
                        && seedMax >= result.Value && seedMax >= result.Value)
                    {
                        return location;
                    }
                }
                next = range.Value + result.FitThrough;
            }
        }

        throw new Exception();
    }

    private Result SpitSeeds(Range locationRange)
    {
        var fitThrough = locationRange.Count;
        var current = locationRange.Value;
        for (var i = 6; i >= 0; i--)
        {
            var map = _maps[i];
            long next = -1;
            foreach (var range in map)
            {
                if (current < range.Destination || current >= range.Destination + range.Count) continue;

                var diff = current - range.Destination;
                next = range.Source + diff;
                var remaining = range.Destination + range.Count - current;
                if (fitThrough > remaining) fitThrough = remaining;
                break;
            }

            if (next >= 0) current = next;
        }

        return new Result(current, fitThrough);
    }
}
