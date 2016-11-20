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
        private IObservableCollection<ConnectorViewModel> arrows;
        private ConnectorViewModel addArrow;

        public AddArrowCommand(IObservableCollection<ConnectorViewModel> _arrows, ConnectorViewModel newArrows)
        {
            arrows = _arrows;
            _arrows = newArrows;

        }


        void IUndoRedoCommand.Execute()
        {
            arrows.Add(_arrows);
        }

        void IUndoRedoCommand.UnExecute()
        {
            arrows.Remove(_arrows);
        }
    }

    internal interface IObservableCollection<T>
    {
    }
}
