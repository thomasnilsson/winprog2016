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
		private ObservableCollection<Class> Classes;

		public SelectAllCommand(ObservableCollection<Class> _Classes)
		{
			this.Classes = _Classes;
		}

		public void Execute()
		{
			foreach (var classItem in Classes)
			{
				classItem.IsSelected = true;
			}
		}

		public void UnExecute()
		{
			foreach (var classItem in Classes)
			{
				classItem.IsSelected = false;
			}
		}
	}
}
