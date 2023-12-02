using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public class Day02 : AdventBase
{
    private record CubeCount(int Cubes, string Colour);

    private record Game(int Id, List<List<CubeCount>> Sets);

    private List<Game> _games;

    protected override void InternalOnLoad()
    {
        _games = [];

        foreach (var line in Input.Lines)
        {
            _games.Add(ParseGame(line));
        }
    }

    private static Game ParseGame(string game)
    {
        var firstSplit = game.Split(':');
        var id = int.Parse(firstSplit[0].Split(' ')[1]);
        var cubes = new List<List<CubeCount>>();
        foreach (var cubeSet in firstSplit[1].Split(';'))
        {
            var set = new List<CubeCount>();
            foreach (var cubeRoll in cubeSet.Split(','))
            {
                var trimmed = cubeRoll.Trim();
                var secondSplit = trimmed.Split(' ');
                var cubeCount = new CubeCount(int.Parse(secondSplit[0]), secondSplit[1]);
                set.Add(cubeCount);
            }
            cubes.Add(set);
        }

        return new Game(id, cubes);
    }

    protected override object InternalPart1()
    {
        var dict = new Dictionary<string, int>();

        var sum = 0;
        foreach (var game in _games)
        {
            var failed = false;
            foreach (var cubeSet in game.Sets)
            {
                dict["red"] = 0;
                dict["green"] = 0;
                dict["blue"] = 0;
                foreach (var cubeCount in cubeSet)
                {
                    if (dict.ContainsKey(cubeCount.Colour))
                        dict[cubeCount.Colour] += cubeCount.Cubes;
                    else
                        dict[cubeCount.Colour] = cubeCount.Cubes;
                }

                if (dict["red"] <= 12 && dict["green"] <= 13 && dict["blue"] <= 14) continue;
                failed = true;
                break;
            }
            if (failed) continue;
            sum += game.Id;
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        var dict = new Dictionary<string, int>();

        var mins = new Dictionary<string, int>();

        long sum = 0;
        foreach (var game in _games)
        {
            mins["red"] = 0;
            mins["green"] = 0;
            mins["blue"] = 0;
            foreach (var cubeSet in game.Sets)
            {
                dict["red"] = 0;
                dict["green"] = 0;
                dict["blue"] = 0;
                foreach (var cubeCount in cubeSet)
                {
                    if (dict.ContainsKey(cubeCount.Colour))
                        dict[cubeCount.Colour] += cubeCount.Cubes;
                    else
                        dict[cubeCount.Colour] = cubeCount.Cubes;
                }

                if (dict["red"] > mins["red"])
                    mins["red"] = dict["red"];
                if (dict["green"] > mins["green"])
                    mins["green"] = dict["green"];
                if (dict["blue"] > mins["blue"])
                    mins["blue"] = dict["blue"];
            }

            long power = mins["red"] * mins["green"] * mins["blue"];
            sum += power;
        }

        return sum;
    }
}
