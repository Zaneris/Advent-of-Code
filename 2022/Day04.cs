using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day04 : AdventBase
{
    class Range
    {
        public Range(string range)
        {
            var split = range.Split('-');
            Min = int.Parse(split[0]);
            Max = int.Parse(split[1]);
        }
        
        public int Min { get; set; }
        public int Max { get; set; }
    }
    
    protected override void InternalPart1()
    {
        var pairs = InputLines
            .Select(line => line.Split(','))
            .Select(split => (new Range(split[0]), new Range(split[1])))
            .ToList();

        var fullyContains = pairs
            .Where(x => (x.Item1.Min >= x.Item2.Min
                         && x.Item1.Min <= x.Item2.Max
                         && x.Item1.Max >= x.Item2.Min
                         && x.Item1.Max <= x.Item2.Max)
                        || (x.Item2.Min >= x.Item1.Min
                            && x.Item2.Min <= x.Item1.Max
                            && x.Item2.Max >= x.Item1.Min
                            && x.Item2.Max <= x.Item1.Max))
            .ToList();
        Console.WriteLine(fullyContains.Count);
    }

    protected override void InternalPart2()
    {
        var pairs = InputLines
            .Select(line => line.Split(','))
            .Select(split => (new Range(split[0]), new Range(split[1])))
            .ToList();

        var fullyContains = pairs
            .Where(x => (x.Item1.Min >= x.Item2.Min && x.Item1.Min <= x.Item2.Max) 
                        || (x.Item1.Max >= x.Item2.Min && x.Item1.Max <= x.Item2.Max) 
                        || (x.Item2.Min >= x.Item1.Min && x.Item2.Min <= x.Item1.Max) 
                        || (x.Item2.Max >= x.Item1.Min && x.Item2.Max <= x.Item1.Max))
            .ToList();
        Console.WriteLine(fullyContains.Count);
    }
}