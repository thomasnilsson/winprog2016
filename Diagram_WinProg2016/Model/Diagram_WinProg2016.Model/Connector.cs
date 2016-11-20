using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Diagram_WinProg2016.Model
{
	public class Connector : INotifyPropertyChanged, IConnector
	{
		private int id;
		private Class startClass;
		public Class endClass;


		public int ID { get { return ID; } set { ID = value; NotifyPropertyChanged(); } }
		public Class StartClass { get { return startClass; } set { startClass = value; NotifyPropertyChanged(); } }
		public Class EndClass { get { return endClass; } set { endClass = value; NotifyPropertyChanged(); } }

		public Connector () {
			StartClass = null;
			EndClass = null;
		}

		public Connector (Class startClass, Class endClass) {
			StartClass = startClass;
			EndClass = endClass;
		}

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
