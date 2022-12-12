using AoCHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static aoc.Util;

namespace aoc;

public class Day_12 : BaseDay
{
    private readonly int[,] _input;
    private readonly List<neighbor> neighbors = new List<neighbor>();
    private readonly (int, int) start;
    private readonly (int, int) end;

    private class neighbor { 
        public neighbor(int X, int Y, int Steps) { x = X; y = Y; steps = Steps; }
        public int x; public int y; public int steps; 
        public List<neighbor> neighbors;
    }

    public Day_12()
    {
        var lines = File.ReadLines(InputFilePath).ToList();
        _input = new int[lines[0].Length, lines.Count()];
        for (int y = 0; y < Util.YLen(_input); y++)
        for (int x = 0; x < Util.XLen(_input); x++)
        {
                
                _input[x,y] = lines[y][x];
                neighbors.Add(new neighbor(x, y, 0));
                if (_input[x,y] == 'S') start = (x, y);
                
                if (_input[x,y] == 'E') {
                    _input[x,y] = (int)'z';
                    end = (x, y);
                }               
        }
        foreach (var nei in neighbors)
        {
            if ((nei.x, nei.y) == start) 
                nei.neighbors = neighbors.Where(n => getStartNeighbors(nei.x, nei.y).Contains((n.x, n.y))).ToList();
            else
                nei.neighbors = neighbors.Where(n => getNeighbors(nei.x, nei.y).Contains((n.x, n.y))).ToList();
        }
    }

    private bool isStart(int x, int y) => (x, y) == start;
    private bool isEnd(int x, int y) => (x, y) == end;

    private IEnumerable<(int, int)> getStartNeighbors(int x, int y)
    {      
        if (y > 0)
            yield return (x, y - 1);  
        if (x > 0)
            yield return (x - 1, y);
        
        if (x < Util.XMax(_input))
            yield return (x + 1, y);        
        if (y < Util.YMax(_input))
            yield return (x, y + 1);
    }

    private IEnumerable<(int, int)> getNeighbors(int x, int y)
    {                   
        if (x > 0 && _input[x - 1, y] - _input[x, y] <= 1) 
            yield return (x - 1, y);           
        if (y < Util.YMax(_input) && _input[x, y + 1] - _input[x, y] <= 1)
            yield return (x, y + 1);
        if (y > 0 && _input[x, y - 1] - _input[x, y] <= 1)
            yield return (x, y - 1); 
        if (x < Util.XMax(_input) && _input[x + 1, y] - _input[x, y] <= 1)
            yield return (x + 1, y);    
    }

    public override ValueTask<string> Solve_1()
    {
        var (sx, sy) = start;

        var min = int.MaxValue;
        var queue = new Queue<neighbor>();
        queue.Enqueue(neighbors.First(x => (x.x, x.y) == start));
        while (queue.Count > 0)
        {
            var item = queue.Dequeue();
            if ((item.x, item.y) == end)
            {
                min = Math.Min(min, item.steps);
                continue;
            }

            //IEnumerable<(int, int)> neighbors;
            //if ((x, y) == start) neighbors = getStartNeighbors(x, y);
            //else neighbors = getNeighbors(x, y);

            //foreach (var (nx, ny) in neighbors)
            //{
            //    if (!visits.ContainsKey((nx, ny)))
            //    {
            //        queue.Enqueue((nx, ny, steps + 1, visits));
            //    }

            //}

            foreach (var nei in item.neighbors)
            {
                if (nei.steps == 0)
                {
                    nei.steps = item.steps + 1;
                    queue.Enqueue(nei);
                }
            }
        }
        return new (min.ToString());
    }

    private void reset() => neighbors.ForEach(x => x.steps = 0);

    public override ValueTask<string> Solve_2()
    {
        var min = int.MaxValue;
        foreach (var sn in neighbors.Where(x => _input[x.x, x.y] == 'a'))
        {
            reset();
            var queue = new Queue<neighbor>();
            queue.Enqueue(sn);
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                if ((item.x, item.y) == end)
                {
                    min = Math.Min(min, item.steps);
                    break;
                }

                foreach (var nei in item.neighbors)
                {
                    if (nei.steps == 0)
                    {
                        nei.steps = item.steps + 1;
                        queue.Enqueue(nei);
                    }
                }
            }
        }
        
        return new (min.ToString());
    }
}
