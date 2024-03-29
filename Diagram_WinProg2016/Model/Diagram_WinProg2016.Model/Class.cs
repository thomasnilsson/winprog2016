﻿namespace Diagram_WinProg2016.Model
{
    public class Class : NotifyBase
    {

        private int x, y;
        private string className, fieldString, methodString;
        private int width, height;
        private bool isSelected;
        public int X { get { return x; } set { x = value; ; NotifyPropertyChanged(); } }
        public int Y { get { return y; } set { y = value; ; NotifyPropertyChanged(); } }
        public int Width { get { return width; } set { width = value; NotifyPropertyChanged("Width"); } }
        public int Height { get { return height; } set { height = value; NotifyPropertyChanged("Height"); } }
        public int CenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged("X"); } }
        public int CenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; NotifyPropertyChanged("Y"); } }
        public string ClassName { get { return className; } set { className = value; ; NotifyPropertyChanged(); } }
        public string MethodString { get { return methodString; } set { methodString = value; ; NotifyPropertyChanged(); } }
        public string FieldString { get { return fieldString; } set { fieldString = value; ; NotifyPropertyChanged(); } }
        public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged(); } }


        //Default Constructor
        public Class()
        {
            Width = 250;
            Height = 100; //this height is arbitrary, it just needs to be large enough to display the whole container.
            X = 100;
            Y = 100;
            ClassName = "New Class";
            FieldString = "First Field";
            MethodString = "First Method";
            IsSelected = false;
        }

    }
}
