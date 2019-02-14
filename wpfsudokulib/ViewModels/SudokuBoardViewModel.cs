using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfsudokulib.Models;

namespace wpfsudokulib.ViewModels
{
    /// <summary>
    /// View model used to hold the sudoku board data
    /// </summary>
    public class SudokuBoardViewModel
    {
        /// <summary>
        /// Holds all the rows displayed in the data grid
        /// </summary>
        public ObservableCollection<SudokuRow> Rows { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SudokuBoardViewModel()
        {
            //Initialize the sudoku board
            InitializeBoard();
        }

        /// <summary>
        /// Used to start a new game or to load an old one
        /// </summary>
        /// <param name="gameState"></param>
        public SudokuBoardViewModel(GameState gameState)
        {
            //Initialize the sudoku board
            InitializeBoard();

            //Copy information from the provided game state
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    //Cell data
                    Rows[i][j].Data = gameState.SudokuBoard[i * 9 + j];

                    //Is the cell readonly
                    Rows[i][j].ReadOnly = gameState.ReadOnly[i * 9 + j];
                }
            }
        }

        /// <summary>
        /// Used for easier access to the row list
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public SudokuRow this[int i]
        {
            get { return Rows[i]; }
            set { Rows[i] = value; }
        }


        /// <summary>
        /// Allocates the row list
        /// </summary>
        private void InitializeBoard()
        {
            Rows = new ObservableCollection<SudokuRow>();
            for (int i = 0; i < 9; i++)
            {
                Rows.Add(new SudokuRow(i));
            }
        }
    }
}
