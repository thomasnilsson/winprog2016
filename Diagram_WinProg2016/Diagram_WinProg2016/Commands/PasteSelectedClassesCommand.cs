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
    class PasteSelectedClassesCommand : IUndoRedoCommand
    {
        private ObservableCollection<Class> classBoxes;
        private ObservableCollection<Class> copyBoxes;

        public PasteSelectedClassesCommand(ObservableCollection<Class> classBoxes, ObservableCollection<Class> copyBoxes)
        {
            this.classBoxes = classBoxes;
            this.copyBoxes = copyBoxes;
        }

        public void Execute()
        {
            foreach (Class classItem in copyBoxes)
            {
                Class newClass = new Class();
                newClass.X = classItem.X + 20;
                newClass.Y = classItem.Y + 20;
                newClass.ClassName = classItem.ClassName;
                newClass.FieldString = classItem.FieldString;
                newClass.MethodString = classItem.MethodString;
                classBoxes.Add(newClass);

            }
        }

        public void UnExecute()
        {

        }
    }
}
