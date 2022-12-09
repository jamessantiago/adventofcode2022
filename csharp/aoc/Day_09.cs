using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc;

public class Day_09 : BaseDay
{
    private readonly List<(char, int)> _input = new List<(char, int)>();

    public Day_09()
    {
        _input = File.ReadLines(InputFilePath).Select(x => (x[0], int.Parse(x.Substring(2)))).ToList();
    }

    public bool withinRange((int, int) a, (int, int) b)
    {
        if (a == b) return true;
        var (ax, ay) = a;
        var (bx, by) = b;
        if (Math.Abs(ax - bx) > 1) return false;
        if (Math.Abs(ay - by) > 1) return false;
        return true;
    }


    public override ValueTask<string> Solve_1()
    {
        var visits = new HashSet<(int, int)>();
        var curHead = (0, 0);
        var curTail = (0, 0);
        visits.Add(curHead);
        foreach (var command in _input)
        {
            var (dir, steps) = command;
            for (int i = 0; i < steps; i++)
            {
                var (x, y) = curHead;
                curHead = dir switch
                {
                    'R' => (x + 1, y),
                    'L' => (x - 1, y),
                    'U' => (x, y - 1),
                    'D' => (x, y + 1),
                    _ => throw new NotImplementedException()
                };

                if (!withinRange(curTail, curHead))
                {
                    curTail = (x, y);
                    visits.Add(curTail);
                }
            }
        }
        return new(visits.Count.ToString());
    }

    public void printVisits(HashSet<(int, int)> visits)
    {
        for (int y = -20; y < 20; y++)
        {
            for (int x = -20; x < 20; x++)
            {
                if (x == 0 && y == 0) Console.Write("s");
                else Console.Write(visits.Contains((x, y)) ? "#" : ".");
            }
            Console.WriteLine();
        }
    }

    public void printKnots(List<(int, int)> knots)
    {
        for (int y = -20; y < 20; y++)
        {
            for (int x = -20; x < 20; x++)
            {
                if (x == 0 && y == 0) Console.Write("s");
                else
                {
                    var knotNum = knots.IndexOf((x, y));
                    Console.Write(knotNum >= 0 ? knotNum : ".");
                }
            }
            Console.WriteLine();
        }
    }

    public override ValueTask<string> Solve_2()
    {
        var visits = new HashSet<(int, int)>();
        var allVisits = new HashSet<(int, int)>();
        var tails = new List<(int, int)>(Enumerable.Range(0, 9).Select(x => (0, 0)));
        var curHead = (0, 0);
        visits.Add(curHead);
        foreach (var command in _input)
        {
            var (dir, steps) = command;
            for (int i = 0; i < steps; i++)
            {
                var (x, y) = curHead;
                curHead = dir switch
                {
                    'R' => (x + 1, y),
                    'L' => (x - 1, y),
                    'U' => (x, y - 1),
                    'D' => (x, y + 1),
                    _ => throw new NotImplementedException()
                };

                for (int ti = 0; ti < 9; ti++)
                {
                    var last = ti == 0 ? curHead : tails[ti - 1];
                    if (!withinRange(last, tails[ti]))
                    {
                        (x, y) = last;
                        var (nx, ny) = tails[ti];
                        nx += Math.Min(Math.Max(-1, x - nx), 1);
                        ny += Math.Min(Math.Max(-1, y - ny), 1);
                        tails[ti] = (nx, ny);
                    }
                }
                visits.Add(tails[8]);
            }
            //Console.SetCursorPosition(0, 0);
            //printKnots(tails);
        }

        //printVisits(visits);
        return new(visits.Count.ToString());
    }
}
