using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc;

public class Day_04 : BaseDay
{
    private readonly List<(int[], int[])> _input = new List<(int[], int[])>();

    public Day_04()
    {
        _input = File.ReadLines(InputFilePath).Select(x => 
            (x.Split(',')[0].Split('-').Select(int.Parse).ToArray(), x.Split(',')[1].Split('-').Select(int.Parse).ToArray()))
            .ToList();
    }

    private bool fullyContainsEither((int[], int[]) pair)
    {
        var (a, b) = pair;
        if (a[0] <= b[0] && a[1] >= b[1]) return true;
        if (b[0] <= a[0] && b[1] >= a[1]) return true;
        return false;
    }

        private bool overlapsEither((int[], int[]) pair)
    {
        var (a, b) = pair;
        if (a[0] <= b[1] && a[1] >= b[1]) return true;
        if (b[0] <= a[1] && b[1] >= a[1]) return true;
        return false;
    }

    public override ValueTask<string> Solve_1()
    {
        var full = _input.Where(x => fullyContainsEither(x));
        return new (full.Count().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var overlaps = _input.Where(x => overlapsEither(x));
        return new (overlaps.Count().ToString());
    }
}
