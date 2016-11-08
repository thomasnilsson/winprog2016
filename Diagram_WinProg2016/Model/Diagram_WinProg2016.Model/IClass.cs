using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Model
{
	interface IClass
	{
		int ID { get; set; }
		int X { get; set; }
		int Y { get; set; }
		int Height { get; set; }
		int Width { get; set; }
		int GridCenterX { get; set; }
		int GridCenterY { get; set; }
		string ClassName { get; set; }
		List<string> Fields { get; set; }
		List<string> Methods { get; set; }

		string ToString();
	}
}
