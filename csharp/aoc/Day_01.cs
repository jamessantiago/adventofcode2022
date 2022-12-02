using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc;

public class Day_01 : BaseDay
{
    private readonly List<int> _input = new List<int>();

    public Day_01()
    {
        int i = 0;
        _input.Add(0);
        foreach (var line in File.ReadLines(InputFilePath))
        {
            if (line == "") 
            {
                i++;
                _input.Add(0);
            }
            else
            {
                _input[i] += int.Parse(line);
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        return new (_input.Max().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new (_input.Order().TakeLast(3).Sum().ToString());
    }
}
