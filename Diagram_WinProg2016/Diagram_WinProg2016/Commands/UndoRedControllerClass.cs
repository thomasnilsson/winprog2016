using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Commands
{
    public class UndoRedoControllerClass
    {
        #region Fields

        // The Undo stack, holding the Undo/Redo commands that have been executed.
        private readonly Stack<UndoRedoCommand> undoStack = new Stack<UndoRedoCommand>();
        // The Redo stack, holding the Undo/Redo commands that have been executed and then unexecuted (undone).
        private readonly Stack<UndoRedoCommand> redoStack = new Stack<UndoRedoCommand>();
        #endregion



        #region Properties
        public static UndoRedoControllerClass Instance { get; } = new UndoRedoControllerClass();



        #endregion

        #region Constructor
        private UndoRedoControllerClass() { }
        #endregion


        #region Methods

        public void AddAndExecute(UndoRedoCommand command)
        {
            undoStack.Push(command);
            redoStack.Clear();
            command.Execute();
        }


        public bool CanUndo() => undoStack.Any();

        public void Undo()
        {
            if (!undoStack.Any()) throw new InvalidOperationException();
            var command = undoStack.Pop();
            redoStack.Push(command);
            command.UnExecute();
        }

        public bool CanRedo() => redoStack.Any();


        public void Redo()
        {
            if (!redoStack.Any()) throw new InvalidOperationException();
            var command = redoStack.Pop();
            undoStack.Push(command);
            command.Execute();
        }

        #endregion






    }
}
