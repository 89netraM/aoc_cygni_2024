using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

switch (Environment.GetEnvironmentVariable("part"))
{
    case "part1":
        Console.WriteLine(BenchmarkProgram.Part1());
        break;
    case "part2":
        Console.WriteLine(BenchmarkProgram.Part2());
        break;
    default:
        var result = BenchmarkRunner.Run<BenchmarkProgram>();
        foreach (var r in result.Reports)
            Console.WriteLine($"LEADERBOARD::{r.BenchmarkCase.Descriptor.WorkloadMethodDisplayInfo}::TIME {double.Round(r.ResultStatistics.Mean)} ns");
        break;
}

public partial class BenchmarkProgram
{
    [Benchmark]
    public long PART1() => Part1();

    public static long Part1() =>
        Part1Regex
            .Matches(File.ReadAllText("./input.txt"))
            .Select(m => long.Parse(m.Groups[1].Value) * long.Parse(m.Groups[2].Value))
            .Sum();

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex Part1Regex { get; }

    [Benchmark]
    public long PART2() => Part2();

    public static long Part2()
    {
        var enable = true;
        var sum = 0L;
        foreach (Match match in Part2Regex.Matches(File.ReadAllText("./input.txt")))
        {
            if (match.Value is "do()")
            {
                enable = true;
            }
            else if (match.Value is "don't()")
            {
                enable = false;
            }
            else if (enable)
            {
                sum += long.Parse(match.Groups[1].Value) * long.Parse(match.Groups[2].Value);
            }
        }
        return sum;
    }

    [GeneratedRegex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")]
    private static partial Regex Part2Regex { get; }
}
