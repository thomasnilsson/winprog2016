using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Diagram_WinProg2016.Model
{
	class Class : NotifyBase
	{
		public int ID { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
		public int GridCenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged(() => X); } }
		public int GridCenterY { get { return Y + Height / 2; } set { Y = value - Width / 2; NotifyPropertyChanged(() => Y); } }
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
		public event PropertyChangedEventHandler PropertyChanged;
    }
}
