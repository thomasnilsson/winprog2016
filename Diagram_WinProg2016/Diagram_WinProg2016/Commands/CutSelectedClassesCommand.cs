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
	class CutSelectedClassesCommand : IUndoRedoCommand
	{
		private ObservableCollection<Class> classBoxes;
		private ObservableCollection<Class> copyBoxes;
		private ObservableCollection<Class> removeBoxes;

		public CutSelectedClassesCommand(ObservableCollection<Class> classBoxes, ObservableCollection<Class> copyBoxes) {
			Trace.Write("Creating CutSelectedClassesCommand\n");
			this.classBoxes = classBoxes;
			this.copyBoxes = copyBoxes;
			removeBoxes = new ObservableCollection<Class>();
			this.copyBoxes.Clear();
		}

		public void Execute() {
			Trace.Write("Executing CutSelectedClassesCommand\n");
			foreach (Class classItem in classBoxes)
			{
				if(classItem.IsSelected) {
					removeBoxes.Add(classItem);
					Class copyClass = new Class(classItem);
					copyBoxes.Add(classItem);
				}
			}
			foreach(Class classItem in removeBoxes) {
				classBoxes.Remove(classItem);
			}
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
			//
		}
	}
}
