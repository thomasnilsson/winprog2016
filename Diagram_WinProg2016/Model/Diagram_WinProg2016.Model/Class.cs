using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Diagram_WinProg2016.Model
{
	public class Class : NotifyBase, IClass
	{
		public int ID { get; set; }
		
		public double X { get; set; }
		public double Y { get; set; }
		public double Height { get; set; }
		public double Width { get; set; }
		public double GridCenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged(() => X); } }
		public double GridCenterY { get { return Y + Height / 2; } set { Y = value - Width / 2; NotifyPropertyChanged(() => Y); } }

		public string ClassName { get; set; }
		public List<string> Fields { get; set; }
		public List<string> Methods { get; set; }

		public double CenterX => Width / 2;
		public double CenterY => Height / 2;

        public Class()
        {
            ClassName = "Class Name";
            Fields[0] = "Fields";
            Methods[0] = "Methods";
        }
		public override string ToString() => $"{GetType().Name} ({ID})";
    }
}
