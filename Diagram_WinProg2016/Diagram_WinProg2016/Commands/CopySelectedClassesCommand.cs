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
					Class copyClass = new Class(classItem);
					copyBoxes.Add(copyClass);
					classItem.IsSelected = false;
				}
			}
		}

		public void UnExecute() {
			
		}
	}
}
