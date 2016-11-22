namespace Diagram_WinProg2016.Model
{
    public class Class : NotifyBase
    {

		private int x, y;
        private string className, fieldString, methodString;
		private bool isSelected;
        public int X { get { return x; } set { x = value; ; NotifyPropertyChanged(); } }
        public int Y { get { return y; } set { y = value; ; NotifyPropertyChanged(); } }
        public string ClassName { get { return className; } set { className = value; ; NotifyPropertyChanged(); } }
        public string MethodString { get { return methodString; } set { methodString = value; ; NotifyPropertyChanged(); } }
        public string FieldString { get { return fieldString; } set { fieldString = value; ; NotifyPropertyChanged(); } }
		public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged(); } }
		

		//Default Constructor
		public Class()
        {
			X = 100;
			Y = 100;
            ClassName = "New Class";
            FieldString = "First Field";
            MethodString = "First Method";
			IsSelected = false;
        }

    }
}
