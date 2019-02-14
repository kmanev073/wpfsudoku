using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfsudokulib.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class SudokuCell
    {        
        public byte? Data { get; set; }

        public bool ReadOnly { get; set; }

        public bool Highlight { get; set; }

        public SudokuCell()
        {

        }

        public SudokuCell(byte? data, bool readOnly, bool highlight)
        {
            Data = data;
            ReadOnly = readOnly;
            Highlight = highlight;
        }

        public SudokuCell(bool highlight)
        {
            Data = null;
            ReadOnly = true;
            Highlight = highlight;
        }
    }
}
