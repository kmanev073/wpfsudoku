using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using wpfsudokulib.ViewModels;

namespace wpfsudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModelsAccessor vma { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            vma = new ViewModelsAccessor();
            DataContext = vma;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vma.SudokuBoardViewModel.Rows[0][0].Data = 5;
            vma.SudokuBoardViewModel.Rows[0][0].ReadOnly = false;
        }
    }
}
