using AdventOfCodeSupport;

namespace AdventOfCode.Helpers;

public static class InputBlockExtensions
{
    public static byte[][] ToByteMap(this InputBlock block)
    {
        var map = new byte[block.Lines.Length][];
        for (var i = 0; i < map.Length; i++)
        {
            map[i] = block.Lines[i].Select(x => (byte)x).ToArray();
        }

        return map;
    }
}
