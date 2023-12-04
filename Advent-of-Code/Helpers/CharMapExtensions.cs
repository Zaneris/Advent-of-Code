using System.Text;
using CommunityToolkit.HighPerformance;

namespace AdventOfCode.Helpers;

public static class CharMapExtensions
{
    private static readonly Point2d[] _diagonals = { new(1, 1), new(-1, 1), new(1, -1), new(-1, -1) };
    private static readonly Point2d[] _cardinals = { new(0, 1), new(1, 0), new(-1, 0), new(0, -1) };
    private static readonly Point2d[] _both = _cardinals.Concat(_diagonals).ToArray();

    /// <summary>
    /// Find number of steps for the shortest path between 2 points.
    /// </summary>
    /// <param name="map">The map to navigate.</param>
    /// <param name="start">Starting point.</param>
    /// <param name="endRule">When is the end found? Passes point and map char of current position.</param>
    /// <param name="validStep">Is the step being attempted valid? Previous map char, attempted map char.</param>
    /// <param name="diagonals">Can we move diagonally?</param>
    public static long ShortestPathSteps(this char[][] map, Point2d start, Func<Point2d, char, bool> endRule,
        Func<char, char, bool> validStep, bool diagonals = false)
    {
        var queue = new Queue<(Point2d position, long steps)>();
        queue.Enqueue((start, 0));
        var seen = new Dictionary<string, long>();
        var moves = diagonals ? _both : _cardinals;
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

    public static void For(this char[][] map, Action<int, int> action)
    {
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[0].Length; x++)
            {
                action(y, x);
            }
        }
    }

    public static IEnumerable<Point2d> Adjacent(this char[][] map, int y, int x)
    {
        foreach (var move in _both)
        {
            var checkX = x + move.X;
            var checkY = y + move.Y;

            if (checkX < 0 || checkY < 0) continue;
            if (checkX >= map[0].Length || checkY >= map.Length) continue;
            yield return new Point2d(checkY, checkX);
        }
    }

    private static readonly StringBuilder _sb = new();
    public static string SubString(this char[] array, int? start, int? end = null, int? length = null)
    {
        if (start is null || end is null) throw new Exception("Cannot be null");
        if (end is null && length is null) throw new Exception("Must provide length or end");
        length ??= end - start + 1;
        return new string(array, (int)start, (int)length!);
    }

    public static string SubString(this ReadOnlySpan2D<byte> map, int y, int start, int end)
    {
        var length = end - start + 1;
        _sb.Clear();
        for (var i = start; i < start + length; i++)
        {
            _sb.Append((char)map[y,i]);
        }

        return _sb.ToString();
    }
}
