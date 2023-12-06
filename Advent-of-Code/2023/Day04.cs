using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public partial class Day04 : AdventBase
{
    private readonly HashSet<string> _hash = [];

    protected override object InternalPart1()
    {
        var cards = Input.Lines;
        long points = 0;
        foreach (var card in cards)
        {
            var numbers = card.Split(':')[1].Split('|');
            _hash.Clear();
            var drawn = Digits().Matches(numbers[0]);
            foreach (Match match in drawn)
            {
                _hash.Add(match.Value);
            }

            var count = 0;
            var picked = Digits().Matches(numbers[1]);
            foreach (Match match in picked)
            {
                if (_hash.Contains(match.Value)) count++;
            }
            if (count > 0)
            {
                points += (long)Math.Pow(2f, count - 1);
            }
        }
        return points;
    }

    protected override object InternalPart2()
    {
        var cards = Input.Lines;
        var available = new int[cards.Length];
        for (var i = 0; i < cards.Length; i++)
        {
            available[i] = 1;
        }

        var cardNum = -1;
        foreach (var card in cards)
        {
            cardNum++;
            var numbers = card.Split(':')[1].Split('|');
            _hash.Clear();
            var drawn = Digits().Matches(numbers[0]);
            foreach (Match match in drawn)
            {
                _hash.Add(match.Value);
            }

            var count = 0;
            var picked = Digits().Matches(numbers[1]);
            foreach (Match match in picked)
            {
                if(_hash.Contains(match.Value)) count++;
            }
            if (count <= 0) continue;
            for (var add = 1; add <= count; add++)
            {
                available[cardNum + add]+=available[cardNum];
            }
        }
        return available.Sum(x => x);
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex Digits();
}
