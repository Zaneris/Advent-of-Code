using AdventOfCodeSupport;

namespace AdventOfCode._2019;

public class Day01 : AdventBase
{
    private int CalculateFuel(int mass)
    {
        return mass / 3 - 2;
    }

    private long FuelWithFuelWeight(int mass)
    {
        long total = 0;
        while (true)
        {
            mass = CalculateFuel(mass);
            if (mass <= 0) break;
            total += mass;
        }

        return total;
    }

    protected override object InternalPart1()
    {
        var moduleMasses = Input.Lines.Select(int.Parse).ToList();
        return moduleMasses.Aggregate<int, long>(0, (current, mass) => current + CalculateFuel(mass));
    }

    protected override object InternalPart2()
    {
        var moduleMasses = Input.Lines.Select(int.Parse).ToList();
        return moduleMasses.Aggregate<int, long>(0, (current, mass) => current + FuelWithFuelWeight(mass));
    }
}
