using System.Numerics;
using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day11 : AdventBase
{
    private abstract class Monkey
    {
        public Queue<long> Items { get; set; }
        public int Value { get; set; }
        public int DivisibleBy { get; set; }
        public int IfTrue { get; set; }
        public int IfFalse { get; set; }
        public int Inspected { get; set; }

        public abstract long Operation(long old);
    }

    private class MonkeyAdd : Monkey
    {
        public override long Operation(long old)
        {
            return old + Value;
        }
    }
    
    private class MonkeyMult : Monkey
    {
        public override long Operation(long old)
        {
            return old * Value;
        }
    }

    private class MonkeySquare : Monkey
    {
        public override long Operation(long old)
        {
            return old * old;
        }
    }

    private List<Monkey> ParseMonkeys()
    {
        var monkeys = new List<Monkey>();
        foreach (var monkey in Input.Blocks)
        {
            var opLine = monkey.Lines[2];
            var opSplit = opLine.Split(' ');
            var value = opSplit[^1];
            var op = opSplit[^2];
            Monkey newMonkey;
            if (value == "old") newMonkey = new MonkeySquare();
            else if (op == "+") newMonkey = new MonkeyAdd { Value = int.Parse(value) };
            else newMonkey = new MonkeyMult { Value = int.Parse(value) };

            var itemLine = monkey.Lines[1].Split(':')[^1];
            itemLine = itemLine.Replace(" ", "");
            var items = itemLine.Split(',').Select(long.Parse).ToList();
            newMonkey.Items = new Queue<long>(items);
            newMonkey.DivisibleBy = int.Parse(monkey.Lines[3].Split(' ')[^1]);
            newMonkey.IfTrue = int.Parse(monkey.Lines[^2].Split(' ')[^1]);
            newMonkey.IfFalse = int.Parse(monkey.Lines[^1].Split(' ')[^1]);
            monkeys.Add(newMonkey);
        }

        return monkeys;
    }

    protected override object InternalPart1()
    {
        var monkeys = ParseMonkeys();

        for (var i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Count > 0)
                {
                    var item = monkey.Items.Dequeue();
                    item = monkey.Operation(item);
                    item /= 3;
                    if (item % monkey.DivisibleBy == 0) monkeys[monkey.IfTrue].Items.Enqueue(item);
                    else monkeys[monkey.IfFalse].Items.Enqueue(item);

                    monkey.Inspected++;
                }
            }
        }

        var mostActive = monkeys.OrderByDescending(x => x.Inspected).Take(2).ToList();
        var result = mostActive[0].Inspected * mostActive[1].Inspected;
        
        return result;
    }

    protected override object InternalPart2()
    {
        var monkeys = ParseMonkeys();

        var lcd = monkeys.Aggregate<Monkey?, long>(1, (current, monkey) => current * monkey!.DivisibleBy);

        for (var i = 0; i < 10_000; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Count > 0)
                {
                    var item = monkey.Items.Dequeue();
                    item = monkey.Operation(item);
                    item %= lcd;
                    if (item % monkey.DivisibleBy == 0) monkeys[monkey.IfTrue].Items.Enqueue(item);
                    else monkeys[monkey.IfFalse].Items.Enqueue(item);

                    monkey.Inspected++;
                }
            }
        }

        var mostActive = monkeys.OrderByDescending(x => x.Inspected).Take(2).ToList();
        var result = new BigInteger(mostActive[0].Inspected) * new BigInteger(mostActive[1].Inspected);
        
        return result;
    }
}
