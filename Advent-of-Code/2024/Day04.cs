using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day04 : AdventBase
{
    protected override object InternalPart1()
    {
        var map = Input.Lines;
        var allXmas = 0;
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[0].Length; x++)
            {
                if (map[y][x] != 'X') continue;
                allXmas += CheckLetter(map, y, x, 1, -1, 'M');
                allXmas += CheckLetter(map, y, x, 1, 0, 'M');
                allXmas += CheckLetter(map, y, x, 1, 1, 'M');
                allXmas += CheckLetter(map, y, x, 0, -1, 'M');
                allXmas += CheckLetter(map, y, x, 0, 1, 'M');
                allXmas += CheckLetter(map, y, x, -1, -1, 'M');
                allXmas += CheckLetter(map, y, x, -1, 0, 'M');
                allXmas += CheckLetter(map, y, x, -1, 1, 'M');
            }
        }

        return allXmas;
    }

    protected override object InternalPart2()
    {
        var map = Input.Lines;
        var allXmas = 0;
        for (var y = 1; y < map.Length - 1; y++)
        {
            for (var x = 1; x < map[0].Length - 1; x++)
            {
                if (map[y][x] != 'A') continue;
                var diag = 0;
                if (map[y - 1][x - 1] == 'M' && map[y + 1][x + 1] == 'S' ||
                    map[y - 1][x - 1] == 'S' && map[y + 1][x + 1] == 'M')
                    diag++;
                if (map[y + 1][x - 1] == 'M' && map[y - 1][x + 1] == 'S' ||
                    map[y + 1][x - 1] == 'S' && map[y - 1][x + 1] == 'M')
                    diag++;
                if (diag == 2) allXmas++;
            }
        }

        return allXmas;
    }

    private static int CheckLetter(string[] map, int y, int x, int dirY, int dirX, char letter)
    {
        while (true)
        {
            var newY = y + dirY;
            var newX = x + dirX;
            if (newY < 0 || newX < 0 || newY >= map.Length || newX >= map[0].Length) return 0;
            if (map[newY][newX] != letter) return 0;
            if (letter == 'S') return 1;
            var nextLetter = letter switch
            {
                'M' => 'A',
                'A' => 'S',
                _ => throw new Exception()
            };
            y = newY;
            x = newX;
            letter = nextLetter;
        }
    }
}
