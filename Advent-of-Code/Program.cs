using AdventOfCodeSupport;

var solutions = new AdventSolutions();
solutions.GetDay(2024,3).Benchmark();
var day = solutions.GetMostRecentDay();
await day.DownloadInputAsync();
day.Part1().Part2();
//await day.CheckPart1Async();
//await day.CheckPart2Async();
//await day.SubmitPart1Async();
//await day.SubmitPart2Async();
//day.Benchmark();
