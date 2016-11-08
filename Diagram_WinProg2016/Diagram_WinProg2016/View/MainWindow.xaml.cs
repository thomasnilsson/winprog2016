using System.Windows;
using Diagram_WinProg2016.ViewModel;

namespace Diagram_WinProg2016
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        public MainWindow()
        {
			DataContext = new MainViewModel();
            InitializeComponent();
        }
    }
}
