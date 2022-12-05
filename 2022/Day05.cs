using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day05 : AdventBase
{
    private string[] _instructions;
    private Stack<char>[] _blocks;

    private void PrepBlocks()
    {
        var split = InputText.Trim().Split("\n\n");
        _instructions = split[1].Split('\n');
        var blocks = split[0].Split('\n');
        _blocks = new Stack<char>[9];
        for (var i = 0; i < 9; i++)
        {
            _blocks[i] = new Stack<char>();
        }
        for (var row = blocks.Length - 2; row >= 0; row--)
        {
            var col = -1;
            for (var c = 1; c < blocks[row].Length; c += 4)
            {
                col++;
                if (blocks[row][c] == ' ') continue;
                _blocks[col].Push(blocks[row][c]);
            }
        }
    }
    
    protected override void InternalPart1()
    {
        PrepBlocks();
        foreach (var instruction in _instructions)
        {
            var split = instruction.Split(' ');
            MoveBlocks(split[1], split[3], split[5]);
        }

        var result = "";
        foreach (var stack in _blocks)
        {
            result += stack.Pop();
        }
        Console.WriteLine(result);
    }

    private void MoveBlocks(string count, string from, string to)
    {
        MoveBlocks(int.Parse(count), int.Parse(from), int.Parse(to));
    }

    private void MoveBlocks(int count, int from, int to)
    {
        for (var i = 0; i < count; i++)
        {
            var c = _blocks[from - 1].Pop();
            _blocks[to-1].Push(c);
        }
    }

    private void MoveBlocksP2(string count, string from, string to)
    {
        MoveBlocksP2(int.Parse(count), int.Parse(from), int.Parse(to));
    }

    private void MoveBlocksP2(int count, int from, int to)
    {
        var tempStack = new Stack<char>();
        for (var i = 0; i < count; i++)
        {
            var c = _blocks[from - 1].Pop();
            tempStack.Push(c);
        }
        for (var i = 0; i < count; i++)
        {
            var c = tempStack.Pop();
            _blocks[to-1].Push(c);
        }
    }

    protected override void InternalPart2()
    {
        PrepBlocks();
        foreach (var instruction in _instructions)
        {
            var split = instruction.Split(' ');
            MoveBlocksP2(split[1], split[3], split[5]);
        }

        var result = "";
        foreach (var stack in _blocks)
        {
            result += stack.Pop();
        }
        Console.WriteLine(result);
    }
}