using Advent_of_Code.Dependencies;

var solutions = new Solutions();
var today = solutions
    .Where(x => x.Year == 2022)
    .OrderByDescending(x => x.Day)
    .First();

today.Part1().Part2();
