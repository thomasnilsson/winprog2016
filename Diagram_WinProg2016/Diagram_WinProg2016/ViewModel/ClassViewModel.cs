using System.Collections.Generic;
using Diagram_WinProg2016.Model;
using System.ComponentModel;

namespace Diagram_WinProg2016.ViewModel
{
	class ClassViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private Class classObj;

		protected ClassViewModel(Class classObj) {
			this.classObj = classObj;
		}
		

		public int Height { get { return classObj.Height; } set { classObj.Height = value; OnPropertyChanged("Height"); } }
		public int Width{ get { return classObj.Width; } set { classObj.Width = value; OnPropertyChanged("Width"); } }
		public int X { get { return classObj.X; } set { classObj.X = value; OnPropertyChanged("X"); } }
		public int Y { get { return classObj.Y; } set { classObj.Y = value; OnPropertyChanged("Y"); } }
		public string ClassName { get { return classObj.ClassName; } }
		public int CenterX { get { return X + Width / 2; } set { X = value - Width / 2; OnPropertyChanged("X"); } }
		public int CenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; OnPropertyChanged("Y"); } }

		public override string ToString() => classObj.ToString();

		protected void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}

}
