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

		private double id;
		private double x;
		private double y;
		private double height;
		private double width;
		private double gridCenterX;
		private double gridCenterY;
		private string className;
		private List<string> fields;
		private List<string> methods;
		public int ID { get { return ID; } set { id = value; NotifyPropertyChanged("ID"); } }
        public double X { get { return x; } set { x = value; NotifyPropertyChanged("X"); } }
        public double Y { get { return y; } set { y = value; NotifyPropertyChanged("Y"); } }
        public double Width { get { return width; } set { width = value; NotifyPropertyChanged("Width"); } }
        public double Height { get { return height; } set { height = value; NotifyPropertyChanged("Height"); } }
        public double CenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged("X"); } }
        public double CenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; NotifyPropertyChanged("Y"); } }
        public string ClassName { get { return className; } set { className = value; NotifyPropertyChanged("ClassName"); } }


        public List<string> Fields { get { return fields; } set { fields = value; NotifyPropertyChanged("Fields"); } }
        public List<string> Methods { get { return methods; } set { methods = value; NotifyPropertyChanged("Fields"); } }

        //constructor
        public Class(int id)
        {

			ID = id;
            X = Y = 100; //start pos
            Width = Height = 200; //initial size
            className = "New Class"; //initial class name
            fields = new List<string>();
            methods = new List<string>();
            fields.Add("First Field");
            methods.Add("Second Method");
        }

		public override string ToString() => $"{GetType().Name} ({ID})";

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
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

    }

    
}
