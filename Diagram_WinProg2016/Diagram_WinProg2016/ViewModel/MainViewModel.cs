using GalaSoft.MvvmLight;
using Diagram_WinProg2016.Model;
using Diagram_WinProg2016.Command;
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
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private bool isAddingEdge;
        private Point moveClassBoxPoint;// Gemmer det første punkt som punktet har under en flytning.
        private Point offsetPosition; //Bruges så klassen bliver flyttet flot rundt
        private int oldPosX; // bruges naar moveClassCommand kaldes
        private int oldPosY;// bruges naar moveClassCommand kaldes
        public ObservableCollection<Class> Classes{ get; set; }
        public ObservableCollection<Class> SelectedClassBox { get; set; }
        private UndoRedoController undoRedoController = UndoRedoController.GetInstance();

        public ICommand AddClassCommand { get; private set; }

        public MainViewModel()
        {
            Classes = new ObservableCollection<Class>();
            AddClassCommand = new RelayCommand(AddClassBox);
            SelectedClassBox = new ObservableCollection<Class>();
            isAddingEdge = false;

            Classes.Add(new Class(Classes.Count));
        }

        public void AddClassBox()
        {
            //undoRedoController.AddAndExecute(new AddClassCommand(ClassBoxs));
        }

        // Action for Mouse down trigger on ClassBox
        // Hvis der ikke er ved at blive tilføjet en kant så fanges musen når en musetast trykkes ned. Dette bruges til at flytte punkter.
        public void MouseDownClassBox(MouseButtonEventArgs e)
        {
            if (!isAddingEdge)
            {
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
            {
                e.MouseDevice.Target.ReleaseMouseCapture();
            }
                
        } 
        // Action for Mouse down trigger on ClassBox
        // Hvis der ikke er ved at blive tilføjet en kant så fanges musen når en musetast trykkes ned. Dette bruges til at flytte punkter.
       
        
        
       public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private static T FindParentOfType<T>(DependencyObject o)
        {
            dynamic parent = VisualTreeHelper.GetParent(o);
            return parent.GetType().IsAssignableFrom(typeof(T)) ? parent : FindParentOfType<T>(parent);
        }
    }
}