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
    /// <summary>
    /// The GameState model that is used to store information in LiteDB
    /// </summary>
    public class GameState
    {
        #region PublicProperties

        /// <summary>
        /// The Id of the game
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The difficulty of the game
        /// </summary>
        public GameDifficulties Difficulty { get; set; }

        /// <summary>
        /// The number of seconds since the game started
        /// </summary>
        public int ElapsedSeconds { get; set; }

        /// <summary>
        /// The DateTime when the game was saved
        /// </summary>
        public DateTime SaveTimestamp { get; set; }

        /// <summary>
        /// List of rows used for the undo functionality
        /// </summary>
        public List<List<SudokuRow>> Undo { get; set; }

        /// <summary>
        /// List of rows used for the redo functionality
        /// </summary>
        public List<List<SudokuRow>> Redo { get; set; }

        /// <summary>
        /// The status of the game
        /// </summary>
        public GameStatuses Status { get; set; }

        /// <summary>
        /// Byte array used to store the sudoku board in LiteDB
        /// </summary>
        public byte?[] SudokuBoard { get; set; }
        
        /// <summary>
        /// Boolean array showing if a cell should be readonly
        /// </summary>
        public bool[] ReadOnly { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor (Required by LiteDB)
        /// </summary>
        public GameState()
        {

        }

        /// <summary>
        /// Constructor used when starting a new game
        /// </summary>
        /// <param name="sudokuService"></param>
        /// <param name="difficulty"></param>
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

        /// <summary>
        /// Constructor used when loading a game
        /// </summary>
        /// <param name="gsViewModel"></param>
        /// <param name="sbViewModel"></param>
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

        #endregion
    }
}
