using System;
using System.Collections.Generic;
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
using wpfsudokulib.ViewModels;

namespace wpfsudoku.UserControls
{
    /// <summary>
    /// Interaction logic for SudokuGrid.xaml
    /// </summary>
    public partial class SudokuGrid : UserControl
    {
        public SudokuGrid()
        {
            InitializeComponent();
        }
        
        private void DgBoard_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var row = e.Row.Item as SudokuRow;
            if (row[e.Column.DisplayIndex].ReadOnly)
            {
                e.Cancel = true;
            }
        }
    }
}
