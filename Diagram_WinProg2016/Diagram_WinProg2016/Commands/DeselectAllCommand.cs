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
		private ObservableCollection<Class> undoCollection;

		public DeselectAllCommand(ObservableCollection<Class> classBoxes)
		{
			this.undoCollection = new ObservableCollection<Class>();
			this.classBoxes = classBoxes;
		}

		public void Execute()
		{
			foreach (Class classItem in classBoxes)
			{
				if(classItem.IsSelected==true) { 
					classItem.IsSelected = false;
					undoCollection.Add(classItem);
				}
			}
		}

		public void UnExecute()
		{
			foreach (Class classItem in undoCollection)
			{
				classItem.IsSelected = true;
			}
		}
	}
}
