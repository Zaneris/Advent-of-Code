using AdventOfCodeSupport;

namespace AdventOfCode._2017;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var numbers = Input.Text.Trim().Select(x => int.Parse(x.ToString())).ToArray();
        long sum = 0;
        for (var i = 0; i < numbers.Length; i++)
        {
            if (i == numbers.Length - 1)
            {
                if (numbers[i] == numbers[0])
                    sum += numbers[i];
            }
            else
            {
                if (numbers[i] == numbers[i + 1])
                    sum += numbers[i];
            }
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        var numbers = Input.Text.Trim().Select(x => int.Parse(x.ToString())).ToArray();
        long sum = 0;
        long halfway = numbers.Length / 2;
        for (var i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] == numbers[(i + halfway) % numbers.Length])
                sum += numbers[i];
        }

        return sum;
    }
}
