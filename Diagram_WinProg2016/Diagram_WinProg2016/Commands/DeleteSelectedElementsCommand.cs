using Diagram_WinProg2016.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Commands
{
	class DeleteSelectedElementsCommand : IUndoRedoCommand
	{
		private ObservableCollection<Class> classes;
		private ObservableCollection<Class> classesToRemove;
        private ObservableCollection<Edge> edges;
        private ObservableCollection<Edge> edgesToRemove;

        public DeleteSelectedElementsCommand(ObservableCollection<Class> _classes, ObservableCollection<Edge> _edges)
		{

            Trace.WriteLine("Delete Command Called");
			classes = _classes;
			classesToRemove = new ObservableCollection<Class>();
            edges = _edges;
            edgesToRemove = new ObservableCollection<Edge>();

            //Find out what to delete:
            //Deleted selected classes and their connected edges
			foreach (var classItem in classes)
			{
                if (classItem.IsSelected)
                {
                    classesToRemove.Add(classItem);
                    foreach (var edgeItem in edges)
                    {
                        if (classItem.Equals(edgeItem.From) || classItem.Equals(edgeItem.To))
                        {
                            edgesToRemove.Add(edgeItem);
                        }
                    }
                }
			}
            
            //Delete selected classes
            foreach (var edgeItem in edges)
            {
                if (edgeItem.IsSelected)
                {
                    edgesToRemove.Add(edgeItem);
                }
            }
		}

		public void Execute()
		{
			foreach (var classToRemove in classesToRemove)
			{
				classes.Remove(classToRemove);
			}

            foreach (var edgeToRemove in edgesToRemove)
            {
                edges.Remove(edgeToRemove);
            }
		}

		public void UnExecute()
		{
			foreach (var removedClass in classesToRemove)
			{
				classes.Add(removedClass);
			}

            foreach (var removedEdge in edgesToRemove)
            {
                edges.Remove(removedEdge);
            }
        }
	}
}
