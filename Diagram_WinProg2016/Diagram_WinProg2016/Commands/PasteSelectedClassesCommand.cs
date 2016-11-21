using Diagram_WinProg2016.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Commands
{
	class PasteSelectedClassesCommand : IUndoRedoCommand
	{
		private ObservableCollection<Class> classBoxes;
		private ObservableCollection<Class> copyBoxes;

		public PasteSelectedClassesCommand(ObservableCollection<Class> classBoxes, ObservableCollection<Class> copyBoxes) {
			this.classBoxes = classBoxes;
			this.copyBoxes = copyBoxes;
		}

		public void Execute() {
			foreach (Class classItem in copyBoxes)
			{
				Class newClass = new Class(classBoxes.Count + 1, classItem.X+10, classItem.Y+10, classItem.ClassName, classItem.MethodString, classItem.FieldString);
				classBoxes.Add(newClass);

			}
		}

		public void UnExecute() {

		}
	}
}
