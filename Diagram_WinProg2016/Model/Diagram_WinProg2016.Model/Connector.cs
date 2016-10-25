using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Model
{
	class Connector : IConnector
	{
		private Class startClass;
		private Class endClass;
		public int ID { get; set; }
	}
}
