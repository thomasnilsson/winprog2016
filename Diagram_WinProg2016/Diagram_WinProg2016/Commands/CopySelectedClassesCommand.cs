using Diagram_WinProg2016.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Diagram_WinProg2016.Commands
{
    class CopySelectedClassesCommand : IUndoRedoCommand
    {
        private ObservableCollection<Class> Classes;
        private ObservableCollection<Class> CopiedClasses;
		private ObservableCollection<Class> undoCollection;

		public CopySelectedClassesCommand(ObservableCollection<Class> classBoxes, ObservableCollection<Class> copyBoxes)
        {
			this.undoCollection = new ObservableCollection<Class>();
            this.Classes = classBoxes;
            this.CopiedClasses = copyBoxes;
            this.CopiedClasses.Clear();
        }

        public void Execute()
        {

            foreach (var classItem in Classes)
            {
                if (classItem.IsSelected)
                {
                    var copyClass = new Class();
                    copyClass.X = classItem.X;
                    copyClass.Y = classItem.Y;
                    copyClass.ClassName = classItem.ClassName;
                    copyClass.FieldString = classItem.FieldString;
                    copyClass.MethodString = classItem.MethodString;

                    CopiedClasses.Add(copyClass);
                    classItem.IsSelected = false;
                }
            }
        }

        public void UnExecute()
        {
            foreach (var classItem in CopiedClasses)
            {
                Classes.Remove(classItem);
            }
        }
    }
}
