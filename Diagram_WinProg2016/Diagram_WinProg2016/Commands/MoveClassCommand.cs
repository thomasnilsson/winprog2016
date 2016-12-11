using Diagram_WinProg2016.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Commands
{
    //
    // Class used to move the x- and y-coordinates of a ClassBox
    //

    public class MoveClassCommand : IUndoRedoCommand
    {
        private Class Class;
        private ObservableCollection<Edge> edges;
        private int x;
        private int y;
        private int newX;
        private int newY;

        public MoveClassCommand(Class _Class, ObservableCollection<Edge> _edges, int _newX, int _newY, int _x, int _y) { 
            Class = _Class;
            edges = _edges;
            newX = _newX; 
            newY = _newY; 
            x = _x; 
            y = _y; 
        }

        public void Execute()
        {
            Class.X = newX;
            Class.Y = newY;
            
           foreach (var edge in edges)
            {
                if (Class.Equals(edge.From))
                {
                    edge.Points = new Edge(Class, edge.To).Points;
                }
                if (Class.Equals(edge.To))
                {
                    edge.Points = new Edge(edge.From, Class).Points;
                }
            }
             
        }
             
        

        public void UnExecute()
        {
            Class.X = x;
            Class.Y = y;
            
            foreach (var edge in edges)
            {
                if (Class.Equals(edge.From))
                {
                    edge.Points = new Edge(Class, edge.To).Points;
                }
                if (Class.Equals(edge.To))
                {
                    edge.Points = new Edge(edge.From, Class).Points;
                }
            }
            
        }
    }
}
