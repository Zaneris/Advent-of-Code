namespace Advent_of_Code.Dependencies;

public abstract class AoCBase : IAoC
{
    public int Year { get; }
    public int Day { get; }

    protected string InputText { get; }

    protected AoCBase(int year, int day)
    {
        Year = year;
        Day = day;
        InputText = File.ReadAllText($"../../../{year}/Inputs/{day}.txt");
    }

    public abstract IAoC Part1();
    public abstract IAoC Part2();
}
