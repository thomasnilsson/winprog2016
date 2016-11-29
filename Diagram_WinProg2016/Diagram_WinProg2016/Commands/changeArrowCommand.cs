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
        private Edge edge, edgeNew;
        private ObservableCollection<Edge> edges;
        private Class start, end;

        public ChangeArrowCommand(ObservableCollection<Edge> _edges, Edge _edge)
        {
            edges = _edges;
            edge = _edge;
            start = edge.EndA;
            end = edge.EndB;
        }

        public void Execute()
        {
            edges.Remove(edge);
            edgeNew = new Edge(end, start);
            edges.Add(edgeNew);
        }

        public void UnExecute()
        {
            edges.Remove(edgeNew);
            edges.Add(edge);
        }
    }
}
