using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using System.Windows;


namespace Diagram_WinProg2016.Model
{
    public class Edge : ViewModelBase
    {
        private Node endA;
        public Node EndA { get { return endA; } set { endA = value; } }

        private Node endB;
        public Node EndB { get { return endB; } set { endB = value; } }

        private string multA;
        public string MultA { get { return multA; } set { multA = value; } }

        private string multB;
        public string MultB { get { return multB; } set { multB = value; } }

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private EdgeType type;
        public EdgeType Type { get { return type; } set { type = value; } }
    }
}
