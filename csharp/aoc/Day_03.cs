using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc;

public class Day_03 : BaseDay
{
    private readonly List<(int[], int[])> _input = new List<(int[], int[])>();

    public Day_03()
    {
        foreach (var line in File.ReadLines(InputFilePath))
        {
            var compartments = line.Select(c => (int)c >= 97 ? (int)c - 96 : c - 38);
            var len = compartments.Count();
            _input.Add((compartments.Take(len / 2).ToArray(), compartments.Skip(len / 2).ToArray()));
        }
    }

    private int[] matches(int[] a, int[] b) => a.Where(x => b.Contains(x)).Distinct().ToArray();
    private int[] matches(int[] a, int[] b, int[] c) => a.Where(x => b.Contains(x) && c.Contains(x)).Distinct().ToArray();

    private int[] cat(int[] a, int[] b) {
        var x = new List<int>(a);
        x.AddRange(b);
        return x.ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        return new (_input.SelectMany(x => matches(x.Item1, x.Item2)).Sum().ToString());
    }

    public override ValueTask<string> Solve_2()
    {   
        long result = 0;
        for (int i = 0; i < _input.Count; i+= 3)
        {
            var bags = _input.Skip(i).Take(3).Select(x => cat(x.Item1, x.Item2)).ToList();
            result += matches(bags[0], bags[1], bags[2]).Sum();
        }
        return new (result.ToString());
    }
}
