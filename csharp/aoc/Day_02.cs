using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc;

public class Day_02 : BaseDay
{
    private readonly List<(int, int)> _input;

    public Day_02()
    {
        _input = File.ReadLines(InputFilePath).Select(x => ((int)x.Split()[0][0] - 64, (int)x.Split()[1][0] - 87)).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        int score = 0;
        foreach (var (p1, p2) in _input)
        {
            score += p2;
            if (p1 == 1 && p2 == 3 || p1 == 2 && p2 == 1 || p1 == 3 && p2 == 2)
            {
                score += 0;
            }
            else if (p1 == p2)
            {
                score += 3;
            }
            else
            {
                score += 6;
            }
        }
        return new (score.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int score = 0;
        foreach (var (p1, p2) in _input)
        {
            if (p2 == 1)
            {
                score += p1 == 1 ? 3 : p1 == 2 ? 1 : 2;
            }
            else if (p2 == 2)
            {
                score += 3;
                score += p1;
            }
            else
            {
                score += p1 == 1 ? 2 : p1 == 2 ? 3 : 1;
                score += 6;
            }
        }
        return new ValueTask<string>(score.ToString());
    }
}
