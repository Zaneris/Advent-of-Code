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

    protected abstract void InternalPart1();
    protected abstract void InternalPart2();

    public IAoC Part1()
    {
        InternalPart1();
        return this;
    }

    public IAoC Part2()
    {
        InternalPart2();
        return this;
    }
}
