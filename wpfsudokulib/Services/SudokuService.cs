using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfsudokulib.Enums;

namespace wpfsudokulib.Services
{
    public class SudokuService
    {
        public byte?[] GenerateNew(GameDifficulties difficulty)
        {
            byte?[] SudokuBoard;
            do
            {
                SudokuBoard = new byte?[81];
                var pencilMarks = new List<byte?>[81];

                for (int i = 0; i < 81; i++)
                {
                    pencilMarks[i] = new List<byte?>();
                    for (byte j = 1; j <= 9; j++)
                    {
                        pencilMarks[i].Add(j);
                    }

                    //Smooth list shuffle
                    pencilMarks[i] = pencilMarks[i].OrderBy(_ => Guid.NewGuid()).ToList();
                }

                for (int i = 0; i < 81; i++)
                {
                    var row = i / 9;
                    var column = i % 9;
                    var blockRow = row / 3;
                    var blockColumn = column / 3;
                    var blockLocalRow = row % 3;
                    var blockLocalColumn = column % 3;

                    for (int index = 0; index < pencilMarks[i].Count; index++)
                    {
                        var value = pencilMarks[i][index];

                        bool flag = false;

                        //Check if we can place that number in the row
                        for (int k = 0; k < 9; k++)
                        {
                            if (column == k)
                            {
                                continue;
                            }
                            if (SudokuBoard[row * 9 + k] == value || (pencilMarks[row * 9 + k].Any(_ => _ == value) && pencilMarks[row * 9 + k].Count == 1))
                            {
                                flag = true;
                            }
                        }
                        if (flag)
                        {
                            continue;
                        }

                        //Check if we can place that number in the column
                        for (int k = 0; k < 9; k++)
                        {
                            if (row == k)
                            {
                                continue;
                            }
                            if (SudokuBoard[k * 9 + column] == value || (pencilMarks[k * 9 + column].Any(_ => _ == value) && pencilMarks[k * 9 + column].Count == 1))
                            {
                                flag = true;
                            }
                        }
                        if (flag)
                        {
                            continue;
                        }

                        //Check if we can place that number in the block
                        for (int k = 0; k < 3; k++)
                        {
                            for (int l = 0; l < 3; l++)
                            {
                                if (blockLocalRow == k && blockLocalColumn == l)
                                {
                                    continue;
                                }
                                if (SudokuBoard[(blockRow * 3) * 9 + blockColumn * 3 + k * 9 + l] == value || (pencilMarks[(blockRow * 3) * 9 + blockColumn * 3 + k * 9 + l].Any(_ => _ == value) && pencilMarks[(blockRow * 3) * 9 + blockColumn * 3 + k * 9 + l].Count == 1))
                                {
                                    flag = true;
                                }
                            }
                        }
                        if (flag)
                        {
                            continue;
                        }

                        SudokuBoard[i] = value;
                        pencilMarks[i].Clear();
                        break;
                    }

                    //Remove pencil marks from row
                    for (int k = 0; k < 9; k++)
                    {
                        if (column == k)
                        {
                            continue;
                        }
                        pencilMarks[row * 9 + k].RemoveAll(_ => _ == SudokuBoard[i]);
                    }

                    //Remove pencil marks from column
                    for (int k = 0; k < 9; k++)
                    {
                        if (row == k)
                        {
                            continue;
                        }
                        pencilMarks[k * 9 + column].RemoveAll(_ => _ == SudokuBoard[i]);
                    }

                    //Remove pencil marks from block
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            if (blockLocalRow == k && blockLocalColumn == l)
                            {
                                continue;
                            }
                            pencilMarks[(blockRow * 3) * 9 + blockColumn * 3 + k * 9 + l].RemoveAll(_ => _ == SudokuBoard[i]);
                        }
                    }
                }
            }
            while (CheckBoard(SudokuBoard) == false);

            Random random = new Random();
            var randomNumber = new byte[1];

            switch (difficulty)
            {
                case GameDifficulties.Hard:
                    for (int i = 0; i < 81; i += 9)
                    {
                        random.NextBytes(randomNumber);
                        randomNumber[0] %= 9;
                        for (int j = 0; j < 9; j++)
                        {
                            if (SudokuBoard[i + j] == randomNumber[0])
                            {
                                SudokuBoard[i + j] = null;
                            }
                        }

                        random.NextBytes(randomNumber);
                        randomNumber[0] %= 9;
                        for (int j = 0; j < 9; j++)
                        {
                            if (SudokuBoard[i + j] == randomNumber[0])
                            {
                                SudokuBoard[i + j] = null;
                            }
                        }
                    }
                    goto case GameDifficulties.Medium;
                case GameDifficulties.Medium:
                    for (int i = 0; i < 81; i += 9)
                    {
                        random.NextBytes(randomNumber);
                        randomNumber[0] %= 9;
                        for (int j = 0; j < 9; j++)
                        {
                            if (SudokuBoard[i + j] == randomNumber[0])
                            {
                                SudokuBoard[i + j] = null;
                            }
                        }

                        random.NextBytes(randomNumber);
                        randomNumber[0] %= 9;
                        for (int j = 0; j < 9; j++)
                        {
                            if (SudokuBoard[i + j] == randomNumber[0])
                            {
                                SudokuBoard[i + j] = null;
                            }
                        }
                    }
                    goto case GameDifficulties.Easy;
                case GameDifficulties.Easy:   
                    for (int i = 0; i < 81; i += 9)
                    {
                        random.NextBytes(randomNumber);
                        randomNumber[0] %= 9;
                        for (int j = 0; j < 9; j++)
                        {
                            if (SudokuBoard[i + j] == randomNumber[0])
                            {
                                SudokuBoard[i + j] = null;
                            }
                        }

                        randomNumber = new byte[1];
                        random.NextBytes(randomNumber);
                        randomNumber[0] %= 9;
                        for (int j = 0; j < 9; j++)
                        {
                            if (SudokuBoard[i + j] == randomNumber[0])
                            {
                                SudokuBoard[i + j] = null;
                            }
                        }
                    }
                    break;
            }

            return SudokuBoard;
        }

        public bool CheckBoard(byte?[] SudokuBoard)
        {
            for (int i = 0; i < 81; i++)
            {
                var row = i / 9;
                var column = i % 9;
                var blockRow = row / 3;
                var blockColumn = column / 3;
                var blockLocalRow = row % 3;
                var blockLocalColumn = column % 3;

                if (SudokuBoard[i] == null)
                {
                    return false;
                }

                //Validate row
                for (int k = 0; k < 9; k++)
                {
                    if (column == k)
                    {
                        continue;
                    }
                    if (SudokuBoard[row * 9 + k] == SudokuBoard[row * 9 + column])
                    {
                        return false;
                    }
                }

                //Validate column
                for (int k = 0; k < 9; k++)
                {
                    if (row == k)
                    {
                        continue;
                    }
                    if (SudokuBoard[k * 9 + column] == SudokuBoard[row * 9 + column])
                    {
                        return false;
                    }
                }

                //Validate block
                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        if (blockLocalRow == k && blockLocalColumn == l)
                        {
                            continue;
                        }
                        if (SudokuBoard[(blockRow * 3) * 9 + blockColumn * 3 + k * 9 + l] == SudokuBoard[(blockRow * 3) * 9 + blockColumn * 3 + blockLocalRow * 9 + blockLocalColumn])
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}