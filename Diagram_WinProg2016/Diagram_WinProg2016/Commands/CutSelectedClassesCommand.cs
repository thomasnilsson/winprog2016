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
        private ObservableCollection<Class> Classes;
        private ObservableCollection<Class> CopiedClasses;
        private ObservableCollection<Class> removeBoxes;

        public CutSelectedClassesCommand(ObservableCollection<Class> classBoxes, ObservableCollection<Class> copyBoxes)
        {
            this.Classes = classBoxes;
            this.CopiedClasses = copyBoxes;
            removeBoxes = new ObservableCollection<Class>();
            this.CopiedClasses.Clear();
        }

        public void Execute()
        {
            foreach (var classItem in Classes)
            {
                if (classItem.IsSelected)
                {
                    removeBoxes.Add(classItem);
                    var copyClass = new Class();
                    copyClass.X = classItem.X;
                    copyClass.Y = classItem.Y;
                    copyClass.ClassName = classItem.ClassName;
                    copyClass.FieldString = classItem.FieldString;
                    copyClass.MethodString = classItem.MethodString;

                    CopiedClasses.Add(classItem);
                    classItem.IsSelected = false;
                }
            }
            foreach (var classItem in removeBoxes)
            {
                Classes.Remove(classItem);
            }
        }

        public void UnExecute()
        {
			foreach (var classItem in removeBoxes)
			{
				Classes.Add(classItem);
			}
		}
    }
}
