using Diagram_WinProg2016.Model;
using System.Collections.ObjectModel;


namespace Diagram_WinProg2016.Commands
{
    class PasteSelectedClassesCommand : IUndoRedoCommand
    {
        private ObservableCollection<Class> Classes;
        private ObservableCollection<Class> CopiedClasses;
		private ObservableCollection<Class> undoCollection;

        public PasteSelectedClassesCommand(ObservableCollection<Class> classBoxes, ObservableCollection<Class> copyBoxes)
        {
			undoCollection = new ObservableCollection<Class>();
            this.Classes = classBoxes;
            this.CopiedClasses = copyBoxes;
        }

        public void Execute()
        {
            foreach (var classItem in CopiedClasses)
            {
                var newClass = new Class();
                newClass.X = classItem.X + 20;
                newClass.Y = classItem.Y + 20;
                newClass.ClassName = classItem.ClassName;
                newClass.FieldString = classItem.FieldString;
                newClass.MethodString = classItem.MethodString;
                Classes.Add(newClass);
				undoCollection.Add(newClass);

            }
        }

        public void UnExecute()
        {
			foreach (var classItem in undoCollection)
			{
				Classes.Remove(classItem);
			}
        }
    }
}
