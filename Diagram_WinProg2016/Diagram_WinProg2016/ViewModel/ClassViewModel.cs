using System.Collections.Generic;
using Diagram_WinProg2016.Model;
using System.ComponentModel;

namespace Diagram_WinProg2016.ViewModel
{
	class ClassViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected Class classObj { get; }

		protected ClassViewModel(Class classObj) {
			this.classObj = classObj;
		}

		public List<string> Data { get; set; }

		public double Height
		{
			get { return classObj.Height; }
			set
			{
				classObj.Height = value; OnPropertyChanged("Height");
			}
		}

		public double Width
		{
			get { return classObj.Width; }
			set
			{
				classObj.Width = value; OnPropertyChanged("Width");
			}
		}

		public double X
		{
			get { return classObj.X; }
			set
			{
				classObj.X = value;
				OnPropertyChanged("X");
			}
		}
		public double Y
		{
			get { return classObj.Y; }
			set
			{
				classObj.Y = value;
				OnPropertyChanged("Y");
			}
		}
		public string className {
			get { return classObj.ClassName; }
		}
		public override string ToString() => classObj.ToString();

		protected void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(name));
			}
		}
	}

}
