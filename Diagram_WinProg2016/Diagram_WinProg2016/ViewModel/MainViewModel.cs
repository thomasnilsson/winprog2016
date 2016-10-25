using System;
using System.Collections.Generic;
using System.Linq;
using Diagram_WinProg2016.Commands
using System.Text;
using System.Threading.Tasks;



namespace Diagram_WinProg2016.ViewModel
{
    public class MainViewModel
    {
        private UndoRedoControllerCmd undoRedoController = UndoRedoControllerCmd.Instance;

        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }



    }
}
