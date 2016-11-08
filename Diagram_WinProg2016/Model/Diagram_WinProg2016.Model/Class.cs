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

		public double X { get { return X; } set { this.X = value; ; NotifyPropertyChanged(); } }
		public double Y { get { return Y; } set { this.Y = value; ; NotifyPropertyChanged(); } }
		public double Height { get { return Height; } set { this.Height = value; ; NotifyPropertyChanged(); } }
		public double Width { get { return Width; } set { this.Width = value; ; NotifyPropertyChanged(); } }
		public double GridCenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged(() => X); } }
		public double GridCenterY { get { return Y + Height / 2; } set { Y = value - Width / 2; NotifyPropertyChanged(() => Y); } }

		public string ClassName { get { return ClassName; } set { this.ClassName = value; ; NotifyPropertyChanged(); } }
		public List<string> Fields { get { return Fields; } set { this.Fields = value; ; NotifyPropertyChanged(); } }
		public List<string> Methods { get { return Methods; } set { this.Methods = value; ; NotifyPropertyChanged(); } }

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
