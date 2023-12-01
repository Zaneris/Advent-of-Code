using System.Reflection;
using AdventOfCodeSupport;

namespace Advent_of_Code.Test;

public class Test2023
{
    private readonly AdventSolutions _solutions;

    public Test2023()
    {
        Assembly.Load("Advent-of-Code");
        _solutions = new AdventSolutions();
    }

    [Fact]
    public void Day01_TestInput_Part1Sample1()
    {
        var day = _solutions.GetDay(2023, 1);
        day.SetTestInput("""
                         1abc2
                         pqr3stu8vwx
                         a1b2c3d4e5f
                         treb7uchet
                         """);
        Assert.Equal("142", day.Part1Answer);
    }

    [Fact]
    public void Day01_TestInput_Part2Sample1()
    {
        var day = _solutions.GetDay(2023, 1);
        day.SetTestInput("""
                         two1nine
                         eightwothree
                         abcone2threexyz
                         xtwone3four
                         4nineeightseven2
                         zoneight234
                         7pqrstsixteen
                         """);
        Assert.Equal("281", day.Part2Answer);
    }
}
