using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Diagram_WinProg2016.ViewModel
{
	public class MainViewModel
	{
		public ObservableCollection<Model.Class> classes { get; set; }
		public ObservableCollection<Model.Connector> connectors { get; set; }

		public MainViewModel() {
			//Retrieve existing classes and connectors

			//Create ObservableCollection with classes and connectors
			classes = new ObservableCollection<Model.Class>(null);
			connectors = new ObservableCollection<Model.Connector>(null);
		}
	}
}
