using Diagram_WinProg2016.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Commands
{
	class DeleteSelectedClassesCommand : IUndoRedoCommand
	{
		private ObservableCollection<Class> classBoxes;
		private ObservableCollection<Class> removeBoxes;

		public DeleteSelectedClassesCommand(ObservableCollection<Class> classBoxes)
		{
			this.classBoxes = classBoxes;
			removeBoxes = new ObservableCollection<Class>();
			foreach (Class classItem in classBoxes)
			{
				if (classItem.IsSelected) removeBoxes.Add(classItem);
			}
		}

		public void Execute()
		{
			foreach (Class removeBox in removeBoxes)
			{
				classBoxes.Remove(removeBox);
			}
		}

		public void UnExecute()
		{
			foreach (Class removeBox in removeBoxes)
			{
				classBoxes.Add(removeBox);
			}
		}
	}
}
