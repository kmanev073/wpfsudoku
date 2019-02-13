using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace wpfsudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<A> asd { get; set; } = new ObservableCollection<A>()
        {
            new A(new bool[] { true, true, true, false, false, false, true, true, true }),
            new A(new bool[] { true, true, true, false, false, false, true, true, true }),
            new A(new bool[] { true, true, true, false, false, false, true, true, true }),
            new A(new bool[] { false, false, false, true, true, true, false, false, false }),
            new A(new bool[] { false, false, false, true, true, true, false, false, false }),
            new A(new bool[] { false, false, false, true, true, true, false, false, false }),
            new A(new bool[] { true, true, true, false, false, false, true, true, true }),
            new A(new bool[] { true, true, true, false, false, false, true, true, true }),
            new A(new bool[] { true, true, true, false, false, false, true, true, true }),
        };

        public MainWindow()
        {
            InitializeComponent();
            DataContext = asd;
        }

        private void DgBoard_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var saddd = e.Row.Item as A;
            if (saddd.CanEdit[e.Column.DisplayIndex] == false)
            {
                e.Cancel = true;
            }
        }
    }

    class A
    {
        public byte?[] a { get; set; } = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public bool[] CanEdit { get; set; } = { true, true, true, false, false, false, true, true, true };

        public bool[] Highlight { get; set; }

        public A(bool[] highlight)
        {
            Highlight = highlight;
        }
    }
}
