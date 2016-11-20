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
    public class AddArrowCommand : IUndoRedoCommand

    {
        private ObservableCollection<ConnectorViewModel> arrows;
        private ConnectorViewModel _addArrow;

        public AddArrowCommand(ObservableCollection<ConnectorViewModel> _arrows, ConnectorViewModel newArrows)
        {
            arrows = _arrows;
            _addArrow = newArrows;

        }


        void IUndoRedoCommand.Execute()
        {
            arrows.Add(_addArrow);
        }

        void IUndoRedoCommand.UnExecute()
        {
            arrows.Remove(_addArrow);
        }
    }

    internal interface IObservableCollection<T>
    {
    }
}
