using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfsudokulib.ViewModels
{
    /// <summary>
    /// Represents a single sudoku cell
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class SudokuCell
    {
        #region PublicProperties
        /// <summary>
        /// The data of the cell, can be null if the cell is empty
        /// </summary>
        public byte? Data { get; set; }

        /// <summary>
        /// True if the cell is readonly
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// True if the cell's background is blue
        /// </summary>
        public bool Highlight { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor (required by LiteDB)
        /// </summary>
        public SudokuCell()
        {

        }

        /// <summary>
        /// Used in the SudokuRow's copy constructor
        /// </summary>
        /// <param name="data"></param>
        /// <param name="readOnly"></param>
        /// <param name="highlight"></param>
        public SudokuCell(byte? data, bool readOnly, bool highlight)
        {
            Data = data;
            ReadOnly = readOnly;
            Highlight = highlight;
        }

        /// <summary>
        /// Used at startup when setting up an empty board
        /// </summary>
        /// <param name="highlight"></param>
        public SudokuCell(bool highlight)
        {
            Data = null;
            ReadOnly = true;
            Highlight = highlight;
        }

        #endregion
    }
}
