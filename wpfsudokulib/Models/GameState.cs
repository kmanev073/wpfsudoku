using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfsudokulib.Enums;
using wpfsudokulib.Services;
using wpfsudokulib.ViewModels;

namespace wpfsudokulib.Models
{
    public class GameState
    {
        public Guid Id { get; set; }

        public GameDifficulties Difficulty { get; set; }

        public int ElapsedSeconds { get; set; }

        public DateTime SaveTimestamp { get; set; }

        public List<List<SudokuRow>> Undo { get; set; }

        public List<List<SudokuRow>> Redo { get; set; }

        public GameStatuses Status { get; set; }

        public byte?[] SudokuBoard { get; set; }
        
        public bool[] ReadOnly { get; set; }

        public GameState()
        {

        }

        public GameState(SudokuService sudokuService, GameDifficulties difficulty)
        {
            Id = Guid.NewGuid();
            Difficulty = difficulty;
            Undo = new List<List<SudokuRow>>();
            Redo = new List<List<SudokuRow>>();
            Status = GameStatuses.Playing;
            SudokuBoard = sudokuService.GenerateNew(difficulty);
            ReadOnly = new bool[81];

            for (int i = 0; i < 81; i++)
            {
                if (SudokuBoard[i].HasValue)
                {
                    ReadOnly[i] = true;
                }
            }
        }

        public GameState(GameStateViewModel gsViewModel, SudokuBoardViewModel sbViewModel)
        {
            Id = gsViewModel.GameId;
            Difficulty = gsViewModel.Difficulty;
            ElapsedSeconds = gsViewModel.ElapsedSeconds;
            SaveTimestamp = DateTime.Now;
            Undo = gsViewModel.Undo;
            Redo = gsViewModel.Redo;
            Status = gsViewModel.Status;
            SudokuBoard = new byte?[81];
            ReadOnly = new bool[81];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SudokuBoard[i * 9 + j] = sbViewModel.Rows[i][j].Data;
                    ReadOnly[i * 9 + j] = sbViewModel.Rows[i][j].ReadOnly;
                }
            }
        }
    }
}
