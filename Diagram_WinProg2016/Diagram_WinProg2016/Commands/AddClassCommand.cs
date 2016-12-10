using Diagram_WinProg2016.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Commands
{
    //
    // Class used to add ClassBox's to the Grid
    //

    public class AddClassCommand : IUndoRedoCommand
    {
        private ObservableCollection<Class>Classes;
        private Class NewClass;

        public AddClassCommand(ObservableCollection<Class> _Classes)
        {
            this.Classes = _Classes;
            NewClass = new Class();
        }

        public void Execute()
        {
            Classes.Add(NewClass);
        }

        public void UnExecute()
        {
            Classes.Remove(NewClass);
        }
    }
}
