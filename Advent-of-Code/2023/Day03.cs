using AdventOfCode.Helpers;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public class Day03 : AdventBase
{
    private char[][] _map;
    private Dictionary<Point2d, int> _gears = new();

    protected override void InternalOnLoad()
    {
        _map = Input.Lines.Select(x => x.ToArray()).ToArray();
    }

    private bool NextToSymbol(int y, int x)
    {
        for (var y2 = -1; y2 <= 1; y2++)
        {
            for (var x2 = -1; x2 <= 1; x2++)
            {
                var checkX = x + x2;
                var checkY = y + y2;
                if (checkX < 0 || checkY < 0) continue;
                if (checkX >= _map[0].Length || checkY >= _map.Length) continue;
                if (!char.IsAsciiDigit(_map[checkY][checkX]) && _map[checkY][checkX] != '.')
                {
                    return true;
                }
            }
        }

        return false;
    }

    private Point2d? NextToGear(int y, int x)
    {
        for (var y2 = -1; y2 <= 1; y2++)
        {
            for (var x2 = -1; x2 <= 1; x2++)
            {
                var checkX = x + x2;
                var checkY = y + y2;
                if (checkX < 0 || checkY < 0) continue;
                if (checkX >= _map[0].Length || checkY >= _map.Length) continue;
                if (_map[checkY][checkX] == '*')
                {
                    return new Point2d(checkY, checkX);
                }
            }
        }

        return null;
    }

    protected override object InternalPart1()
    {
        int? numStart = null;
        int? numEnd = null;
        bool symbol = false;
        long sum = 0;
        for (var y = 0; y < _map.Length; y++)
        {
            for (var x = 0; x < _map[0].Length; x++)
            {
                if (char.IsAsciiDigit(_map[y][x]))
                {
                    if (numStart is null)
                    {
                        numStart = x;
                    }

                    if (!symbol && NextToSymbol(y, x))
                    {
                        symbol = true;
                    }

                    if (x == _map[0].Length - 1)
                    {
                        numEnd = x;
                    }
                }
                else
                {
                    if (numStart is not null && numEnd is null)
                    {
                        numEnd = x - 1;
                    }
                }

                if (numEnd is not null)
                {
                    if (symbol)
                    {
                        var num = "";
                        for (var i = (int)numStart!; i <= numEnd; i++)
                        {
                            num += _map[y][i];
                        }

                        sum += int.Parse(num);
                    }

                    numStart = null;
                    numEnd = null;
                    symbol = false;
                }
            }
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        int? numStart = null;
        int? numEnd = null;
        Point2d? gear = null;
        long sum = 0;
        for (var y = 0; y < _map.Length; y++)
        {
            for (var x = 0; x < _map[0].Length; x++)
            {
                if (char.IsAsciiDigit(_map[y][x]))
                {
                    if (numStart is null)
                    {
                        numStart = x;
                    }

                    if (gear is null)
                    {
                        var location = NextToGear(y, x);
                        if (location is not null) gear = location;
                    }

                    if (x == _map[0].Length - 1)
                    {
                        numEnd = x;
                    }
                }
                else
                {
                    if (numStart is not null && numEnd is null)
                    {
                        numEnd = x - 1;
                    }
                }

                if (numEnd is not null)
                {
                    if (gear is not null)
                    {
                        var num = "";
                        for (var i = (int)numStart!; i <= numEnd; i++)
                        {
                            num += _map[y][i];
                        }

                        var value = int.Parse(num);
                        if (!_gears.ContainsKey((Point2d)gear))
                        {
                            _gears[(Point2d)gear] = value;
                        }
                        else if (_gears[(Point2d)gear] >= 0)
                        {
                            sum += _gears[(Point2d)gear] * value;
                            _gears[(Point2d)gear] = -1;
                        }
                    }

                    numStart = null;
                    numEnd = null;
                    gear = null;
                }
            }
        }

        return sum;
    }
}
