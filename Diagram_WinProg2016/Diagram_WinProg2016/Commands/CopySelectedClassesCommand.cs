using Diagram_WinProg2016.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Diagram_WinProg2016.Commands
{
	class CopySelectedClassesCommand : IUndoRedoCommand
	{
		private ObservableCollection<Class> classBoxes;
		private ObservableCollection<Class> copyBoxes;

		public CopySelectedClassesCommand(ObservableCollection<Class> classBoxes, ObservableCollection<Class> copyBoxes) {
			this.classBoxes = classBoxes;
			this.copyBoxes = copyBoxes;
			this.copyBoxes.Clear();
		}

		public void Execute() {
			
			foreach (Class classItem in classBoxes)
			{
				if (classItem.IsSelected)
				{
					Class copyClass = new Class(classBoxes.Count + 1);
                    copyClass.X = classItem.X;
                    copyClass.Y = classItem.Y;
                    copyClass.ClassName = classItem.ClassName;
                    copyClass.FieldString = classItem.FieldString;
                    copyClass.MethodString = classItem.MethodString;

                    copyBoxes.Add(copyClass);
					classItem.IsSelected = false;
				}
			}
		}

		public void UnExecute() {
			
		}
	}
}
