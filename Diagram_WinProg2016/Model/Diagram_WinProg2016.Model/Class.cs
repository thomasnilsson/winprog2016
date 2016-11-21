namespace Diagram_WinProg2016.Model
{
    public class Class : NotifyBase
    {
        private int id, x, y;
        private string className, fieldstring, methodstring;
		private bool isSelected;

		public int ID { get { return id; } set { id = value; NotifyPropertyChanged(); } }
        public int X { get { return x; } set { x = value; ; NotifyPropertyChanged(); } }
        public int Y { get { return y; } set { y = value; ; NotifyPropertyChanged(); } }
        public string ClassName { get { return className; } set { className = value; ; NotifyPropertyChanged(); } }
        public string MethodString { get { return methodstring; } set { methodstring = value; ; NotifyPropertyChanged(); } }
        public string FieldString { get { return fieldstring; } set { fieldstring = value; ; NotifyPropertyChanged(); } }
		public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged(); } }

		//Default Constructor
		public Class(int ID)
        {
            this.ID = ID;
            X = Y = 0; //start pos
            ClassName = "New Class"; //initial class name
            FieldString = "First Field";
            MethodString = "First Method";
        }

		public Class(int ID, int X, int Y, string ClassName, string MethodString, string FieldString) {
			this.ID = ID;
			this.X = X;
			this.Y = Y;
			this.ClassName = ClassName;
			this.MethodString = MethodString;
			this.FieldString = FieldString;
		}

		public Class(Class classItem) {
			ID = classItem.ID;
			X = classItem.X;
			Y = classItem.Y;
			ClassName = classItem.ClassName;
			MethodString = classItem.MethodString;
			FieldString = classItem.FieldString;

		}
        public override string ToString() => $"{GetType().Name} ({ID})";

    }
}
