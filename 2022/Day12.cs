using AdventOfCode.Helpers;
using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day12 : AdventBase
{
    private Point _start;
    private Point _end;

    private void FindStartAndEnd(char[][] map)
    {
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[0].Length; x++)
            {
                switch (map[y][x])
                {
                    case 'S':
                        _start = new Point(y, x);
                        map[y][x] = 'a';
                        break;
                    case 'E':
                        _end = new Point(y, x);
                        map[y][x] = 'z';
                        break;
                }
            }
        }
    }

    protected override object InternalPart1()
    {
        var map = Input.ToCharMap();
        FindStartAndEnd(map);
        var pathFinder = new ShortestPath(map, _start, (point, _) => point == _end,
            (oldChar, newChar) => newChar - oldChar <= 1);
        return pathFinder.Run();
    }

    protected override object InternalPart2()
    {
        var map = Input.ToCharMap();
        FindStartAndEnd(map);
        var pathFinder = new ShortestPath(map, _end, (_, c) => c == 'a',
            (oldChar, newChar) => newChar - oldChar >= -1);
        return pathFinder.Run();
    }
}
