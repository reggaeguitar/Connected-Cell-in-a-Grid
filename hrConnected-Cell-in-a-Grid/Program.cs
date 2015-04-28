using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

public class Solution
{
    private static void Main()
    {
        _console = Console.In;
        Console.WriteLine(Solve(_console));
    }

    private static int _rows;
    private static int _cols;
    private static bool[,] _grid;
    private static bool[,] _visited;
    private static TextReader _console;

    public static string Solve(TextReader console)
    {
        var sb = new StringBuilder();
        _rows = Int32.Parse(Console.ReadLine());
        _cols = Int32.Parse(Console.ReadLine());
        var visited = new bool[_cols, _rows];
        _grid = new bool[_cols, _rows];
        ReadInGrid();
        var answer = -1;
        KeyValuePair<int, int>? curStart = FindNextUnvisited();
        while (curStart != null)
        {
            int curRegionSize = FindConnectedNeighbors(curStart, 0);
            if (curRegionSize > answer)
            {
                answer = curRegionSize;
            }
            curStart = FindNextUnvisited();
        }
        sb.Append(answer);
        return sb.ToString();
    }

    private static int FindConnectedNeighbors(KeyValuePair<int, int>? cur, int size)
    {
        // todo 
    }

    private static KeyValuePair<int, int>? FindNextUnvisited()
    {
        for (int i = 0; i < _rows; ++i)
        {
            for (int j = 0; j < _cols; ++j)
            {
                if (_grid[i, j])
                {
                    return new KeyValuePair<int, int>(i, j);
                }
            }
        }
        return null;
    }

    private static void ReadInGrid()
    {
        for (int i = 0; i < _rows; ++i)
        {
            var curRow = _console.ReadLine()
                .Split(' ')
                .Select(x => x != "1")
                .ToArray();
            for (int j = 0; j < _cols; ++j)
            {
                if (curRow[i])
                {
                    _grid[i, j] = true;
                }
            }
        }

    }
}
