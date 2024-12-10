using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day02 : AdventBase
{
    private enum Direction
    {
        Unknown,
        Increasing,
        Decreasing
    }

    private enum Safety
    {
        Unsafe,
        Safe
    }

    private class Report
    {
        public required List<int> Levels { get; set; }
        public Direction Direction { get; set; }
        public Safety Safety { get; set; }
    }

    private List<Report> Reports { get; set; } = [];

    protected override void InternalOnLoad()
    {
        foreach (var line in Input.Lines)
        {
            var report = new Report
            {
                Levels = line.Split(' ').Select(int.Parse).ToList()
            };
            Reports.Add(report);
        }
    }

    protected override object InternalPart1()
    {
        foreach (var report in Reports)
        {
            OneFaultCheck(report, false);
        }

        return Reports.Count(x => x.Safety == Safety.Safe);
    }

    protected override object InternalPart2()
    {
        foreach (var report in Reports)
        {
            OneFaultCheck(report, true);
        }

        return Reports.Count(x => x.Safety == Safety.Safe);
    }

    private static void OneFaultCheck(Report report, bool oneFault)
    {
        for (var skip = -1; skip < (oneFault ? report.Levels.Count : 0); skip++)
        {
            int? previousLevel = null;
            report.Safety = Safety.Safe;
            report.Direction = Direction.Unknown;

            for (var i = 0; i < report.Levels.Count; i++)
            {
                if (i == skip) continue;
                var currentLevel = report.Levels[i];
                if (previousLevel is null)
                {
                    previousLevel = currentLevel;
                    continue;
                }

                if (report.Direction == Direction.Unknown)
                {
                    if (currentLevel > previousLevel)
                    {
                        report.Direction = Direction.Increasing;
                    }
                    else if (currentLevel < previousLevel)
                    {
                        report.Direction = Direction.Decreasing;
                    }
                }

                if ((currentLevel == previousLevel)
                    || (report.Direction == Direction.Increasing && currentLevel < previousLevel)
                    || (report.Direction == Direction.Decreasing && currentLevel > previousLevel)
                    || (Math.Abs(currentLevel - (int)previousLevel) > 3))
                {
                    report.Safety = Safety.Unsafe;
                    break;
                }

                previousLevel = currentLevel;
            }

            if (report.Safety == Safety.Safe) return;
        }
    }
}
