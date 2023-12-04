using AdventOfCode.Helpers;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public class Day03 : AdventBase
{
    private byte[][] _map = null!;
    private readonly Dictionary<Point2d, int> _gears = new();

    protected override void InternalOnLoad()
    {
        _map = Input.ToByteMap();
    }

    protected override object InternalPart1()
    {
        int? numStart = null;
        int? numEnd = null;
        bool symbol = false;
        long sum = 0;
        _map.For((y, x) =>
        {
            if (_map[y][x] is >= (byte)'0' and <= (byte)'9')
            {
                numStart ??= x;
                if (!symbol)
                {
                    _map.Adjacent(y, x, (y2, x2) =>
                    {
                        if (_map[y2][x2] is (< (byte)'0' or > (byte)'9') and not (byte)'.')
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
                var num = _map[y].SubString(numStart, numEnd);
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
            if (_map[y][x] is >= (byte)'0' and <= (byte)'9')
            {
                numStart ??= x;

                if (gear is null)
                {
                    _map.Adjacent(y, x, (y2, x2) =>
                    {
                        if (_map[y2][x2] is (byte)'*')
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
                var num = _map[y].SubString(numStart, numEnd);
                var value = int.Parse(num);

                if (!_gears.TryAdd((Point2d)gear, value))
                    sum += _gears[(Point2d)gear] * value;
            }

            numStart = null;
            numEnd = null;
            gear = null;
        });

        return sum;
    }
}
