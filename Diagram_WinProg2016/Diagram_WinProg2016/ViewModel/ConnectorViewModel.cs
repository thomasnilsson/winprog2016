using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.ViewModel
{
	class ConnectorViewModel
	{
		public ClassViewModel startClass { get { return this.startClass; } set { this.startClass = value; } }
		public ClassViewModel endClass { get { return this.endClass; } set { this.endClass = value; } }
		
		public ConnectorViewModel(ClassViewModel startClass, ClassViewModel endClass) {
			this.startClass = startClass;
			this.endClass = endClass;
		}
	}
}
