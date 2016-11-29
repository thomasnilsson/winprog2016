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

        public CutSelectedClassesCommand(ObservableCollection<Class> classBoxes, ObservableCollection<Class> copyBoxes)
        {
            this.classBoxes = classBoxes;
            this.copyBoxes = copyBoxes;
            removeBoxes = new ObservableCollection<Class>();
            this.copyBoxes.Clear();
        }

        public void Execute()
        {
            foreach (Class classItem in classBoxes)
            {
                if (classItem.IsSelected)
                {
                    removeBoxes.Add(classItem);
                    Class copyClass = new Class();
                    copyClass.X = classItem.X;
                    copyClass.Y = classItem.Y;
                    copyClass.ClassName = classItem.ClassName;
                    copyClass.FieldString = classItem.FieldString;
                    copyClass.MethodString = classItem.MethodString;

                    copyBoxes.Add(classItem);
                    classItem.IsSelected = false;
                }
            }
            foreach (Class classItem in removeBoxes)
            {
                classBoxes.Remove(classItem);
            }
        }

        public void UnExecute()
        {
			foreach (Class classItem in removeBoxes)
			{
				classBoxes.Add(classItem);
			}
		}
    }
}
