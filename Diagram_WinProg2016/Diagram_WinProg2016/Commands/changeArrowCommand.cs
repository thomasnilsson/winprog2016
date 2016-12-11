using Diagram_WinProg2016.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Commands
{
    class ChangeArrowCommand : IUndoRedoCommand
    {
        private Edge edge, newEdge;
        private ObservableCollection<Edge> Edges;
        private Class start, end;

        public ChangeArrowCommand(ObservableCollection<Edge> _Edges, Edge _edge)
        {
            Edges = _Edges;
            edge = _edge;
            start = edge.From;
            end = edge.To;
        }

        public void Execute()
        {
            Edges.Remove(edge);
            newEdge = new Edge(end, start);
            Edges.Add(newEdge);
        }

        public void UnExecute()
        {
            Edges.Remove(newEdge);
            Edges.Add(edge);
        }
    }
}
