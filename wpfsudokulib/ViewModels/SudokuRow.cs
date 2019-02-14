using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfsudokulib.ViewModels
{
    public class SudokuRow
    {        
        public ObservableCollection<SudokuCell> Cells { get; set; }

        public SudokuRow()
        {

        }

        public SudokuRow(int rowIndex)
        {
            Cells = new ObservableCollection<SudokuCell>();
            for(int i = 0; i < 9; i++)
            {
                if ((rowIndex < 3 || rowIndex > 5) && (i < 3 || i > 5))
                {
                    Cells.Add(new SudokuCell(true));
                }
                else if ((rowIndex >= 3 && rowIndex <= 5) && (i >= 3 && i <= 5))
                {
                    Cells.Add(new SudokuCell(true));
                }
                else
                {
                    Cells.Add(new SudokuCell(false));
                }
            }
        }

        public SudokuRow(SudokuRow row)
        {
            Cells = new ObservableCollection<SudokuCell>();
            for (int i = 0; i < 9; i++)
            {
                Cells.Add(new SudokuCell(row[i].Data, row[i].ReadOnly, row[i].Highlight));
            }
        }

        public SudokuCell this[int i]
        {
            get { return Cells[i]; }
            set { Cells[i] = value; }
        }
    }
}
