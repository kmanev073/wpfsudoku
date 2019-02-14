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
        /// <summary>
        /// List of cells in the row
        /// </summary>
        public ObservableCollection<SudokuCell> Cells { get; set; }

        /// <summary>
        /// Default construcotr (required by LiteDB)
        /// </summary>
        public SudokuRow()
        {

        }

        /// <summary>
        /// Generates a properly highlight row
        /// </summary>
        /// <param name="rowIndex"></param>
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

        /// <summary>
        /// Copy construcotor
        /// </summary>
        /// <param name="row"></param>
        public SudokuRow(SudokuRow row)
        {
            Cells = new ObservableCollection<SudokuCell>();
            for (int i = 0; i < 9; i++)
            {
                Cells.Add(new SudokuCell(row[i].Data, row[i].ReadOnly, row[i].Highlight));
            }
        }

        /// <summary>
        /// Used for easier access to the row's cells
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public SudokuCell this[int i]
        {
            get { return Cells[i]; }
            set { Cells[i] = value; }
        }
    }
}
