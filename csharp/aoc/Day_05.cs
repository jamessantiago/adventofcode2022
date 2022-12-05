using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc;

public class Day_05 : BaseDay
{
    private readonly List<Stack<char>> _input;
    private readonly List<int[]> commands = new List<int[]>();
    private readonly List<Stack<char>> _input2;

    public Day_05()
    {
        bool isCommands = false;
        var lines = File.ReadLines(InputFilePath).ToList();
        var stackLen = (lines[0].Length / 4) + 1;
        _input = new List<Stack<char>>();
        for (int i = 0; i < stackLen; i++) _input.Add(new Stack<char>());
        foreach (var line in lines)
        {
            if (line == "")
            {
                isCommands = true;
                continue;
            }

            if (!isCommands)
            {
                for (int i = 0; i < stackLen; i++)
                {
                    var box = line[(i * 4) + 1];
                    if (box != ' ') _input[i].Push(box);
                }                
            }
            else
            {
                var matches = Regex.Match(line, @"move (\d+) from (\d+) to (\d+)");
                commands.Add(matches.Groups.Values.Skip(1).Select(x => int.Parse(x.Value)).ToArray());
            }
        }

        for (int i = 0; i < stackLen; i++)
        {
            _input[i].Pop();
            _input[i] = new Stack<char>(_input[i]);
        }

        _input2 = new List<Stack<char>>();
        _input.ToList().ForEach(x => _input2.Add(new Stack<char>(x.Reverse())));
    }

    public override ValueTask<string> Solve_1()
    {
        foreach (var command in commands)
        {
            for (int i = 0; i < command[0]; i++)
            {
                var take = _input[command[1] - 1].Pop();
                _input[command[2] - 1].Push(take);
            }
        }
        var sb = new StringBuilder();
        foreach (var stack in _input)
        {
            sb.Append(stack.Pop());
        }
        return new(sb.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        foreach (var command in commands)
        {
            var takes = new List<char>();
            for (int i = 0; i < command[0]; i++)
            {
                takes.Add(_input2[command[1] - 1].Pop());
            }
            takes.Reverse();
            takes.ForEach(x => _input2[command[2] - 1].Push(x));
        }
        var sb = new StringBuilder();
        foreach (var stack in _input2)
        {
            sb.Append(stack.Pop());
        }
        return new(sb.ToString());
    }
}
