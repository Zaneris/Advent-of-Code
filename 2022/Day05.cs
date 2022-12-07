using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day05 : AdventBase
{
    private string[] _instructions;
    private Stack<char>[] _stacks;

    private void PrepStacks()
    {
        _instructions = Input.Blocks[1].Lines;
        var blocks = Input.Blocks[0].Lines;
        _stacks = new Stack<char>[9];
        for (var i = 0; i < 9; i++)
        {
            _stacks[i] = new Stack<char>();
        }
        for (var row = blocks.Length - 2; row >= 0; row--)
        {
            var col = -1;
            for (var c = 1; c < blocks[row].Length; c += 4)
            {
                col++;
                if (blocks[row][c] == ' ') continue;
                _stacks[col].Push(blocks[row][c]);
            }
        }
    }
    
    protected override object InternalPart1()
    {
        PrepStacks();
        MoveBlocks(true);
        return BuildResult();
    }

    protected override object InternalPart2()
    {
        PrepStacks();
        MoveBlocks(false);
        return BuildResult();
    }

    private string BuildResult()
    {
        var result = "";
        foreach (var stack in _stacks)
        {
            result += stack.Pop();
        }

        return result;
    }

    private void MoveBlocks(bool part1)
    {
        foreach (var instruction in _instructions)
        {
            var split = instruction.Split(' ');
            MoveBlocks(int.Parse(split[1]), int.Parse(split[3]), int.Parse(split[5]), part1);
        }
    }

    private void MoveBlocks(int count, int from, int to, bool part1)
    {
        if (part1)
        {
            for (var i = 0; i < count; i++)
            {
                var c = _stacks[from - 1].Pop();
                _stacks[to - 1].Push(c);
            }

            return;
        }
        
        var tempStack = new Stack<char>();
        for (var i = 0; i < count; i++)
        {
            var c = _stacks[from - 1].Pop();
            tempStack.Push(c);
        }
        for (var i = 0; i < count; i++)
        {
            var c = tempStack.Pop();
            _stacks[to - 1].Push(c);
        }
    }
}
