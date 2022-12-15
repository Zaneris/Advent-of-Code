using System.Text;
using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day13 : AdventBase
{
    private abstract class D13
    {
        public abstract override string ToString();
    }

    private class D13List : D13
    {
        public List<D13> List { get; } = new();
        public override string ToString()
        {
            var chain = List.Select(x => x.ToString());
            var joined = string.Join(',', chain);
            return $"[{joined}]";
        }
    }

    private class D13Num : D13
    {
        public int Num { get; set; }
        public override string ToString()
        {
            return Num.ToString();
        }
    }

    private D13List BuildTree(string line)
    {
        var stack = new Stack<D13List>();
        var index = 0;
        var regex = new Regex(@"^(\[)|^(\d+),?|^(\]),?");
        while (index < line.Length - 1)
        {
            var sub = line[index..];
            var result = regex.Match(sub).Groups;
            if (result[1].Value != string.Empty)
            {
                var next = new D13List();
                if (stack.Count > 0) stack.Peek().List.Add(next);
                stack.Push(next);
                index++;
            }
            else if (result[2].Value != string.Empty)
            {
                var num = new D13Num { Num = int.Parse(result[2].Value) };
                stack.Peek().List.Add(num);
                index += result[0].Value.Length;
            }
            else if (result[3].Value != string.Empty)
            {
                stack.Pop();
                index += result[0].Value.Length;
            }
        }

        var toReturn = stack.Pop();
        if (toReturn.ToString() != line)
            throw new Exception("Mismatch");
        return toReturn;
    }

    private static bool? RightOrder(D13List tree1, D13List tree2)
    {
        var i = 0;
        for (; i < tree1.List.Count ; i++)
        {
            if (i >= tree2.List.Count) return false;
            var item1 = tree1.List[i];
            var item2 = tree2.List[i];

            {
                if (item1 is D13Num num1 && item2 is D13Num num2)
                {
                    if (num1.Num < num2.Num) return true;
                    if (num1.Num > num2.Num) return false;
                    continue;
                }
            }

            {
                if (item1 is D13Num num1 && item2 is D13List list2)
                {
                    var list1 = new D13List();
                    list1.List.Add(num1);
                    var result = RightOrder(list1, list2);
                    if (result is not null) return result;
                }
            }

            {
                if (item1 is D13List list1 && item2 is D13Num num2)
                {
                    var list2 = new D13List();
                    list2.List.Add(num2);
                    var result = RightOrder(list1, list2);
                    if (result is not null) return result;
                }
            }

            {
                if (item1 is D13List list1 && item2 is D13List list2)
                {
                    var result = RightOrder(list1, list2);
                    if (result is not null) return result;
                }
            }
        }

        if (i < tree2.List.Count) return true;
        return null;
    }

    protected override object InternalPart1()
    {
        var pair = 0;
        var sum = 0;
        foreach (var block in Input.Blocks)
        {
            pair++;
            var tree1 = BuildTree(block.Lines[0]);
            var tree2 = BuildTree(block.Lines[1]);
            var result = RightOrder(tree1, tree2);
            if (result is true) sum += pair;
        }
        return sum;
    }

    private class TreeComparer : IComparer<D13List>
    {
        public int Compare(D13List list1, D13List list2)
        {
            return Day13.RightOrder(list1, list2) is false ? 1 : -1;
        }
    }

    protected override object InternalPart2()
    {
        var trees = new List<D13List>();
        foreach (var block in Input.Blocks)
        {
            trees.Add(BuildTree(block.Lines[0]));
            trees.Add(BuildTree(block.Lines[1]));
        }
        trees.Add(BuildTree("[[2]]"));
        trees.Add(BuildTree("[[6]]"));

        trees.Sort(new TreeComparer());

        var two = 0;
        var six = 0;
        for (var i = 0; i < trees.Count; i++)
        {
            if (trees[i].ToString() == "[[2]]") two = i + 1;
            if (trees[i].ToString() == "[[6]]") six = i + 1;
        }

        return two * six;
    }
}
