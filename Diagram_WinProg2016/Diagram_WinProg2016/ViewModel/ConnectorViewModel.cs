using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diagram_WinProg2016.Model;

namespace Diagram_WinProg2016.ViewModel
{
	class ConnectorViewModel
	{
		private Class startClass;
		private Class endClass;

		public ClassViewModel StartClass { get; set; }

		public ClassViewModel EndClass { get; set; }
	}
}
