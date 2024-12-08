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
            Console.WriteLine(
                $"LEADERBOARD::{r.BenchmarkCase.Descriptor.WorkloadMethodDisplayInfo}::TIME {double.Round(r.ResultStatistics.Mean)} ns"
            );
        break;
}

public partial class BenchmarkProgram
{
    [Benchmark]
    public long PART1() => Part1();

    public static long Part1()
    {
        var map = File.ReadAllLines("./input.txt");
        long count = 0;
        for (int y = 0; y < map.Length; y++)
        for (int x = 0; x < map[y].Length; x++)
        {
            if (
                x + 3 < map[y].Length
                && (map[y][x], map[y][x + 1], map[y][x + 2], map[y][x + 3])
                    is
                        ('X', 'M', 'A', 'S')
                        or
                        ('S', 'A', 'M', 'X')
            )
            {
                count++;
            }
            if (
                y + 3 < map.Length
                && (map[y][x], map[y + 1][x], map[y + 2][x], map[y + 3][x])
                    is
                        ('X', 'M', 'A', 'S')
                        or
                        ('S', 'A', 'M', 'X')
            )
            {
                count++;
            }
            if (
                y + 3 < map.Length
                && x + 3 < map[y].Length
                && (map[y][x], map[y + 1][x + 1], map[y + 2][x + 2], map[y + 3][x + 3])
                    is
                        ('X', 'M', 'A', 'S')
                        or
                        ('S', 'A', 'M', 'X')
            )
            {
                count++;
            }
            if (
                y + 3 < map.Length
                && x - 3 >= 0
                && (map[y][x], map[y + 1][x - 1], map[y + 2][x - 2], map[y + 3][x - 3])
                    is
                        ('X', 'M', 'A', 'S')
                        or
                        ('S', 'A', 'M', 'X')
            )
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark]
    public long PART2() => Part2();

    public static long Part2()
    {
        var map = File.ReadAllLines("./input.txt");
        long count = 0;
        for (int y = 0; y < map.Length - 2; y++)
        for (int x = 0; x < map[y].Length - 2; x++)
        {
            if (
                (map[y][x], map[y + 1][x + 1], map[y + 2][x + 2])
                    is
                        ('M', 'A', 'S')
                        or
                        ('S', 'A', 'M')
                && (map[y + 2][x], map[y + 1][x + 1], map[y][x + 2])
                    is
                        ('M', 'A', 'S')
                        or
                        ('S', 'A', 'M')
            )
            {
                count++;
            }
        }
        return count;
    }
}
