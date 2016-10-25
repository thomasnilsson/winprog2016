using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Model
{
	public class Connector : IConnector
	{
		public Class startClass { get; set; }
		public Class endClass { get; set; }
		public int ID { get; set; }
	}
}
