using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diagram_WinProg2016.Commands
{
    /* 
        Denne klasse holder styr paa kommandoer der kan undo/redoes. 
        Klassen er en singleton,  saa vi er sikret at der kun er en instans af den i programmet. 
        Et singleton pattern kraever 
        - en privat konstruktor saa den ikke kan instatieres
        - en private statisk instance af controlleren  
        - statisk GetInstance() metode som returnere controller instancen. 
     */
    public class UndoRedoController
    {
        // Part of singleton pattern.
        private static UndoRedoController controller = new UndoRedoController();

        private readonly Stack<IUndoRedoCommand> undoStack = new Stack<IUndoRedoCommand>();
        private readonly Stack<IUndoRedoCommand> redoStack = new Stack<IUndoRedoCommand>();
		private LinkedList<IUndoRedoCommand> undoList = new LinkedList<IUndoRedoCommand>();
		private LinkedList<IUndoRedoCommand> redoList = new LinkedList<IUndoRedoCommand>();
        // Part of singleton pattern.
        private UndoRedoController() { }

        // Part of singleton pattern.
        public static UndoRedoController GetInstance() { return controller; }

        // Bruges til at tilføje commander.
        public void AddAndExecute(IUndoRedoCommand command){
			if(undoList.Count > 10) {
				undoList.RemoveLast();
			}
			undoList.AddFirst(command);
			redoList.Clear();
			//Trace.Write("Command was pushed to undostack: " + command.ToString() + "\n");
			undoStack.Push(command);
            redoStack.Clear();
            command.Execute();
        }

        // Sørger for at undo kun kan kaldes når der er kommandoer i undo stacken.
        public bool CanUndo()
        {
            return undoStack.Any();
        }

        // Udfører undo hvis det kan lade sig gøre.
        public void Undo()
        {
            if (undoStack.Count() > 0)
	    {
            IUndoRedoCommand command = undoStack.Pop();
            redoStack.Push(command);
            command.UnExecute();
	    }
        }

        // Sørger for at redo kun kan kaldes når der er kommandoer i redo stacken.
        public bool CanRedo()
        {
            return redoStack.Any();
        }

        // Udfører redo hvis det kan lade sig gøre.
        public void Redo()
        {
            if (redoStack.Count() > 0)
	    {
            IUndoRedoCommand command = redoStack.Pop();
            undoStack.Push(command);
            command.Execute();
	    }
        }

        public void Reset()
        {
            undoStack.Clear();
            redoStack.Clear();
        }
    }
}
