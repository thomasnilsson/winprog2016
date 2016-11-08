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
		double X { get; set; }
		double Y { get; set; }
		double Height { get; set; }
		double Width { get; set; }
		double GridCenterX { get; set; }
		double GridCenterY { get; set; }
		string ClassName { get; set; }
		List<string> Fields { get; set; }
		List<string> Methods { get; set; }

		string ToString();
	}
}
