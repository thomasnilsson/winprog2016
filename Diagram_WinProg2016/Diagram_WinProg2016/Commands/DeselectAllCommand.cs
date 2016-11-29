using Diagram_WinProg2016.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Commands
{
	class DeselectAllCommand : IUndoRedoCommand
	{
		private ObservableCollection<Class> classBoxes;

		public DeselectAllCommand(ObservableCollection<Class> classBoxes)
		{
			this.classBoxes = classBoxes;
		}

		public void Execute()
		{
			foreach (Class classItem in classBoxes)
			{
				classItem.IsSelected = false;
			}
		}

		public void UnExecute()
		{
			foreach (Class classItem in classBoxes)
			{
				classItem.IsSelected = false;
			}
		}
	}
}
