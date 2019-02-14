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
    public class SudokuBoardViewModel
    {
        public ObservableCollection<SudokuRow> Rows { get; set; }

        public SudokuBoardViewModel()
        {
            InitializeBoard();
        }

        public SudokuBoardViewModel(GameState gameState)
        {
            InitializeBoard();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Rows[i][j].Data = gameState.SudokuBoard[i * 9 + j];
                    Rows[i][j].ReadOnly = gameState.ReadOnly[i * 9 + j];
                }
            }
        }

        public SudokuRow this[int i]
        {
            get { return Rows[i]; }
            set { Rows[i] = value; }
        }

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
