using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day09 : AdventBase
{
    private class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    private void Move(char direction, IReadOnlyList<Coord> snake)
    {
        if (direction == 'U') snake[0].Y++;
        else if (direction == 'R') snake[0].X++;
        else if (direction == 'D') snake[0].Y--;
        else if (direction == 'L') snake[0].X--;

        for (var i = 1; i < snake.Count; i++)
        {
            if (Math.Abs(snake[i-1].X - snake[i].X) < 2 && Math.Abs(snake[i-1].Y - snake[i].Y) < 2) continue;
            if (snake[i-1].X > snake[i].X && snake[i-1].Y == snake[i].Y) snake[i].X++;
            else if (snake[i-1].X < snake[i].X && snake[i-1].Y == snake[i].Y) snake[i].X--;
            else if (snake[i-1].X == snake[i].X && snake[i-1].Y > snake[i].Y) snake[i].Y++;
            else if (snake[i-1].X == snake[i].X && snake[i-1].Y < snake[i].Y) snake[i].Y--;
            else if (snake[i-1].X > snake[i].X && snake[i-1].Y > snake[i].Y)
            {
                snake[i].X++;
                snake[i].Y++;
            }
            else if (snake[i-1].X > snake[i].X && snake[i-1].Y < snake[i].Y)
            {
                snake[i].X++;
                snake[i].Y--;
            }
            else if (snake[i-1].X < snake[i].X && snake[i-1].Y < snake[i].Y)
            {
                snake[i].X--;
                snake[i].Y--;
            }
            else if (snake[i-1].X < snake[i].X && snake[i-1].Y > snake[i].Y)
            {
                snake[i].X--;
                snake[i].Y++;
            }
        }
    }
    
    protected override object InternalPart1()
    {
        var snake = new Coord[2];
        snake[0] = new Coord();
        snake[1] = new Coord();
        var seen = new HashSet<string>();
        foreach (var line in Input.Lines)
        {
            var parts = line.Split(' ');
            var direction = parts[0][0];
            var count = int.Parse(parts[1]);
            for (var i = 0; i < count; i++)
            {
                Move(direction, snake);
                var position = $"{snake[1].X},{snake[1].Y}";
                seen.Add(position);
            }
        }

        return seen.Count;
    }

    protected override object InternalPart2()
    {
        var snake = new Coord[10];
        for (var i = 0; i < 10; i++)
        {
            snake[i] = new Coord();
        }
        var seen = new HashSet<string>();
        foreach (var line in Input.Lines)
        {
            var parts = line.Split(' ');
            var direction = parts[0][0];
            var count = int.Parse(parts[1]);
            for (var i = 0; i < count; i++)
            {
                Move(direction, snake);
                var position = $"{snake[9].X},{snake[9].Y}";
                seen.Add(position);
            }
        }

        return seen.Count;
    }
}
