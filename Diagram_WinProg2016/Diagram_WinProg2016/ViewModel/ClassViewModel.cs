using Diagram_WinProg2016.Model;
using System.ComponentModel;

namespace Diagram_WinProg2016.ViewModel
{
	class ClassViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private Class classObj;

		public Class ClassObj { get { return classObj; } set { classObj = value; } }
		public double Height { get { return classObj.Height; } set { classObj.Height = value; OnPropertyChanged("Height"); } }
		public double Width { get { return classObj.Width; } set { classObj.Width = value; OnPropertyChanged("Width"); } }
		public double X { get { return classObj.X; } set { classObj.X = value; OnPropertyChanged("X"); } }
		public double Y { get { return classObj.Y; } set { classObj.Y = value; OnPropertyChanged("Y"); } }
		public string ClassName { get { return classObj.ClassName; } set { classObj.ClassName = value; OnPropertyChanged("ClassName"); } }

		public ClassViewModel() {

			classObj = new Class(0);

		 }

		public override string ToString() => classObj.ToString();

		protected void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}

}
