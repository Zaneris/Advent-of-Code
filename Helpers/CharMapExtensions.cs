namespace AdventOfCode.Helpers;

public static class CharMapExtensions
{
    private static readonly Point2d[] _diagonals = { new(1, 1), new(-1, 1), new(1, -1), new(-1, -1) };
    private static readonly Point2d[] _cardinals = { new(0, 1), new(1, 0), new(-1, 0), new(0, -1) };
    
    /// <summary>
    /// Find number of steps for the shortest path between 2 points.
    /// </summary>
    /// <param name="map">The map to navigate.</param>
    /// <param name="start">Starting point.</param>
    /// <param name="endRule">When is the end found, passes point and map char of current position.</param>
    /// <param name="validStep">Is the step being attempted valid? Old map char, attempted map char.</param>
    /// <param name="diagonals">Can we move diagonally?</param>
    public static long ShortestPathSteps(this char[][] map, Point2d start, Func<Point2d, char, bool> endRule, 
        Func<char, char, bool> validStep, bool diagonals = false)
    {
        var queue = new Queue<(Point2d position, long steps)>();
        queue.Enqueue((start, 0));
        var seen = new Dictionary<string, long>();
        var moves = diagonals ? _cardinals.Concat(_diagonals).ToArray() : _cardinals;
        var yLimit = map.Length;
        var xLimit = map[0].Length;
        while (queue.TryDequeue(out var target))
        {
            if (seen.ContainsKey(target.position)) continue;
            seen.Add(target.position, target.steps);
            var oldChar = map[target.position.Y][target.position.X];
            foreach (var move in moves)
            {
                var next = target.position + move;
                if (next.Y < 0 || next.Y >= yLimit || next.X < 0 || next.X >= xLimit) continue;
                if (seen.ContainsKey(next)) continue;
                var nextChar = map[next.Y][next.X];
                if (!validStep(oldChar, nextChar)) continue;
                var steps = target.steps + 1;
                if (endRule(next, nextChar)) return steps;
                queue.Enqueue((next, steps));
            }
        }

        throw new Exception("Could not find path.");
    }
}
