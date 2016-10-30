using Diagram_WinProg2016.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Command
{
    //
    // Class used to add ClassBox's to the Grid
    //

    public class AddClassCommand : IUndoRedoCommand
    {
        private ObservableCollection<Class> classBoxes;
        private Class classBox;

        public AddClassCommand(ObservableCollection<Class> _classBox) { 
            classBoxes = _classBox;
            classBox = new Class(classBoxes.Count + 1);
        }

        public void Execute()
        {
            classBoxes.Add(classBox);
        }

        public void UnExecute()
        {
            classBoxes.Remove(classBox);
        }
    }
}
