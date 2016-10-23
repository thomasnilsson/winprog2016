using System;
using System.Collections.Generic;
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

namespace Diagram_WinProg2016
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void insertclass_Click(object sender, RoutedEventArgs e)
        {
            ClassUserControl newClass = new ClassUserControl();
            MainGrid.Children.Add(newClass); 
        }

		private void insertarrow_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
