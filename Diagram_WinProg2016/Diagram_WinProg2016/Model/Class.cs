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

        private List<AttributeOrMethod> fields, methods;
        public List<AttributeOrMethod> Fields { get { return fields; } set { fields = value; NotifyPropertyChanged("Fields"); } }
        public List<AttributeOrMethod> Methods { get { return methods; } set { methods = value; NotifyPropertyChanged("Fields"); } }

        //constructor
        public Class(int newNum)
        {

            Number = newNum;
            X = Y = 100; //start pos
            Width = Height = 200; //initial size
            className = "Is this class Name getting through?"; //initial class name
            fields = new List<AttributeOrMethod>();
            methods = new List<AttributeOrMethod>();
            fields.Add(new AttributeOrMethod("First Field"));
            methods.Add(new AttributeOrMethod("First Method"));
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

    public class AttributeOrMethod
    {
        public AttributeOrMethod(string _name)
        {
            name = _name;
            //FontStyle fStyle = FontStyles.Normal;
            //FontWeight fWeight = FontWeights.Normal;
        }
        private string name;
        public string Name { get { return name; } set { name = value; } }
        //private FontStyle fStyle;
        //private FontWeight fWeight;
        //public FontStyle FStyle { get { return fStyle; } set { fStyle = value; } }
        //public FontWeight FWeight { get { return fWeight; } set { fWeight = value; } }
    }


}
