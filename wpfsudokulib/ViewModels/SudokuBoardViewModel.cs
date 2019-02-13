using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfsudokulib.ViewModels
{
    public class SudokuBoardViewModel
    {
        public ObservableCollection<SudokuRow> Rows { get; set; }

        public SudokuBoardViewModel()
        {
            Rows = new ObservableCollection<SudokuRow>();
            for (int i = 0; i < 9; i++)
            {
                Rows.Add(new SudokuRow(i));
            }
        }

        public SudokuRow this[int i]
        {
            get { return Rows[i]; }
            set { Rows[i] = value; }
        }
    }
}
