using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc;

public class Day_06 : BaseDay
{
    private readonly string _input;

    public Day_06()
    {
        _input = File.ReadLines(InputFilePath).First();
    }

    public override ValueTask<string> Solve_1()
    {
        int found = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            var next = _input.Skip(i).Take(4);
            if (next.Distinct().Count() == 4)
            {
                found = i + 4;
                break;
            }
        }
        return new (found.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int found = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            var next = _input.Skip(i).Take(14);
            if (next.Distinct().Count() == 14)
            {
                found = i + 14;
                break;
            }
        }
        return new(found.ToString());
    }
}
