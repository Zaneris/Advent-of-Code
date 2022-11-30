using AdventOfCodeSupport;

var solutions = new Solutions();
var today = solutions
    .Where(x => x.Year == 2022)
    .OrderByDescending(x => x.Day)
    .First();

today.Part1();
