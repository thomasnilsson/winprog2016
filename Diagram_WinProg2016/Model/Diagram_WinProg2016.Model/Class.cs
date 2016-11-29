using System.Collections.Generic;
using System.Windows.Media;

namespace Diagram_WinProg2016.Model
{
    public class Class : NotifyBase
    {

		private int x, y;
        private string className, fieldString, methodString;
		public int X { get { return x; } set { x = value; ; NotifyPropertyChanged(); } }
        public int Y { get { return y; } set { y = value; ; NotifyPropertyChanged(); } }
        public string ClassName { get { return className; } set { className = value; ; NotifyPropertyChanged(); } }
        public string MethodString { get { return methodString; } set { methodString = value; ; NotifyPropertyChanged(); } }
        public string FieldString { get { return fieldString; } set { fieldString = value; ; NotifyPropertyChanged(); } }

        private int width, height;
        public int Width { get { return width; } set { width = value; NotifyPropertyChanged("Width"); } }
        public int Height { get { return height; } set { height = value; NotifyPropertyChanged("Height"); } }


        public int CenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged("X"); } }
        public int CenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; NotifyPropertyChanged("Y"); } }

       
        private List<attOrMethodName> attNamesClass, methodNamesClass;
        public List<attOrMethodName> AttNamesClass { get { return attNamesClass; } set { attNamesClass = value; NotifyPropertyChanged("Attribut names"); } }
        public List<attOrMethodName> MethodNamesClass { get { return methodNamesClass; } set { methodNamesClass = value; NotifyPropertyChanged("MethodNames"); } }


        //Default Constructor
        public Class(int num)
        {
			X = 100;
			Y = 100;
            ClassName = "New Class";
            FieldString = "First Field";
            MethodString = "First Method";
            attNamesClass = new List<attOrMethodName>();
            methodNamesClass = new List<attOrMethodName>();
            attNamesClass.Add(new attOrMethodName("+ Attribute : Type"));
            attNamesClass.Add(new attOrMethodName("- Attribute : Type"));
            methodNamesClass.Add(new attOrMethodName("+ Method( ) : ReturnType"));
            methodNamesClass.Add(new attOrMethodName("- Method( ) : ReturnType"));
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                NotifyPropertyChanged("IsSelected");
                NotifyPropertyChanged("SelectedColor");
            }

        }
        public Brush SelectedColor { get { return IsSelected ? Brushes.Blue : Brushes.Black; } }

        public class attOrMethodName
        {
            public attOrMethodName(string _name)
            {
                name = _name;
                }
            private string name;
            public string Name { get { return name; } set { name = value; } }
            
        }
    }
}


