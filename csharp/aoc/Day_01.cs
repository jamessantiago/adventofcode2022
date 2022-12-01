using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc;

public class Day_01 : BaseDay
{
    private readonly List<int> _input;

    public Day_01()
    {
        _input = File.ReadLines(InputFilePath).Select(d => int.Parse(d)).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        throw new NotImplementedException();
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }
}
