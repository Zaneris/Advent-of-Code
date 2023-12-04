using AdventOfCode.Helpers;
using AdventOfCodeSupport;
using CommunityToolkit.HighPerformance;

namespace AdventOfCode._2023;

public class Day03 : AdventBase
{
    private readonly Dictionary<Point2d, int> _gears = new();

    protected override void InternalOnLoad()
    {
        var bytes = Input.Bytes; // Pre-load bytes
    }

    private bool Adjacent(ReadOnlySpan2D<byte> map, int y, int x, int size = 1)
    {
        for (var y2 = -size; y2 <= size; y2++)
        {
            for (var x2 = -size; x2 <= size; x2++)
            {
                var checkX = x + x2;
                var checkY = y + y2;

                if (checkX < 0 || checkY < 0) continue;
                if (checkX >= map.Width || checkY >= map.Height) continue;
                if (x2 == 0 && y2 == 0) continue;
                if (map[checkY,checkX] is (< (byte)'0' or > (byte)'9') and not (byte)'.')
                    return true;
            }
        }

        return false;
    }

    protected override object InternalPart1()
    {
        int? numStart = null;
        int? numEnd = null;
        bool symbol = false;
        long sum = 0;
        var map = Input.Span2D;

        for (var y = 0; y < map.Height; y++)
        {
            for (var x = 0; x < map.Width; x++)
            {
                if (map[y,x] is >= (byte)'0' and <= (byte)'9')
                {
                    numStart ??= x;
                    if (!symbol && Adjacent(map, y, x))
                    {
                        symbol = true;
                    }

                    if (x == map.Width - 1)
                        numEnd = x;
                }
                else if (numStart is not null && numEnd is null)
                {
                    numEnd = x - 1;
                }

                if (numEnd is null) continue;

                if (symbol)
                {
                    var num = map.SubString(y, (int)numStart!, (int)numEnd);
                    sum += int.Parse(num);
                }

                numStart = null;
                numEnd = null;
                symbol = false;
            }
        }

        return sum;
    }

    private Point2d? Adjacent2(ReadOnlySpan2D<byte> map, int y, int x, int size = 1)
    {
        for (var y2 = -size; y2 <= size; y2++)
        {
            for (var x2 = -size; x2 <= size; x2++)
            {
                var checkX = x + x2;
                var checkY = y + y2;

                if (checkX < 0 || checkY < 0) continue;
                if (checkX >= map.Width || checkY >= map.Height) continue;
                if (x2 == 0 && y2 == 0) continue;
                if (map[checkY,checkX] is (byte)'*')
                    return new Point2d(checkY, checkX);
            }
        }

        return null;
    }

    protected override object InternalPart2()
    {
        int? numStart = null;
        int? numEnd = null;
        Point2d? gear = null;
        long sum = 0;
        var map = Input.Span2D;

        for (var y = 0; y < map.Height; y++)
        {
            for (var x = 0; x < map.Width; x++)
            {
                if (map[y,x] is >= (byte)'0' and <= (byte)'9')
                {
                    numStart ??= x;

                    if (gear is null)
                    {
                        var location = Adjacent2(map, y, x);
                        if (location is not null) gear = location;
                    }

                    if (x == map.Width - 1)
                        numEnd = x;
                }
                else if (numStart is not null && numEnd is null)
                {
                    numEnd = x - 1;
                }

                if (numEnd is null) continue;

                if (gear is not null)
                {
                    var num = map.SubString(y, (int)numStart!, (int)numEnd);
                    var value = int.Parse(num);

                    if (!_gears.TryAdd((Point2d)gear, value))
                        sum += _gears[(Point2d)gear] * value;
                }

                numStart = null;
                numEnd = null;
                gear = null;
            }
        }

        return sum;
    }
}
