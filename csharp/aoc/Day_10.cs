using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc;

public class Day_10 : BaseDay
{
    private readonly List<(string, int)> _input = new List<(string, int)>();

    public Day_10()
    {
        _input = File.ReadLines(InputFilePath).Select(l => l.Split(' ')).Select(l => (l[0], int.Parse(l.Length > 1 ? l[1] : "0"))).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var cycles = new List<long>();
        var x = 1;
        cycles.Add(x);
        foreach (var (inst, val) in _input)
        {
            cycles.Add(x);
            if (inst == "noop") continue;
            x += val;
            cycles.Add(x);
        }

        long result = 0;
        for (int i = 20; i < cycles.Count; i += 40)
        {
            result += cycles[i - 1] * i;
        }

        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var cycles = new List<long>();
        int width = 40, height = 6;
        var screen = new bool[width * height + 1];
        var screen_pos = 0;
        var x = 1;
        cycles.Add(x);
        foreach (var (inst, val) in _input)
        {
            cycles.Add(x);
            screen[screen_pos++] = x - 1 == screen_pos % width || x == screen_pos % width || x + 1 == screen_pos % width;
            if (inst == "noop") continue;
            x += val;
            cycles.Add(x);
            screen[screen_pos++] = x - 1 == screen_pos % width || x == screen_pos % width || x + 1 == screen_pos % width;
        }

        bool first = true;
        Console.Write('#');
        for (int i = 1; i < 240; i++)
        {
            if (!first && i % width == 0) Console.WriteLine();
            else if (first) first = false;
            Console.Write(screen[i - 1] ? "#" : ".");
        }
        Console.WriteLine();

        return new("");
    }
}
