using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace aoc;

public class Day_11 : BaseDay
{
    private readonly List<List<long>> items = new List<List<long>>();
    private readonly List<(string, long)> operations = new List<(string, long)>();
    private readonly List<(long, int, int)> tests = new List<(long, int, int)>();
    private readonly List<int> inspectionCounts = new List<int>();

    private readonly List<List<long>> items2 = new List<List<long>>();
    private readonly List<long> inspectionCounts2 = new List<long>();

    public Day_11()
    {
        var lines = File.ReadLines(InputFilePath);
        int curMonkey = 0;
        var curTest = (0L, 0, 0);
        foreach (var line in lines)
        {
            if (line.StartsWith("Monkey"))
            {
                curMonkey = int.Parse(Regex.Match(line, @"Monkey (\d+)").Groups[1].Value);
                inspectionCounts.Add(0);
                inspectionCounts2.Add(0);
            }
            else if (line.StartsWith("  Start"))
            {
                var nums = Regex.Match(line, @"  Starting items: (.+)").Groups[1].Value;
                items.Add(new List<long>(nums.Split(", ").Select(long.Parse)));
                items2.Add(new List<long>(nums.Split(", ").Select(long.Parse)));
            }
            else if (line.StartsWith("  Oper"))
            {
                var ops = Regex.Match(line, @"  Operation: new = old (.) (\d+|old)");
                var isSquare = ops.Groups[2].Value == "old";
                operations.Add((isSquare ? "square" : ops.Groups[1].Value, isSquare ? 0 : long.Parse(ops.Groups[2].Value)));
            }
            else if (line.StartsWith("  Test"))
            {
                var test = Regex.Match(line, @"  Test: divisible by (\d+)").Groups[1].Value;
                curTest = (long.Parse(test), 0, 0);
            }
            else if (line.StartsWith("    If true"))
            {
                var monk = Regex.Match(line, @"    If true: throw to monkey (\d+)").Groups[1].Value;
                curTest.Item2 = int.Parse(monk);
            }
            else if (line.StartsWith("    If false"))
            {
                var monk = Regex.Match(line, @"    If false: throw to monkey (\d+)").Groups[1].Value;
                curTest.Item3 = int.Parse(monk);
                tests.Add(curTest);
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        for (int r = 0; r < 20; r++)
        for (int i = 0; i < items.Count; i++)
        {
            var (op, val) = operations[i];
            var (div, monkTrue, monkFalse) = tests[i];
            foreach (var item in items[i])
            {
                long newItem = 0;
                if (op == "+")
                {
                    newItem = (item + val) / 3;
                }
                else if (op == "*")
                {
                    newItem = (item * val) / 3;
                }
                else
                {
                    newItem = (item * item) / 3;
                }

                inspectionCounts[i]++;

                if (newItem % div == 0)
                {
                    items[monkTrue].Add(newItem);
                }
                else
                {
                    items[monkFalse].Add(newItem);
                }
            }
            items[i].Clear();
        }
        var top2 = inspectionCounts.OrderByDescending(x => x).Take(2).ToArray();

        return new((top2[0] * top2[1]).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var modulus = tests.Select(x => x.Item1).Aggregate((a, n) => a * n);

        for (int r = 0; r < 10000; r++)
        for (int i = 0; i < items2.Count; i++)
        {
            var (op, val) = operations[i];
            var (div, monkTrue, monkFalse) = tests[i];
            foreach (var item in items2[i])
            {
                long newItem = 0;
                if (op == "+")
                {
                    newItem = item + val;
                }
                else if (op == "*")
                {
                    newItem = item * val;
                }
                else
                {
                    newItem = item * item;
                }

                newItem %= modulus;

                inspectionCounts2[i]++;

                if (newItem % div == 0)
                {
                    items2[monkTrue].Add(newItem);
                }
                else
                {
                    items2[monkFalse].Add(newItem);
                }
            }
            items2[i].Clear();
        }
        var top2 = inspectionCounts2.OrderByDescending(x => x).Take(2).ToArray();

        return new((top2[0] * top2[1]).ToString());
    }
}
