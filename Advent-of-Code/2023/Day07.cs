using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public class Day07 : AdventBase
{
    private record HandBid(string Hand, long Bid);

    private enum Hands
    {
        HighCard = 0,
        OnePair = 1,
        TwoPair = 2,
        ThreeOfAKind = 3,
        FullHouse = 4,
        FourOfAKind = 5,
        FiveOfAKind = 6
    }

    private static Hands GetHandType(string hand)
    {
        var groups = hand.GroupBy(x => x).ToArray();
        if (groups.Any(x => x.Count() == 5)) return Hands.FiveOfAKind;
        if (groups.Any(x => x.Count() == 4)) return Hands.FourOfAKind;
        if (groups.Any(x => x.Count() == 3) && groups.Any(x => x.Count() == 2)) return Hands.FullHouse;
        if (groups.Any(x => x.Count() == 3)) return Hands.ThreeOfAKind;
        if (groups.Count(x => x.Count() == 2) == 2) return Hands.TwoPair;
        if (groups.Any(x => x.Count() == 2)) return Hands.OnePair;
        return Hands.HighCard;
    }

    private static Hands GetHandsWithJoker(string hand, bool joker)
    {
        var handType = GetHandType(hand);
        var jokers = hand.Count(x => x == 'J');
        if (!joker || jokers == 0) return handType;
        return jokers switch
        {
            1 => handType switch
            {
                Hands.OnePair => Hands.ThreeOfAKind,
                Hands.TwoPair => Hands.FullHouse,
                Hands.ThreeOfAKind => Hands.FourOfAKind,
                Hands.FourOfAKind => Hands.FiveOfAKind,
                _ => handType
            },
            2 => handType switch
            {
                Hands.OnePair => Hands.ThreeOfAKind, // Pair would be jokers
                Hands.TwoPair => Hands.FourOfAKind, // One pair is jokers
                Hands.FullHouse => Hands.FiveOfAKind,
                _ => handType
            },
            3 => handType switch
            {
                Hands.ThreeOfAKind => Hands.FourOfAKind,
                Hands.FullHouse => Hands.FiveOfAKind,
                _ => handType
            },
            _ => handType switch
            {
                Hands.FourOfAKind => Hands.FiveOfAKind,
                _ => handType
            }
        };
    }

    private static int CardStrength(char card, bool joker = false) => card switch
    {
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'J' => joker ? 1 : 11,
        'T' => 10,
        _ => int.Parse(card.ToString())
    };

    private static int WinningHand(HandBid hand1, HandBid hand2, bool joker = false)
    {
        var handType1 = GetHandsWithJoker(hand1.Hand, joker);
        var handType2 = GetHandsWithJoker(hand2.Hand, joker);
        if (handType1 != handType2) return (int)handType1 > (int)handType2 ? 1 : -1;
        for (var i = 0; i < 5; i++)
        {
            if (hand1.Hand[i] == hand2.Hand[i]) continue;
            return CardStrength(hand1.Hand[i], joker) > CardStrength(hand2.Hand[i], joker) ? 1 : -1;
        }

        return 0;
    }

    private List<HandBid> _handBids;

    protected override void InternalOnLoad()
    {
        _handBids = [];
        foreach (var line in Input.Lines)
        {
            var split = line.Split(' ');
            _handBids.Add(new HandBid(split[0], long.Parse(split[1])));
        }
    }

    protected override object InternalPart1()
    {
        _handBids.Sort((h1, h2) => WinningHand(h1, h2));

        return _handBids.Select((x, i) => x.Bid * (i + 1)).Sum();
    }

    protected override object InternalPart2()
    {
        _handBids.Sort((h1, h2) => WinningHand(h1, h2, true));

        return _handBids.Select((x, i) => x.Bid * (i + 1)).Sum();
    }
}
