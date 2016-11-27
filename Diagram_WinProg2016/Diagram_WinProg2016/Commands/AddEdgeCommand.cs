using Diagram_WinProg2016.Commands;
using Diagram_WinProg2016.Model;
using Diagram_WinProg2016.ViewModel;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Diagram_WinProg2016.Commands
{
    public class AddEdgeCommand : IUndoRedoCommand
    {
        private ObservableCollection<Edge> edges;
        private Edge edge;

        public AddEdgeCommand(ObservableCollection<Edge> _edges, Class _endA, Class _endB)
        {
            edges = _edges;
            edge = new Edge(_endA, _endB);
            
        }

        public void Execute()
        {
            edges.Add(edge);
        }

        public void UnExecute()
        {
            edges.Remove(edge);
        }
    }
}
