namespace AdventOfCode.Helpers;

/// <summary>
/// Path finding the shortest path between 2 points.
/// </summary>
public class ShortestPath
{
    private readonly char[][] _map;
    private readonly Func<Point, char, bool> _endRule;
    private readonly Dictionary<string, long> _seen = new();
    private readonly Queue<(Point position, long steps)> _queue = new();
    private readonly Func<char, char, bool> _validStep;
    private readonly Point[] _moves;
    private readonly long _yLimit;
    private readonly long _xLimit;

    private readonly Point[] _diagonals = { new(1, 1), new(-1, 1), new(1, -1), new(-1, -1) };
    private readonly Point[] _cardinals = { new(0, 1), new(1, 0), new(-1, 0), new(0, -1) };

    /// <summary>
    /// Setup to find shortest path between 2 points.
    /// </summary>
    /// <param name="map">The map to navigate.</param>
    /// <param name="start">Starting point.</param>
    /// <param name="endRule">When is the end found, passes point and map char of current position.</param>
    /// <param name="validStep">Is the step being attempted valid? Old map char, attempted map char.</param>
    /// <param name="diagonals">Can we move diagonally?</param>
    public ShortestPath(char[][] map, Point start, Func<Point, char, bool> endRule, Func<char, char, bool> validStep,
        bool diagonals = false)
    {
        _map = map;
        _endRule = endRule;
        _queue.Enqueue((start, 0));
        _validStep = validStep;
        _moves = diagonals ? _cardinals.Concat(_diagonals).ToArray() : _cardinals;
        _yLimit = _map.Length;
        _xLimit = _map[0].Length;
    }

    /// <summary>
    /// Start finding the path.
    /// </summary>
    /// <returns>Shortest number of steps needed.</returns>
    /// <exception cref="Exception">Could not find path.</exception>
    public long Run()
    {
        while (_queue.TryDequeue(out var target))
        {
            if (_seen.ContainsKey(target.position)) continue;
            _seen.Add(target.position, target.steps);
            var oldChar = _map[target.position.Y][target.position.X];
            foreach (var move in _moves)
            {
                var next = target.position + move;
                if (next.Y < 0 || next.Y >= _yLimit || next.X < 0 || next.X >= _xLimit) continue;
                if (_seen.ContainsKey(next)) continue;
                var nextChar = _map[next.Y][next.X];
                if (!_validStep(oldChar, nextChar)) continue;
                var steps = target.steps + 1;
                if (_endRule(next, nextChar)) return steps;
                _queue.Enqueue((next, steps));
            }
        }

        throw new Exception("Could not find path.");
    }
}
