using System.Collections.Generic;
using Diagram_WinProg2016.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using System.Linq;


namespace Diagram_WinProg2016.ViewModel
{
	class ConnectorViewModel
	{
		private Connector connectorObj;

		protected ConnectorViewModel(Connector connectorObj)
		{
			this.connectorObj = connectorObj;
		}

	}
}
