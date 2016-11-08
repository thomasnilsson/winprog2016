using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Diagram_WinProg2016.Model
{
	public class Connector : INotifyPropertyChanged, IConnector
	{
		public int ID { get { return ID; } set { ID = value; NotifyPropertyChanged(); } }
		public Class startClass { get { return startClass; } set { startClass = value; NotifyPropertyChanged(); } }
		public Class endClass { get { return endClass; } set { endClass = value; NotifyPropertyChanged(); } }

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}

	
}
