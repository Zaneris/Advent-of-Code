using System.Reflection;
using AdventOfCodeSupport;

namespace Advent_of_Code.Test;

public class Test2017
{
    private readonly AdventSolutions _solutions;

    public Test2017()
    {
        Assembly.Load("Advent-of-Code");
        _solutions = new AdventSolutions();
    }

    [Fact]
    public void Day01_TestInput_Part1Sample1()
    {
        var day = _solutions.GetDay(2017, 1);
        day.SetTestInput("91212129");
        Assert.Equal("9", day.Part1Answer);
    }

    [Fact]
    public void Day01_TestInput_Part1Sample2()
    {
        var day = _solutions.GetDay(2017, 1);
        day.SetTestInput("1122");
        Assert.Equal("3", day.Part1Answer);
    }

    [Fact]
    public void Day01_TestInput_Part2Sample1()
    {
        var day = _solutions.GetDay(2017, 1);
        day.SetTestInput("1212");
        Assert.Equal("6", day.Part2Answer);
    }

    [Fact]
    public void Day01_TestInput_Part2Sample2()
    {
        var day = _solutions.GetDay(2017, 1);
        day.SetTestInput("12131415");
        Assert.Equal("4", day.Part2Answer);
    }
}
