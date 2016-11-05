using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Model
{
	public class Diagram
	{
		public List<Class> classes { get; set; }
		public List<Connector> connectors { get; set; }
	}
}
