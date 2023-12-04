using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public class Day04 : AdventBase
{
    protected override object InternalPart1()
    {
        var cards = Input.Lines;
        long points = 0;
        foreach (var card in cards)
        {
            var numbers = card.Split(':')[1].Split('|');
            var drawn = Regex.Matches(numbers[0], @"\d+").Select(x => int.Parse(x.Value));
            var picked = Regex.Matches(numbers[1], @"\d+").Select(x => int.Parse(x.Value));
            var intersect = drawn.Intersect(picked).ToHashSet();
            if (intersect.Count > 0)
            {
                points += (long)Math.Pow(2f, intersect.Count - 1);
            }
        }
        return points;
    }

    protected override object InternalPart2()
    {
        var cards = Input.Lines;
        var cardNum = 0;
        var available = new Dictionary<int, int>();
        foreach (var card in cards)
        {
            cardNum++;
            available[cardNum] = 1;
        }

        cardNum = 0;
        foreach (var card in cards)
        {
            cardNum++;
            var numbers = card.Split(':')[1].Split('|');
            var drawn = Regex.Matches(numbers[0], @"\d+").Select(x => int.Parse(x.Value));
            var picked = Regex.Matches(numbers[1], @"\d+").Select(x => int.Parse(x.Value));
            var intersect = drawn.Intersect(picked).ToHashSet();
            if (intersect.Count <= 0) continue;
            for (var i = 0; i < available[cardNum]; i++)
            {
                for (var add = 1; add <= intersect.Count; add++)
                {
                    available[cardNum + add]++;
                }
            }
        }
        return available.Sum(x => x.Value);
    }
}
