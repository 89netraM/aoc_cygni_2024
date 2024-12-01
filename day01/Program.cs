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

public class BenchmarkProgram
{
    [Benchmark]
    public int PART1() => Part1();

    public static int Part1()
    {
        using var input = File.OpenText("./input.txt");
        List<int> left = [], right = [];
        GetColumns(input, left.Add, right.Add);
        return left.Order().Zip(right.Order(), (l, r) => int.Abs(l - r)).Sum();
    }

    [Benchmark]
    public int PART2() => Part2();

    public static int Part2()
    {
        using var input = File.OpenText("./input.txt");
        List<int> left = [];
        Dictionary<int, int> right = [];
        GetColumns(input, left.Add, i => { if (!right.TryAdd(i, 1)) { right[i]++; } });
        return left.Sum(l => l * right.GetValueOrDefault(l));
    }

    private static void GetColumns(StreamReader input, Action<int> addLeft, Action<int> addRight)
    {
        while (input.ReadLine() is string line)
        {
            var columns = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            addLeft(int.Parse(columns[0]));
            addRight(int.Parse(columns[1]));
        }
    }
}
