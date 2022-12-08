using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static aoc.Util;

namespace aoc;

public class Day_08 : BaseDay
{
    private readonly int[,] _input;

    public Day_08()
    {
        var lines = File.ReadLines(InputFilePath).ToList();
        _input = new int[lines[0].Length, lines.Count()];
        for (int y = 0; y < lines.Count(); y++)
            for (int x = 0; x < lines[0].Length; x++)
                _input[x, y] = int.Parse(lines[y][x].ToString());

    }

    public void visibleFromTop(Dictionary<(int, int), bool> visible)
    {
        for (int x = 0; x <= Util.XMax(_input); x++)
        {
            var maxHeight = 0;
            for (int y = 0; y <= Util.YMax(_input); y++)
            {
                if (_input[x, y] > maxHeight || y == 0)
                {
                    visible[(x, y)] = true;
                    maxHeight = _input[x, y];
                }
            }
        }
    }

    public void visibleFromleft(Dictionary<(int, int), bool> visible)
    {
        var maxHeight = 0;
        for (int y = 0; y <= Util.YMax(_input); y++)
        {
            maxHeight = 0;
            for (int x = 0; x <= Util.XMax(_input); x++)
            {
                if (_input[x, y] > maxHeight || x == 0)
                {
                    visible[(x, y)] = true;
                    maxHeight = _input[x, y];
                }
            }
        }
    }

    public void visibleFromBottom(Dictionary<(int, int), bool> visible)
    {
        var maxHeight = 0;
        for (int x = 0; x <= Util.XMax(_input); x++)
        {
            maxHeight = 0;
            for (int y = Util.YMax(_input); y >= 0; y--)
            {
                if (_input[x, y] > maxHeight || y == Util.YMax(_input))
                {
                    visible[(x, y)] = true;
                    maxHeight = _input[x, y];
                }
            }
        }
    }

    public void visibleFromRight(Dictionary<(int, int), bool> visible)
    {
        var maxHeight = 0;
        for (int y = 0; y <= Util.YMax(_input); y++)
        {
            maxHeight = 0;
            for (int x = Util.XMax(_input); x >= 0; x--)
            {
                if (_input[x, y] > maxHeight || x == Util.XMax(_input))
                {
                    visible[(x, y)] = true;
                    maxHeight = _input[x, y];
                }
            }
        }
    }

    Dictionary<(int, int), bool> visible = new Dictionary<(int, int), bool>();
    public override ValueTask<string> Solve_1()
    {
        visibleFromTop(visible);
        visibleFromBottom(visible);
        visibleFromleft(visible);
        visibleFromRight(visible);
        return new(visible.Count(x => x.Value).ToString());
    }

    public override ValueTask<string> Solve_2()
    {

        var total = new Dictionary<(int, int), long>();
        for (int x = 0; x <= Util.XMax(_input); x++)
            for (int y = 0; y <= Util.YMax(_input); y++)
            {
                if (x == 0 || y == 0 || x == Util.XMax(_input) || y == Util.YMax(_input)) continue;

                var lastHeight = 0;

                var curX = x - 1;
                var leftScore = 0;
                // view left
                if (_input[curX, y] > _input[x, y])
                {
                    leftScore++;
                }
                else
                {
                    while (true)
                    {
                        if (curX < 0 || _input[curX, y] < lastHeight) break;

                        if (_input[curX, y] >= lastHeight)
                        {
                            leftScore++;
                            lastHeight = _input[curX, y];
                        }

                        curX--;
                    }
                }

                curX = x + 1;
                lastHeight = 0;
                var rightScore = 0;
                // view right
                if (_input[curX, y] > _input[x, y])
                {
                    rightScore++;
                }
                else
                {
                    while (true)
                    {
                        if (curX > Util.XMax(_input) || _input[curX, y] < lastHeight) break;

                        if (_input[curX, y] >= lastHeight)
                        {
                            rightScore++;
                            lastHeight = _input[curX, y];
                        }

                        curX++;
                    }
                }

                var curY = y + 1;
                lastHeight = 0;
                var topScore = 0;
                // view top
                if (_input[x, curY] > _input[x, y])
                {
                    topScore++;
                }
                else
                {
                    while (true)
                    {
                        if (curY > Util.YMax(_input) || _input[x, curY] < lastHeight) break;

                        if (_input[x, curY] >= lastHeight)
                        {
                            topScore++;
                            lastHeight = _input[x, curY];
                        }

                        curY++;
                    }
                }

                curY = y - 1;
                lastHeight = 0;
                var bottomScore = 0;
                // view bottom
                if (_input[x, curY] > _input[x, y])
                {
                    bottomScore++;
                }
                else
                {
                    while (true)
                    {
                        if (curY < 0 || _input[x, curY] < lastHeight) break;

                        if (_input[x, curY] >= lastHeight)
                        {
                            bottomScore++;
                            lastHeight = _input[x, curY];
                        }

                        curY--;
                    }
                }

                total[(x, y)] = topScore * bottomScore * leftScore * rightScore;
            }
        return new(total.Max(x => x.Value).ToString());
    }
}
