using Diagram_WinProg2016.Commands;
using Diagram_WinProg2016.Model;
using Diagram_WinProg2016.ViewModel;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Diagram_WinProg2016.Commands
{
    public class AddArrowsCommand : IUndoRedoCommand
    {
        private ObservableCollection<Connector> arrows;
        private Connector addingTheArrow;
        public AddArrowsCommand(ObservableCollection<Connector> _addArrow, Class _startClass, Class _endClass) { arrows = _addArrow; addingTheArrow = new Connector(_startClass, _endClass); }

        //undoRedoController.AddAndExecute(new ArrowsCommand(Edges, addingEdgeFromA, (Class)movingClass.DataContext));


        void IUndoRedoCommand.Execute()
        {
            arrows.Add(addingTheArrow);
        }

        void IUndoRedoCommand.UnExecute()
        {
            arrows.Remove(addingTheArrow);
        }
    }

    internal interface IObservableCollection<T>
    {
    }
}
