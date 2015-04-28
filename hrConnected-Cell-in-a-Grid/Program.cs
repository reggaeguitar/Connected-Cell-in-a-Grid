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
        Console.WriteLine(Solve(Console.In));
    }

    public static string Solve(TextReader console)
    {
        var sb = new StringBuilder();
        var rows = Int32.Parse(Console.ReadLine());
        var cols = Int32.Parse(Console.ReadLine());
        var visited = new bool[cols, rows];
        var grid = new bool[cols, rows];
        ReadInGrid(grid, rows, cols, console);
        var answer = -1;
        KeyValuePair<int, int>? curStart = FindNextUnvisited(grid, rows, cols);
        while (curStart != null)
        {
            int curRegionSize = FindConnectedNeighbors(grid, curStart, rows, cols);
            if (curRegionSize > answer)
            {
                answer = curRegionSize;
            }
            curStart = FindNextUnvisited(grid, rows, cols);
        }
        sb.Append(answer);
        return sb.ToString();
    }

    private static int FindConnectedNeighbors(bool[,] grid, KeyValuePair<int, int>? start, int rows, int cols)
    {
                
    }

    private static KeyValuePair<int, int>? FindNextUnvisited(bool[,] grid, int rows, int cols)
    {
        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < cols; ++j)
            {
                if (grid[i, j])
                {
                    return new KeyValuePair<int, int>(i, j);
                }
            }
        }
        return null;
    }

    private static void ReadInGrid(bool[,] grid, int rows, int cols, TextReader console)
    {
        for (int i = 0; i < rows; ++i) // read in grid
        {
            var curRow = console.ReadLine()
                .Split(' ')
                .Select(x => x != "1")
                .ToArray();
            for (int j = 0; j < cols; ++j)
            {
                if (curRow[i])
                {
                    grid[i, j] = true;
                }
            }
        }

    }
}
