namespace Diagram_WinProg2016.Model
{
    public class Class : NotifyBase
    {
        /*
		public int ID { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public string ClassName { get; set; }
		public string MethodString { get; set; }
		public string FieldString { get; set; }
		public bool IsSelected { get; set; }
		*/

		private int id, x, y;
        private string className, fieldString, methodString;
		private bool isSelected;
		public int ID { get { return id; } set { id = value; NotifyPropertyChanged(); } }
        public int X { get { return x; } set { x = value; ; NotifyPropertyChanged(); } }
        public int Y { get { return y; } set { y = value; ; NotifyPropertyChanged(); } }
        public string ClassName { get { return className; } set { className = value; ; NotifyPropertyChanged(); } }
        public string MethodString { get { return methodString; } set { methodString = value; ; NotifyPropertyChanged(); } }
        public string FieldString { get { return fieldString; } set { fieldString = value; ; NotifyPropertyChanged(); } }
		public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged(); } }
		

		//Default Constructor
		public Class(int ID)
        {
            this.ID = ID;
			X = 0;
			Y = 0;
            ClassName = "New Class";
            FieldString = "First Field";
            MethodString = "First Method";
			IsSelected = false;
        }

		public Class(int ID, int X, int Y, string ClassName, string MethodString, string FieldString) {
			this.ID = ID;
			this.X = X;
			this.Y = Y;
			this.ClassName = ClassName;
			this.MethodString = MethodString;
			this.FieldString = FieldString;
			IsSelected = false;
		}

		public Class(Class classItem) {
			ID = classItem.ID;
			X = classItem.X;
			Y = classItem.Y;
			ClassName = classItem.ClassName;
			MethodString = classItem.MethodString;
			FieldString = classItem.FieldString;
			IsSelected = false;

		}
        public override string ToString() => $"{GetType().Name} ({ID})";

    }
}
