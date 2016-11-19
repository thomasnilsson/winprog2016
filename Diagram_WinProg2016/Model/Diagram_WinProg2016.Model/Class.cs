using System.Collections.Generic;

namespace Diagram_WinProg2016.Model
{
    public class Class : NotifyBase
    {
        private int id, x, y;
        //, height, width, gridCenterX, gridCenterY;
        private string className, fieldstring, methodstring;
        private List<string> fields, methods;

        public int ID { get { return id; } set { id = value; NotifyPropertyChanged(); } }
        public int X { get { return x; } set { x = value; ; NotifyPropertyChanged(); } }
        public int Y { get { return y; } set { y = value; ; NotifyPropertyChanged(); } }
        //public int Height { get { return height; } set { height = value; ; NotifyPropertyChanged(); } }
        //public int Width { get { return width; } set { width = value; ; NotifyPropertyChanged(); } }
        //public int GridCenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged(() => X); } }
        //public int GridCenterY { get { return Y + Height / 2; } set { Y = value - Width / 2; NotifyPropertyChanged(() => Y); } }

        public string ClassName { get { return className; } set { className = value; ; NotifyPropertyChanged(); } }
        public string MethodString { get { return methodstring; } set { methodstring = value; ; NotifyPropertyChanged(); } }
        public string FieldString { get { return fieldstring; } set { fieldstring = value; ; NotifyPropertyChanged(); } }
        public List<string> Fields { get { return fields; } set { fields = value; ; NotifyPropertyChanged(); } }
        public List<string> Methods { get { return methods; } set { methods = value; ; NotifyPropertyChanged(); } }





        //public int CenterX => Width / 2;
        //public int CenterY => Height / 2;

        public Class(int newNum)
        {
            //Width = Height = 200; //initial size
            ID = newNum;
            X = Y = 100; //start pos
            ClassName = "New Class"; //initial class name
            FieldString = "First Field";
            MethodString = "First Method";
            //Fields = new List<string>();
            //Methods = new List<string>();
            //Fields.Add("First Field");
            //Methods.Add("Second Method");
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
