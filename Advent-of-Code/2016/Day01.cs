using AdventOfCodeSupport;

namespace AdventOfCode._2016;

public class Day01 : AdventBase
{
    public const int NORTH = 0;
    public const int EAST = 1;
    public const int SOUTH = 2;
    public const int WEST = 3;

    protected override object InternalPart1()
    {
        var currentDirection = NORTH;
        var x = 0;
        var y = 0;
        var input = Input.Text.Trim();
        input = input.Replace(" ", "");
        var directions = input.Split(',');
        foreach (var direction in directions)
        {
            if (direction[0] == 'R') currentDirection++;
            else currentDirection--;
            if (currentDirection < 0) currentDirection += 4;
            if (currentDirection > 3) currentDirection -= 4;
            var move = int.Parse(direction[1..].ToString());
            switch (currentDirection)
            {
                case NORTH: y += move; break;
                case EAST:  x += move; break;
                case SOUTH: y -= move; break;
                case WEST:  x -= move; break;
            }
        }
        Console.WriteLine($"X: {x} Y: {y}");
        return Math.Abs(x) + Math.Abs(y);
    }

    protected override object InternalPart2()
    {
        var currentDirection = NORTH;
        var x = 0;
        var y = 0;
        var input = Input.Text.Trim();
        var visited = new HashSet<string>();
        input = input.Replace(" ", "");
        var directions = input.Split(',');
        foreach (var direction in directions)
        {
            currentDirection = (currentDirection + (direction[0] == 'R' ? 1 : -1) + 4) % 4;
            var move = int.Parse(direction[1..]);
            for (var i = 0; i < move; i++)
            {
                switch (currentDirection)
                {
                    case NORTH: y++; break;
                    case EAST:  x++; break;
                    case SOUTH: y--; break;
                    case WEST:  x--; break;
                }

                if (!visited.Add($"X:{x} Y:{y}"))
                {
                    return Math.Abs(x) + Math.Abs(y);
                }
            }
        }
        Console.WriteLine($"X: {x} Y: {y}");
        return Math.Abs(x) + Math.Abs(y);
    }
}
