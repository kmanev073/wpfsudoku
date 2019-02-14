using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfsudokulib.Enums;
using wpfsudokulib.ViewModels;

namespace wpfsudokulib.Models
{
    public class GameState
    {
        public Guid Id { get; set; }

        public GameDifficulties Difficulty { get; set; }

        public int ElapsedSeconds { get; set; }

        public DateTime SaveTimestamp { get; set; }

        public Stack<Move> Undo { get; set; }

        public Stack<Move> Redo { get; set; }

        public GameStatuses Status { get; set; }

        public byte?[] SudokuBoard { get; set; }

        public GameState()
        {

        }

        public GameState(GameDifficulties difficulty)
        {
            Id = Guid.NewGuid();
            Difficulty = difficulty;
            Undo = new Stack<Move>();
            Redo = new Stack<Move>();
            Status = GameStatuses.Playing;
            
            SudokuBoard = new byte?[81];
            //generate board
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

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SudokuBoard[i * 9 + j] = sbViewModel.Rows[i][j].Data;
                }
            }
        }
    }
}
