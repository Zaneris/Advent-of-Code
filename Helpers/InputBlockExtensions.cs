using AdventOfCodeSupport;

namespace AdventOfCode.Helpers;

public static class InputBlockExtensions
{
    public static char[][] ToCharMap(this InputBlock block)
    {
        var map = new char[block.Lines.Length][];
        for (var i = 0; i < map.Length; i++)
        {
            map[i] = block.Lines[i].ToArray();
        }

        return map;
    }
}
