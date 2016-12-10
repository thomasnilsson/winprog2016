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
		private ObservableCollection<Class> Classes;
		private ObservableCollection<Class> undoCollection;

		public DeselectAllCommand(ObservableCollection<Class> _Classes)
		{
			this.undoCollection = new ObservableCollection<Class>();
			this.Classes = _Classes;
		}

		public void Execute()
		{
			foreach (var classItem in Classes)
			{
				if(classItem.IsSelected==true) { 
					classItem.IsSelected = false;
					undoCollection.Add(classItem);
				}
			}
		}

		public void UnExecute()
		{
			foreach (var classItem in undoCollection)
			{
				classItem.IsSelected = true;
			}
		}
	}
}
