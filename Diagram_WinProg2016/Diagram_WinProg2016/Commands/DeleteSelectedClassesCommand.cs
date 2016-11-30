using Diagram_WinProg2016.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Commands
{
	class DeleteSelectedClassesCommand : IUndoRedoCommand
	{
		private ObservableCollection<Class> classes;
		private ObservableCollection<Class> classesToRemove;
        private ObservableCollection<Edge> edges;
        private ObservableCollection<Edge> edgesToRemove;

        public DeleteSelectedClassesCommand(ObservableCollection<Class> _classes, ObservableCollection<Edge> _edges)
		{
			classes = _classes;
			classesToRemove = new ObservableCollection<Class>();
            edges = _edges;
            edgesToRemove = new ObservableCollection<Edge>();

			foreach (Class classItem in classes)
			{
                if (classItem.IsSelected)
                {
                    classesToRemove.Add(classItem);
                    foreach (Edge edgeItem in edges)
                    {
                        if (classItem.Equals(edgeItem.EndA) || classItem.Equals(edgeItem.EndB))
                        {
                            edgesToRemove.Add(edgeItem);
                        }
                    }
                }
			}

            
		}

		public void Execute()
		{
			foreach (Class classToRemove in classesToRemove)
			{
				classes.Remove(classToRemove);
			}

            foreach (Edge edgeToRemove in edgesToRemove)
            {
                edges.Remove(edgeToRemove);
            }
		}

		public void UnExecute()
		{
			foreach (Class removedClass in classesToRemove)
			{
				classes.Add(removedClass);
			}

            foreach (Edge removedEdge in edgesToRemove)
            {
                edges.Remove(removedEdge);
            }
        }
	}
}
