using System.Collections.Generic;
using Diagram_WinProg2016.Model;

namespace Diagram_WinProg2016.ViewModel
{
	class ClassViewModel
	{
		protected Class classObj { get; }

		protected ClassViewModel(Class classObj) {
			classObj = classObj;
		}

		public List<string> Data { get; set; }

		public double Height
		{
			get { return classObj.Height; }
			set
			{
				classObj.Height = value; OnPropertyChanged();
			}
		}

		public double Width
		{
			get { return classObj.Width; }
			set
			{
				classObj.Width = value; OnPropertyChanged();
			}
		}

		public double X
		{
			get { return classObj.X; }
			set
			{
				classObj.X = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(XCenter));
			}
		}
		public double Y
		{
			get { return classObj.Y; }
			set
			{
				classObj.Y = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(YCenter));
			}
		}
		public string className {
			get { return classObj.ClassName; }
		}
		public override string ToString() => classObj.ToString();
	}
}
