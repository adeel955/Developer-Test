using System;
using System.Diagnostics;

class SudokuValidator
{
    static void Main()
    {
        int[][] Sudoku = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},
                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},
                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };

        bool isValid = ValidateSudoku(Sudoku);
        if (isValid)
        {
            Console.WriteLine($"Its a Good Sudoko");

        }
        else
        {
            Console.WriteLine("Its a Bad Sudoku");
        }
    }

    static bool ValidateSudoku(int[][] sudoku)
    {
        int n = sudoku.Length;

        // Check if it is a square matrix
        if (Math.Sqrt(n) % 1 != 0)
            return false;

        // Check rows and columns
        for (int i = 0; i < n; i++)
        {
            if (!IsValidSet(sudoku[i]) || !IsValidSet(GetColumn(sudoku, i)))
                return false;
        }

        // Check little squares
        int sqrtN = (int)Math.Sqrt(n);
        for (int i = 0; i < n; i += sqrtN)
        {
            for (int j = 0; j < n; j += sqrtN)
            {
                if (!IsValidSet(GetSquare(sudoku, i, j, sqrtN)))
                    return false;
            }
        }

        return true;
    }

    static bool IsValidSet(int[] set)
    {
        int n = set.Length;
        bool[] seen = new bool[n + 1];

        foreach (var num in set)
        {
            if (num < 1 || num > n || seen[num])
                return false;

            seen[num] = true;
        }

        return true;
    }

    static int[] GetColumn(int[][] matrix, int col)
    {
        int[] column = new int[matrix.Length];
        for (int i = 0; i < matrix.Length; i++)
        {
            column[i] = matrix[i][col];
        }
        return column;
    }

    static int[] GetSquare(int[][] matrix, int startRow, int startCol, int size)
    {
        int[] square = new int[size * size];
        int index = 0;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                square[index++] = matrix[startRow + i][startCol + j];
            }
        }

        return square;
    }
}
