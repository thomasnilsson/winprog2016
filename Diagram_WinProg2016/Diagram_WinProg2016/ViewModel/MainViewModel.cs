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
using System.Windows.Shapes;

namespace Diagram_WinProg2016.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Class> Classes{ get; set; }

        public ICommand AddClassCommand { get; private set; }
        public ObservableCollection<Class> ClassBoxes { get; set; }

        private UndoRedoController undoRedoController = UndoRedoController.GetInstance();
        public ObservableCollection<Class> SelectedClassBox { get; set; }
        // Er der ved at blive tilfojet en kant?
        private bool isAddingEdge;

        //Punkter n�r der flyttes rundt. 
        private Point moveClassBoxPoint;// Gemmer det f�rste punkt som punktet har under en flytning.
        private Point offsetPosition; //Bruges s� klassen bliver flyttet flot rundt
        private int oldPosX; // bruges naar moveClassCommand kaldes
        private int oldPosY;// bruges naar moveClassCommand kaldes

        public MainViewModel()
        {
            Classes = new ObservableCollection<Class>();
            AddClassCommand = new RelayCommand(AddClassBox);


        }
        public void AddClassBox()
        {
            //undoRedoController.AddAndExecute(new AddClassCommand(ClassBoxs));
        }
        //////////////////Mouse actions//////////////////////////////////

        //// Action for Mouse down trigger on Edge
        //public void MouseDownEdge(MouseButtonEventArgs e)
        //{
        //    if (!isAddingEdge)
        //    {
        //        if (SelectedClassBox.Count == 1)
        //        {
        //            SelectedClassBox.ElementAt(0).IsSelected = false;
        //            SelectedClassBox.Clear();
        //        }
        //        e.MouseDevice.Target.CaptureMouse();
        //        FrameworkElement edgeElement = (FrameworkElement)e.MouseDevice.Target;
        //        Edge edge = (Edge)edgeElement.DataContext;
        //        edge.IsSelected = true;
        //        if (selectedEdge != null)
        //        {
        //            selectedEdge.IsSelected = false;
        //        }
        //        selectedEdge = edge;
        //    }
        //}
        //// Action for Mouse up trigger on Edge
        //public void MouseUpEdge(MouseButtonEventArgs e)
        //{
        //    e.MouseDevice.Target.ReleaseMouseCapture();
        //}
        // Action for Mouse down trigger on ClassBox
        // Hvis der ikke er ved at blive tilf�jet en kant s� fanges musen n�r en musetast trykkes ned. Dette bruges til at flytte punkter.
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
            // Tjek at musen er fanget og at der ikke er ved at blive tilf�jet en kant.
            if (Mouse.Captured != null && !isAddingEdge)
            {
                FrameworkElement movingClass = (FrameworkElement)e.MouseDevice.Target;
                ClassBox movingClassBox = (ClassBox)movingClass.DataContext;

                Canvas canvas = FindParentOfType<Canvas>(movingClass);
                Point mousePosition = Mouse.GetPosition(canvas);

                mousePosition.X -= offsetPosition.X;
                mousePosition.Y -= offsetPosition.Y;

                if (oldPosX + mousePosition.X >= 0) { moveClassBoxPoint.X = movingClassBox.X = oldPosX + (int)mousePosition.X; }
                else { moveClassBoxPoint.X = movingClassBox.X = 0; }

                if (oldPosY + mousePosition.Y >= 0) { moveClassBoxPoint.Y = movingClassBox.Y = oldPosY + (int)mousePosition.Y; }
                else { moveClassBoxPoint.Y = movingClassBox.Y = 0; }

                // Updating the edges associated with the classbox being moved
                foreach (Edge edge in Edges)
                {
                    if (movingClassBox.Equals(edge.EndA))
                    {
                        edge.Points = new Edge(movingClassBox, edge.EndB).Points;
                    }
                    if (movingClassBox.Equals(edge.EndB))
                    {
                        edge.Points = new Edge(edge.EndA, movingClassBox).Points;
                    }
                }
            }
        }
        // Action for Mouse up trigger on ClassBox
        // Benyttes til at flytte punkter og tilf�je kanter.
        public void MouseUpClassBox(MouseButtonEventArgs e)
        {
            FrameworkElement movingClass = (FrameworkElement)e.MouseDevice.Target;
            ClassBox movingClassBox = (ClassBox)movingClass.DataContext;

            if (isAddingEdge)
            {
                // Hvis det er den f�rste klasse der er blevet trykket p� under tilf�jelsen af kanten, s� gemmes punktet bare og punktet bliver markeret som valgt.
                if (addingEdgeEndA == null)
                {
                    addingEdgeEndA = movingClassBox;
                    addingEdgeEndA.IsSelected = true;
                }
                // Ellers hvis det ikke er den f�rste og de to noder der h�rer til klasserne er forskellige, s� oprettes kanten med kommando.
                else if (addingEdgeEndA != movingClassBox)
                {
                    undoRedoController.AddAndExecute(new AddEdgeCommand(Edges, addingEdgeEndA, (ClassBox)movingClass.DataContext));
                    // De tilh�rende v�rdier nulstilles.
                    isAddingEdge = false;
                    RaisePropertyChanged("ModeOpacity");
                    addingEdgeEndA.IsSelected = false;
                    addingEdgeEndA = null;
                }
            }
            else if (moveClassBoxPoint != default(Point))
            {
                Canvas canvas = FindParentOfType<Canvas>(movingClass);
                Point mousePosition = Mouse.GetPosition(canvas);
                undoRedoController.AddAndExecute(new MoveClassBoxCommand(movingClassBox, Edges, movingClassBox.X, movingClassBox.Y, (int)oldPosX, (int)oldPosY));
                // Nulstil v�rdier.
                moveClassBoxPoint = new Point();
                // Musen frig�res.
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