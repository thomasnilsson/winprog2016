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
        private int X { get; set; }
        private int Y { get; set; }
        private int Height { get; set; }
        private int Width { get; set; }
        private int GridCenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged(() => X); } }
        private int GridCenterY { get { return Y + Height / 2; } set { Y = value - Width / 2; NotifyPropertyChanged(() => Y); } }
        private string ClassName { get; set; }
        private List<string> Fields { get; set; }
        private List<string> Methods { get; set; }
        public double CenterX => Width / 2;
        public double CenterY => Height / 2;

        public Class()
        {
            ClassName = "Class Name";
            Fields[0] = "Fields";
            Methods[0] = "Methods";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
