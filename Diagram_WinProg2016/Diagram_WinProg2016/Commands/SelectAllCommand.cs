using Diagram_WinProg2016.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Commands
{
	class SelectAllCommand : IUndoRedoCommand
	{
		private ObservableCollection<Class> classBoxes;

		public SelectAllCommand(ObservableCollection<Class> classBoxes)
		{
			this.classBoxes = classBoxes;
		}

		public void Execute()
		{
			foreach (Class classItem in classBoxes)
			{
				classItem.IsSelected = true;
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
