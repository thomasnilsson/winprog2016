using System.Collections.Generic;
using Diagram_WinProg2016.Model;
using System.ComponentModel;

namespace Diagram_WinProg2016.ViewModel
{
    class ClassViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Class classObj;

        protected ClassViewModel(Class classObj)
        {
            this.classObj = classObj;
        }

        public int X { get { return classObj.X; } set { classObj.X = value; OnPropertyChanged("X"); } }
        public int Y { get { return classObj.Y; } set { classObj.Y = value; OnPropertyChanged("Y"); } }
        public string ClassName { get { return classObj.ClassName; } }
		public bool IsSelected { get { return classObj.IsSelected; } set { classObj.IsSelected = value; OnPropertyChanged("IsSelected"); } }

        public override string ToString() => classObj.ToString();

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}
