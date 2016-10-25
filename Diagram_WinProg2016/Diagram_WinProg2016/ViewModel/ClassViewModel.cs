using System.Collections.Generic;

using Diagram_WinProg2016.Model;

namespace Diagram_WinProg2016.ViewModel
{
	class ClassViewModel
	{
		protected Class ClassObj { get; }
		protected ClassViewModel(Class classObj) {
			classObj = ClassObj;
		}

		public List<string> Data { get; set; }
		public double Height
		{
			get { return ClassObj.Height; }
			set
			{
				ClassObj.Height = value; OnPropertyChanged();
			}
		}
		public int Number => ClassObj.Number;
		public ClassObj Type => ClassObj.Type;

		public double Width
		{
			get { return ClassObj.Width; }
			set
			{
				ClassObj.Width = value; OnPropertyChanged();
			}
		}

		public double X
		{
			get { return ClassObj.X; }
			set
			{
				ClassObj.X = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(XCenter));
			}
		}
		public double Y
		{
			get { return ClassObj.Y; }
			set
			{
				ClassObj.Y = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(YCenter));
			}
		}
		public override string ToString() => ClassObj.ToString();
	}
}
