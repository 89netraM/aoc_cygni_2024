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
        return ReadNums(input).Count(IsSafe);
    }

    [Benchmark]
    public int PART2() => Part2();

    public static int Part2()
    {
        using var input = File.OpenText("./input.txt");
        return ReadNums(input)
            .Count(nums =>
            {
                if (IsSafe(nums))
                {
                    return true;
                }
                for (int i = 0; i < nums.Count; i++)
                {
                    var removed = nums[i];
                    nums.RemoveAt(i);
                    if (IsSafe(nums))
                    {
                        return true;
                    }
                    nums.Insert(i, removed);
                }
                return false;
            });
    }

    private static IEnumerable<List<long>> ReadNums(StreamReader input)
    {
        while (input.ReadLine() is string line)
        {
            yield return line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(long.Parse)
                .ToList();
        }
    }

    private static bool IsSafe(IReadOnlyList<long> nums)
    {
        var diff = nums.Zip(nums.Skip(1), (a, b) => b - a).ToArray();
        return (diff[0] < 0 ? diff.All(l => l < 0) : diff.All(l => l > 0))
            && diff.All(l => long.Abs(l) <= 3);
    }
}
