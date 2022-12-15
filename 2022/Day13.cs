using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public partial class Day13 : AdventBase
{
    private class D13 { }

    private class D13Array : D13
    {
        public List<D13> D13S { get; } = new();
    }

    private class D13Num : D13
    {
        public int Num { get; set; }
    }

    private D13Array BuildTree(string line)
    {
        var root = new D13Array();
        var stack = new Stack<D13Array>();
        stack.Push(root);
        var index = 0;
        var regex = new Regex(@"^(\[)|^(\d+),?|^(\]),?");
        while (index < line.Length)
        {
            var sub = line[index..];
            var result = regex.Match(sub).Groups;
            if (result[1].Value != string.Empty)
            {
                var next = new D13Array();
                stack.Peek().D13S.Add(next);
                stack.Push(next);
                index++;
            }
            else if (result[2].Value != string.Empty)
            {
                var num = new D13Num { Num = int.Parse(result[2].Value) };
                stack.Peek().D13S.Add(num);
                index += result[0].Value.Length;
            }
            else if (result[3].Value != string.Empty)
            {
                stack.Pop();
                index += result[0].Value.Length;
            }
        }

        return root;
    }

    protected override object InternalPart1()
    {
        var tree = BuildTree(Input.Lines[0]);
        return 0;
    }

    protected override object InternalPart2()
    {
        return 0;
    }
}
