using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Model
{
	interface IConnector
	{
		Class StartClass { get; set; }
		Class EndClass { get; set; }
		int ID { get; set; }
	}
}
