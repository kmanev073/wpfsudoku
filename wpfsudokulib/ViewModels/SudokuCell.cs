using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfsudokulib.ViewModels
{
    public class SudokuCell : INotifyPropertyChanged
    {
        private byte? data;
        
        public byte? Data
        {
            get { return data; }
            set { data = value; }
        }

        public bool ReadOnly { get; set; }

        public bool Highlight { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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
