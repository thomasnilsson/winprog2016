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
    class AddEdgeCommand : IUndoRedoCommand
    {

        private ObservableCollection<EdgeViewModel> edges;
        private EdgeViewModel _edge;

        public AddEdgeCommand(ObservableCollection<EdgeViewModel> _edges, EdgeViewModel newEdge)
        {
            edges = _edges;
            _edge = newEdge;
        }

        public void Execute()
        {
            edges.Add(_edge);
        }

        public void UnExecute()
        {
            edges.Remove(_edge);
        }
    }
}
