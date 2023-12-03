using AdventOfCode.Helpers;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public class Day03 : AdventBase
{
    private char[][] _map = null!;
    private readonly Dictionary<Point2d, int> _gears = new();

    protected override void InternalOnLoad()
    {
        _map = Input.ToCharMap();
    }

    protected override object InternalPart1()
    {
        int? numStart = null;
        int? numEnd = null;
        bool symbol = false;
        long sum = 0;
        _map.For((y, x) =>
        {
            if (char.IsAsciiDigit(_map[y][x]))
            {
                numStart ??= x;
                if (!symbol)
                {
                    _map.Adjacent(y, x, (y2, x2) =>
                    {
                        if (_map[y2][x2] is (< '0' or > '9') and not '.')
                        {
                            symbol = true;
                        }
                    });
                }
                if (x == _map[0].Length - 1)
                    numEnd = x;
            }
            else if (numStart is not null && numEnd is null)
            {
                numEnd = x - 1;
            }

            if (numEnd is null) return;

            if (symbol)
            {
                var num = new string(_map[y], (int)numStart!, (int)numEnd - (int)numStart + 1);
                sum += int.Parse(num);
            }

            numStart = null;
            numEnd = null;
            symbol = false;
        });

        return sum;
    }

    protected override object InternalPart2()
    {
        int? numStart = null;
        int? numEnd = null;
        Point2d? gear = null;
        long sum = 0;
        _map.For((y, x) =>
        {
            if (char.IsAsciiDigit(_map[y][x]))
            {
                numStart ??= x;

                if (gear is null)
                {
                    _map.Adjacent(y, x, (y2, x2) =>
                    {
                        if (_map[y2][x2] is '*')
                        {
                            gear = new Point2d(y2, x2);
                        }
                    });
                }

                if (x == _map[0].Length - 1)
                    numEnd = x;
            }
            else if (numStart is not null && numEnd is null)
            {
                numEnd = x - 1;
            }

            if (numEnd is null) return;

            if (gear is not null)
            {
                var num = new string(_map[y], (int)numStart!, (int)numEnd - (int)numStart + 1);
                var value = int.Parse(num);

                var gear2d = (Point2d)gear;
                if (!_gears.TryAdd(gear2d, value) && _gears[gear2d] >= 0)
                {
                    sum += _gears[gear2d] * value;
                    _gears[gear2d] = -1;
                }
            }

            numStart = null;
            numEnd = null;
            gear = null;
        });

        return sum;
    }
}
