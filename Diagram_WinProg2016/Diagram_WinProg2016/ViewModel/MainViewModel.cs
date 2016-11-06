using GalaSoft.MvvmLight;
using Diagram_WinProg2016.Command;
using Diagram_WinProg2016.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Shapes;

namespace Diagram_WinProg2016.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Class> Classes{ get; set; }

        public ICommand AddClassCommand { get; private set; }

        public ICommand SaveAsPngCommand { get; private set; }


        public ICommand OpenDiagram { get; private set; }

        public ObservableCollection<Class> ClassBoxes { get; set; }

        private UndoRedoController undoRedoController = UndoRedoController.GetInstance();
        public ObservableCollection<Class> SelectedClassBox { get; set; }
        // Er der ved at blive tilfojet en kant?
        private bool isAddingEdge;

        //Punkter når der flyttes rundt. 
        private Point moveClassBoxPoint;// Gemmer det første punkt som punktet har under en flytning.
        private Point offsetPosition; //Bruges så klassen bliver flyttet flot rundt
        private int oldPosX; // bruges naar moveClassCommand kaldes
        private int oldPosY;// bruges naar moveClassCommand kaldes

        public MainViewModel()
        {
            Classes = new ObservableCollection<Class>();
            AddClassCommand = new RelayCommand(AddClassBox);
            OpenDiagram = new RelayCommand(OpenNewDiagram);
            isAddingEdge = false;
            SaveAsPngCommand = new RelayCommand<StackPanel>(saveScreen);
            Classes.Add(new Class(Classes.Count));
            Trace.WriteLine("Classes: " + Classes.Count);
        }
        public void AddClassBox()
        {
            undoRedoController.AddAndExecute(new AddClassCommand(Classes));
            Trace.WriteLine("ADD CLASS COMMAND WAS EXECUTED");
        }
        public void saveScreen(StackPanel input)
        {
            new SaveAsPngCommand(input);
        }

        //////////////////Mouse actions//////////////////////////////////
       
       public void OpenNewDiagram()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG documents (.png)|*.png"; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

           
            if (result == true)
            {//Needs to clear the diagram if one is open
                string name = dlg.FileName;
                new Open();
            }
        }


        // Action for Mouse down trigger on ClassBox
        // Hvis der ikke er ved at blive tilføjet en kant så fanges musen når en musetast trykkes ned. Dette bruges til at flytte punkter.
        public void MouseDownClassBox(MouseButtonEventArgs e)
        {
            if (!isAddingEdge)
            {
                //if (selectedEdge != null)
                //{
                //    selectedEdge.IsSelected = false;
                //    selectedEdge = null;
                //}
                e.MouseDevice.Target.CaptureMouse();
                FrameworkElement movingClass = (FrameworkElement)e.MouseDevice.Target;
                Class movingClassBox = (Class)movingClass.DataContext;
                Canvas canvas = FindParentOfType<Canvas>(movingClass);
                offsetPosition = Mouse.GetPosition(canvas);
                oldPosX = movingClassBox.X;
                oldPosY = movingClassBox.Y;

                if (SelectedClassBox.Count == 0)
                {
                    SelectedClassBox.Add(movingClassBox);
                }
                else
                {
                    SelectedClassBox.ElementAt(0).IsSelected = false;
                    SelectedClassBox.Clear();
                    SelectedClassBox.Add(movingClassBox);
                }
                movingClassBox.IsSelected = true;
            }
        }
        // Action for Mouse move trigger
        public void MouseMoveClassBox(MouseEventArgs e)
        {
            // Tjek at musen er fanget og at der ikke er ved at blive tilføjet en kant.
            if (Mouse.Captured != null && !isAddingEdge)
            {
                FrameworkElement movingClass = (FrameworkElement)e.MouseDevice.Target;
                Class movingClassBox = (Class)movingClass.DataContext;

                Canvas canvas = FindParentOfType<Canvas>(movingClass);
                Point mousePosition = Mouse.GetPosition(canvas);

                mousePosition.X -= offsetPosition.X;
                mousePosition.Y -= offsetPosition.Y;

                if (oldPosX + mousePosition.X >= 0) { moveClassBoxPoint.X = movingClassBox.X = oldPosX + (int)mousePosition.X; }
                else { moveClassBoxPoint.X = movingClassBox.X = 0; }

                if (oldPosY + mousePosition.Y >= 0) { moveClassBoxPoint.Y = movingClassBox.Y = oldPosY + (int)mousePosition.Y; }
                else { moveClassBoxPoint.Y = movingClassBox.Y = 0; }
            }
        }
        // Action for Mouse up trigger on ClassBox
        // Benyttes til at flytte punkter og tilføje kanter.
        public void MouseUpClassBox(MouseButtonEventArgs e)
        {
            FrameworkElement movingClass = (FrameworkElement)e.MouseDevice.Target;
            Class movingClassBox = (Class)movingClass.DataContext;

            if (moveClassBoxPoint != default(Point))
            {
                Canvas canvas = FindParentOfType<Canvas>(movingClass);
                Point mousePosition = Mouse.GetPosition(canvas);
                undoRedoController.AddAndExecute(new MoveClassBoxCommand(movingClassBox, movingClassBox.X, movingClassBox.Y, (int)oldPosX, (int)oldPosY));
                // Nulstil værdier.
                moveClassBoxPoint = new Point();
                // Musen frigøres.
                e.MouseDevice.Target.ReleaseMouseCapture();
            }
            else
                e.MouseDevice.Target.ReleaseMouseCapture();
        }
        private static T FindParentOfType<T>(DependencyObject o)
        {
            dynamic parent = VisualTreeHelper.GetParent(o);
            return parent.GetType().IsAssignableFrom(typeof(T)) ? parent : FindParentOfType<T>(parent);
        }
    }
}
