using System.Numerics;
using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public class Day08 : AdventBase
{
    private record Node(string Left, string Right);

    private readonly Dictionary<string, Node> _nodes = [];
    private string _pattern = "";

    protected override void InternalOnLoad()
    {
        _pattern = Input.Lines[0];
        foreach (var line in Input.Lines.Skip(2))
        {
            var groups = Regex.Match(line, @"^(\w{3}).{4}(\w{3}),\s(\w{3})").Groups;
            _nodes[groups[1].Value] = new Node(groups[2].Value, groups[3].Value);
        }
    }

    private long FindSteps(string startingNode, bool isP2 = false)
    {
        var current = startingNode;
        var p = 0;
        long steps = 0;
        while (isP2 ? current[2] != 'Z' : current != "ZZZ")
        {
            var node = _nodes[current];
            current = _pattern[p] switch
            {
                'L' => node.Left,
                'R' => node.Right,
                _ => throw new Exception("Invalid direction")
            };
            p = (p + 1) % _pattern.Length;
            steps++;
        }

        return steps;
    }

    protected override object InternalPart1()
    {
        return FindSteps("AAA");
    }

    private static long Lcm(IReadOnlyList<long> numbers)
    {
        BigInteger lcm = numbers[0];
        for (var i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] == 0) break;
            lcm = lcm / BigInteger.GreatestCommonDivisor(lcm, numbers[i]) * numbers[i];
        }
        return (long)lcm;
    }

    protected override object InternalPart2()
    {
        var steps = _nodes.Where(x => x.Key[2] == 'A').Select(x => FindSteps(x.Key, true)).ToArray();

        return Lcm(steps);
    }
}
