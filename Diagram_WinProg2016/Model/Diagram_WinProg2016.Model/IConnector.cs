using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Model
{
	interface IConnector
	{
		Class startClass { get; set; }
		Class endClass { get; set; }
		int ID { get; }
	}
}
