using System;
using System.IO;
using System.Linq;
using System.Text;

public class Solution
{
    private static void Main()
    {
        _reader = new StreamReader("input.txt");
        //_reader = Console.In; // crit
        Console.WriteLine(Solve());
    }
    // down, down right, right, up right, up, up left, left, down left
    private static int[] _rowDirs = { 1, 1, 0, -1, -1, -1, 0, 1 };
    private static int[] _colDirs = { 0, 1, 1, 1, 0, -1, -1, -1};
    private static int _rows;
    private static int _cols;
    private static int _nextRowToVisit;
    private static int _nextColToVisit;
    private static bool[,] _grid;
    private static bool[,] _visited;
    private static TextReader _reader;

    private struct Pair
    {
        public int Row;
        public int Col;

        public Pair(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }

    public static string Solve()
    {
        var sb = new StringBuilder();
        _rows = Int32.Parse(_reader.ReadLine());
        _cols = Int32.Parse(_reader.ReadLine());
        _visited = new bool[_rows, _cols];
        _grid = new bool[_rows, _cols];
        ReadInGrid();
        var answer = -1;
        Pair? curStart = FindNextUnvisited();
        while (curStart != null)
        {
            _visited[curStart.Value.Row, curStart.Value.Col] = true;
            int curRegionSize = FindConnectedNeighbors(curStart, 1);
            if (curRegionSize > answer)
            {
                answer = curRegionSize;
            }
            curStart = FindNextUnvisited();
        }
        sb.Append(answer);
        return sb.ToString();
    }

    private static bool IsInBounds(int row, int col)
    {
        return (col >= 0 && row >= 0 && col < _cols && row < _rows);
    }

    private static int FindConnectedNeighbors(Pair? cur, int size)
    {
        int maxSize = 0;
        for (int i = 0; i < _rowDirs.Length; ++i)
        {
            var newRow = cur.Value.Row + _rowDirs[i];
            var newCol = cur.Value.Col + _colDirs[i];
            if (IsInBounds(newRow, newCol) && 
                _grid[newRow, newCol] &&
                !_visited[newRow, newCol])
            {
                _visited[newRow, newCol] = true;
                maxSize += FindConnectedNeighbors(
                    new Pair(newRow, newCol), size);
            }
        }
        return maxSize + size;
    }

    private static Pair? FindNextUnvisited()
    {
        for (int row = _nextRowToVisit; row < _rows; ++row)
        {
            for (int col = _nextColToVisit; col < _cols; ++col)
            {
                if (_grid[row, col] && !_visited[row, col])
                {
                    _nextRowToVisit = row;
                    _nextRowToVisit = col;
                    return new Pair(row, col);
                }
            }
        }
        return null;
    }

    private static void ReadInGrid()
    {
        for (int row = 0; row < _rows; ++row)
        {
            var curRow = _reader.ReadLine()
                .Split(' ')
                .Select(x => x == "1")
                .ToArray();
            for (int col = 0; col < _cols; ++col)
            {
                if (curRow[col])
                {
                    _grid[row, col] = true;
                }
            }
        }
    }
}
