using AdventOfCode.Helpers;
using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day12 : AdventBase
{
    private Point2d _start;
    private Point2d _end;

    private void FindStartAndEnd(char[][] map)
    {
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[0].Length; x++)
            {
                switch (map[y][x])
                {
                    case 'S':
                        _start = new Point2d(y, x);
                        map[y][x] = 'a';
                        break;
                    case 'E':
                        _end = new Point2d(y, x);
                        map[y][x] = 'z';
                        break;
                }
            }
        }
    }

    private bool EndFound(Point2d nextPoint, char nextPointChar) => nextPoint == _end;
    private bool ValidStep(char oldPositionChar, char newPositionChar) => newPositionChar - oldPositionChar <= 1;

    protected override object InternalPart1()
    {
        var map = Input.ToCharMap();
        FindStartAndEnd(map);
        return map.ShortestPathSteps(_start, EndFound, ValidStep);
    }


    protected override object InternalPart2()
    {
        var map = Input.ToCharMap();
        FindStartAndEnd(map);
        return map.ShortestPathSteps(_end, (_, c) => c == 'a', (oldChar, newChar) => newChar - oldChar >= -1);
    }
}
