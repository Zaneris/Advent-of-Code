using System.Reflection;
using AdventOfCodeSupport;

namespace Advent_of_Code.Test;

public class Test2024
{
    private readonly AdventSolutions _solutions;

    public Test2024()
    {
        Assembly.Load("Advent-of-Code");
        _solutions = new AdventSolutions();
    }

    [Fact]
    public void Day01_TestInput_Part1Sample1()
    {
        var day = _solutions.GetDay(2024, 1);
        day.SetTestInput("""
                         1abc2
                         pqr3stu8vwx
                         a1b2c3d4e5f
                         treb7uchet
                         """);
        Assert.Equal("0", day.Part1Answer);
    }
}
