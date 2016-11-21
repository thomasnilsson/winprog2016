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
			Trace.Write("Creating CopySelectedClassesCommand\n");
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

				}
			}
			Trace.Write("Executing CopySelectedClassesCommand\n");
			foreach (Class classItem in classBoxes)
			{
				Trace.Write("classBoxes contained item: " + classItem.ToString() + "\n");
			}
			foreach (Class classItem in copyBoxes)
			{
				Trace.Write("copyBoxes contained item: " + classItem.ToString() + "\n");
			}
		}

		public void UnExecute() {
			
		}
	}
}
