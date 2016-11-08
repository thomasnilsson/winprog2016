using System.Collections.Generic;

namespace Diagram_WinProg2016.Model
{
	public class Class : NotifyBase, IClass
	{
		private int id, x, y, height, width, gridCenterX, gridCenterY;
		private string className;
		private List<string> fields, methods;

		public int ID { get { return id; } set { id = value; NotifyPropertyChanged(); } }
		public int X { get { return x; } set { x = value; ; NotifyPropertyChanged(); } }
		public int Y { get { return y; } set { y = value; ; NotifyPropertyChanged(); } }
		public int Height { get { return height; } set { height = value; ; NotifyPropertyChanged(); } }
		public int Width { get { return width; } set { width = value; ; NotifyPropertyChanged(); } }
		public int GridCenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged(() => X); } }
		public int GridCenterY { get { return Y + Height / 2; } set { Y = value - Width / 2; NotifyPropertyChanged(() => Y); } }

		public string ClassName { get { return className; } set {className = value; ; NotifyPropertyChanged(); } }
		public List<string> Fields { get { return fields; } set { fields = value; ; NotifyPropertyChanged(); } }
		public List<string> Methods { get { return methods; } set { methods = value; ; NotifyPropertyChanged(); } }

		public int CenterX => Width / 2;
		public int CenterY => Height / 2;

		public Class(int newNum)
		{

			ID = newNum;
			X = Y = 100; //start pos
			Width = Height = 200; //initial size
			ClassName = "New Class"; //initial class name
			Fields = new List<string>();
			Methods = new List<string>();
			Fields.Add("First Field");
			Methods.Add("Second Method");
		}

		private bool isSelected;
		public bool IsSelected
		{
			get { return isSelected; }
			set
			{
				isSelected = value;
				NotifyPropertyChanged("IsSelected");
			}

		}

		public override string ToString() => $"{GetType().Name} ({ID})";
    }
}

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Diagram_WinProg2016.Model
{
    public class Class : NotifyBase
    {

        private int x, y, number; // position and unique ID
        public int Number { get { return number; } private set { number = value; } } //ID property
        public int X { get { return x; } set { x = value; NotifyPropertyChanged("X"); } }
        public int Y { get { return y; } set { y = value; NotifyPropertyChanged("Y"); } }
        private int width, height;
        public int Width { get { return width; } set { width = value; NotifyPropertyChanged("Width"); } }
        public int Height { get { return height; } set { height = value; NotifyPropertyChanged("Height"); } }
        public int CenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged("X"); } }
        public int CenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; NotifyPropertyChanged("Y"); } }

        private string className;
        public string ClassName { get { return className; } set { className = value; NotifyPropertyChanged("ClassName"); } }

        private List<string> fields, methods;
        public List<string> Fields { get { return fields; } set { fields = value; NotifyPropertyChanged("Fields"); } }
        public List<string> Methods { get { return methods; } set { methods = value; NotifyPropertyChanged("Fields"); } }

        //constructor
        public Class(int newNum)
        {

            Number = newNum;
            X = Y = 100; //start pos
            Width = Height = 200; //initial size
            className = "New Class"; //initial class name
            fields = new List<string>();
            methods = new List<string>();
            fields.Add("First Field");
            methods.Add("Second Method");
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }

        }
    }    
}

*/
