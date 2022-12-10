using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day10 : AdventBase
{
    private long _registerX = 1;
    
    private class AddX
    {
        public int Ticks { get; set; }
        public int Value { get; set; }
    }

    protected override object InternalPart1()
    {
        AddX? active = null;
        long cycle = 1;
        var i = 0;
        long sum = 0;
        while (i < Input.Lines.Length)
        {
            if (active is not null)
            {
                active.Ticks--;
                if (active.Ticks == 0)
                {
                    _registerX += active.Value;
                    active = null;
                }
            }

            var pixel = (cycle-1) % 40;
            if (pixel == 0) Console.WriteLine();
            if (pixel >= _registerX - 1 && pixel <= _registerX + 1)
            {
                Console.Write('#');
            }
            else Console.Write(' ');

            if ((cycle + 20) % 40 == 0)
            {
                var strength = cycle * _registerX;
                sum += strength;
            }

            if (active is null)
            {
                var split = Input.Lines[i++].Split(' ');
                if (split.Length == 2)
                {
                    var value = int.Parse(split[1]);
                    active = new AddX { Ticks = 2, Value = value };
                }
            }
            cycle++;
        }
        Console.WriteLine();
        return sum;
    }

    protected override object InternalPart2()
    {
        // Read print of part 1
        return 0;
    }
}
