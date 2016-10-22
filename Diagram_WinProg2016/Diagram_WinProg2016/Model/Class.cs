using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Diagram_WinProg2016.Model
{
    class Class : INotifyPropertyChanged
    {
        private string ClassName;
        private List<string> Fields;
        private List<string> Methods; 
        public Class()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
