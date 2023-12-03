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

    [Fact]
    public void Day02_TestInput_Part1Sample1()
    {
        var day = _solutions.GetDay(2023, 2);
        day.SetTestInput("""
                         Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                         Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                         Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                         Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                         Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                         """);
        Assert.Equal("8", day.Part1Answer);
    }

    [Fact]
    public void Day02_TestInput_Part2Sample1()
    {
        var day = _solutions.GetDay(2023, 2);
        day.SetTestInput("""
                         Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                         Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                         Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                         Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                         Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                         """);
        Assert.Equal("2286", day.Part2Answer);
    }

    [Fact]
    public void Day03_TestInput_Part1Sample1()
    {
        var day = _solutions.GetDay(2023, 3);
        day.SetTestInput("""
                         467..114..
                         ...*......
                         ..35..633.
                         ......#...
                         617*......
                         .....+.58.
                         ..592.....
                         ......755.
                         ...$.*....
                         .664.598..
                         """);
        Assert.Equal("4361", day.Part1Answer);
    }

    [Fact]
    public void Day03_TestInput_Part2Sample1()
    {
        var day = _solutions.GetDay(2023, 3);
        day.SetTestInput("""
                         467..114..
                         ...*......
                         ..35..633.
                         ......#...
                         617*......
                         .....+.58.
                         ..592.....
                         ......755.
                         ...$.*....
                         .664.598..
                         """);
        Assert.Equal("467835", day.Part2Answer);
    }
}
