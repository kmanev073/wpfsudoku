using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpfsudokulib;
using wpfsudokulib.Enums;
using wpfsudokulib.ViewModels;

namespace wpfsudoku.UserControls
{
    /// <summary>
    /// Interaction logic for SudokuGrid.xaml
    /// </summary>
    public partial class SudokuGrid : UserControl
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SudokuGrid()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Prevents the editing of readonly cells.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgBoard_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var row = e.Row.Item as SudokuRow;
            if (row[e.Column.DisplayIndex].ReadOnly)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Called when a cell is edited. Invokes the EditCellCommand in the ViewModel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgBoard_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var mvm = DataContext as MainViewModel;
            mvm.EditCellCommand.Execute(null);
        }

        /// <summary>
        /// Called just before the ViewModel update. Used to store the old grid state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgBoard_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var mvm = DataContext as MainViewModel;
            var rows = new List<SudokuRow>();
            for (int i = 0; i < 9; i++)
            {
                rows.Add(new SudokuRow(mvm.SudokuBoardViewModel.Rows[i]));
            }
            mvm.GameStateViewModel.Undo.Add(rows);
        }
    }
}
