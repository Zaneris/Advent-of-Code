using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day01 : AdventBase
{
    private List<int> LeftList { get; set; } = [];
    private List<int> RightList { get; set; } = [];

    protected override void InternalOnLoad()
    {
        foreach (var line in Input.Lines)
        {
            var leftRight = line.Split("   ");
            LeftList.Add(int.Parse(leftRight[0]));
            RightList.Add(int.Parse(leftRight[1]));
        }
    }

    protected override object InternalPart1()
    {
        LeftList.Sort();
        RightList.Sort();

        var totalDifference = 0;

        for (var i = 0; i < LeftList.Count; i++)
        {
            var left = LeftList[i];
            var right = RightList[i];
            var difference = Math.Abs(right - left);
            totalDifference += difference;
        }

        return totalDifference;
    }

    protected override object InternalPart2()
    {
        return LeftList.Select(left => RightList.Count(right => right == left) * left).Sum();
    }
}
