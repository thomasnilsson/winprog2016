using GalaSoft.MvvmLight;
using Diagram_WinProg2016.Commands;
using Diagram_WinProg2016.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.ComponentModel;

namespace Diagram_WinProg2016.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        #region FIELDS
        //classes som er databindet
        public ObservableCollection<Class> Classes { get; set; }
        public ObservableCollection<Class> CopiedClasses { get; set; }
        public ObservableCollection<Edge> Edges { get; set; }
        public ObservableCollection<Edge> SelectedEdges { get; set; }
        public ObservableCollection<Class> SelectedClass { get; set; }
        
        //commands
        //mouse commands
        public ICommand HelpCommand { get; private set; }
        public ICommand MouseDownClassCommand { get; private set; }
        public ICommand MouseMoveClassCommand { get; private set; }
        public ICommand MouseUpClassCommand { get; private set; }
        public ICommand MouseDownEdgeCommand { get; private set; }
        public ICommand MouseUpEdgeCommand { get; private set; }
        public ICommand MouseDownBackgroundCommand { get; private set; }
        public ICommand MouseUpBackgroundCommand { get; private set; }
        public ICommand RightClickBackgroundCommand { get; private set; }


        //misc commands
        public ICommand AddClassCommand { get; private set; }
        public ICommand DeleteSelectedElementsCommand { get; private set; }
        public ICommand CutSelectedClassesCommand { get; private set; }
        public ICommand CopySelectedClassesCommand { get; private set; }
        public ICommand PasteSelectedClassesCommand { get; private set; }
		public ICommand SelectAllCommand { get; private set; }
		public ICommand DeselectAllCommand { get; private set; }
        


        public ICommand UndoCommand { get; set; }
        public ICommand RedoCommand { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }

        public ICommand SavePngCommand { get; private set; }

        public ICommand AddEdgeCommand { get; private set; }

        private UndoRedoController undoRedoController = UndoRedoController.GetInstance();
        //Punkter når der flyttes rundt. 
        private Point initialPoint;// Gemmer det første punkt som punktet har under en flytning.
        private Point offsetPosition; //Bruges så klassen bliver flyttet flot rundt
        private int oldPosX; // bruges naar moveClassCommand kaldes
        private int oldPosY;// bruges naar moveClassCommand kaldes

        public Edge selectedEdge;

        private bool isAddingEdge;


        #endregion

        #region CONSTRUCTOR

        public MainViewModel()
        {
            //observable collections
            Classes = new ObservableCollection<Class>();
            SelectedClass = new ObservableCollection<Class>();
            Edges = new ObservableCollection<Edge>();
            SelectedEdges = new ObservableCollection<Edge>();
            CopiedClasses = new ObservableCollection<Class>();

            //undoable and redoable commands
            AddEdgeCommand = new RelayCommand(ToggleEdge);
            AddClassCommand = new RelayCommand(AddNewClass);
            DeleteSelectedElementsCommand = new RelayCommand(DeleteSelectedClasses);
            CutSelectedClassesCommand = new RelayCommand(CutSelectedClasses);
            CopySelectedClassesCommand = new RelayCommand(CopySelectedClasses);
            PasteSelectedClassesCommand = new RelayCommand(PasteSelectedClasses);
			SelectAllCommand = new RelayCommand(SelectAll);
			DeselectAllCommand = new RelayCommand(DeselectAll);
            UndoCommand = new RelayCommand(Undo);
            RedoCommand = new RelayCommand(Redo);

            //Non undo/redoable commands such as save and load.
            HelpCommand = new RelayCommand(ShowHelpBox);
            SaveCommand = new RelayCommand(SaveXML);
            LoadCommand = new RelayCommand(LoadXML);
            SavePngCommand = new RelayCommand<Canvas>(SavePNG);

            //Mouse commands
            MouseDownClassCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownClass);
            MouseMoveClassCommand = new RelayCommand<MouseEventArgs>(MouseMoveClass);
            MouseUpClassCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpClass);
            RightClickBackgroundCommand = new RelayCommand<MouseButtonEventArgs>(RightClickBackground);
            MouseDownEdgeCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownEdge);
            MouseUpEdgeCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpEdge);
            MouseUpBackgroundCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpBackground);
            MouseDownBackgroundCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownBackground);

        }

        private void ShowHelpBox()
        {
            //shows a message box the user, with instructions.
            string message1 = "To create a new class box press 'Class'. To create additional fields and methods user the ENTER button inside the text boxes.";
            string message2 = "To create a new relation between classes press 'Insert Connector' and press on two classes in succession.";
            string message3 = "If you regret pressing 'Insert Connector', simply right click anywhere.";
            string message4 = "Shortcuts: ";
            string message5 = "Insert class: CTRL + N";
            string message6 = "Add edge: CTRL + E";
            string message7 = "Copy: CTRL + C \n"+"Paste: CTRL + V \n"+"Cut: CTRL + X";
            string message8 = "Select or deselect all: CTRL + A";
            
            MessageBox.Show(message1 + "\n \n" + message2 + "\n\n" + message3 + "\n\n" + message4 + "\n" + message5
                + "\n" + message6 + "\n" + message7 + "\n" + message8 );
        }

        #endregion

        #region SAVE/LOAD
        //SAVE DIAGRAM AS XML http://www.wpf-tutorial.com/dialogs/the-savefiledialog/

        private void SaveXML()
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "Class Diagram";
            dialog.Filter = "XML file (*.xml)|*.xml| All files (*.*) | *.*";

            if (dialog.ShowDialog() == true)
            {
                var path = dialog.FileName;
                var serializer = new BackgroundWorker();
                serializer.DoWork += serializer_DoWork;
                serializer.RunWorkerAsync(path);
            }
        }

        //taken from top answer by "mservidio" http://stackoverflow.com/questions/5794386/basic-backgroundworker-usage-with-parameters
        private void serializer_DoWork(object sender, DoWorkEventArgs e)
        {
            var path = e.Argument as string;
            SerializeObjectToXML(path);
            
        }


        //SAVE XML DIAGRAm
        private void LoadXML()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML file (*.xml)|*.xml| All files (*.*) | *.*";
            if (dialog.ShowDialog() == true)
            {
                var path = dialog.FileName;
                DeSerializeXMLToObject(path);
            }
        }



        //SAVE AS PNG IMAGE
        //from http://stackoverflow.com/questions/34821089/extract-image-from-wpf-image-control-and-save-it-to-a-png-file-on-my-local-pcc
        public void SavePNG(Canvas screen)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "Class Diagram Printout";
            dialog.Filter = "PNG file (*.png)|*.png| All files (*.*) | *.*";
            if (dialog.ShowDialog() == true)
            {

                var encoder = new PngBitmapEncoder();
                var renderBitmap = new RenderTargetBitmap((int)screen.ActualWidth, (int)screen.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
                renderBitmap.Render(screen);

                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                var stream = new FileStream(dialog.FileName, FileMode.Create);
                encoder.Save(stream);
            }
        }

        //serialization used when saving as XML
        public void SerializeObjectToXML(string filepath)
        {
            var SerializedClasses = Classes;
            var serializer = new XmlSerializer(typeof(ObservableCollection<Class>));
            using (var wr = new StreamWriter(filepath))
            {
                serializer.Serialize(wr, SerializedClasses);
            }

        }
        //de-serialization used when loading from XML
        private void DeSerializeXMLToObject(string filepath)
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<Class>));
            using (var wr = new StreamReader(filepath))
            {
                var LoadedClasses = (ObservableCollection<Class>)serializer.Deserialize(wr);
                Classes.Clear();
                Edges.Clear();
                foreach (var classItem in LoadedClasses)
                {
                    Classes.Add(classItem);
                }
                undoRedoController.Reset();
            }
        }
        #endregion

        #region CONNECTORS
        public void ChangeArrow()
        {
            if (selectedEdge != null)
            {
                undoRedoController.AddAndExecute(new ChangeArrowCommand(Edges, selectedEdge));
            }
            selectedEdge = null;
        }

        // Remove selected Class and connected Edges
        public void DeleteEdge()
        {
            if (selectedEdge != null)
            {
                selectedEdge.IsSelected = false;
                undoRedoController.AddAndExecute(new RemoveEdgesCommand(Edges, selectedEdge));
                selectedEdge = null;
            }
            
        }

        #endregion

        #region COMMANDS

        public void AddNewClass()
        {
            undoRedoController.AddAndExecute(new AddClassCommand(Classes));
        }

        public void DeleteSelectedClasses()
        {
            undoRedoController.AddAndExecute(new DeleteSelectedElementsCommand(Classes, Edges));
        }

        public void CopySelectedClasses()
        {
            undoRedoController.AddAndExecute(new CopySelectedClassesCommand(Classes, CopiedClasses));
        }

        public void CutSelectedClasses()
        {
            undoRedoController.AddAndExecute(new CutSelectedClassesCommand(Classes, CopiedClasses));
        }

        public void PasteSelectedClasses()
        {
            undoRedoController.AddAndExecute(new PasteSelectedClassesCommand(Classes, CopiedClasses));
        }
		public void SelectAll() {
			undoRedoController.AddAndExecute(new SelectAllCommand(Classes));
		}
		public void DeselectAll() {
			undoRedoController.AddAndExecute(new DeselectAllCommand(Classes));
		}
        private static int CountLines(string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");
            if (str == string.Empty)
                return 0;
            var index = -1;
            var count = 0;
            while (-1 != (index = str.IndexOf(Environment.NewLine, index + 1)))
                count++;

            return count + 1;
        }
        //updates the visual height of the box, used for connecting the edges visually correctly
        public void UpdateVisual()
        {
            foreach (var _class in Classes)
            {
                var nameLineCount = CountLines(_class.ClassName);
                var fieldLineCount = CountLines(_class.FieldString);
                var methodLineCount = CountLines(_class.MethodString);
                _class.Height = 25 + (nameLineCount * 16) + (fieldLineCount * 16) + (methodLineCount * 16);
                foreach (var edge in Edges)
                {
                    if (_class.Equals(edge.EndA))
                    {
                        edge.Points = new Edge(_class, edge.EndB).Points;
                    }
                    if (_class.Equals(edge.EndB))
                    {
                        edge.Points = new Edge(edge.EndA, _class).Points;
                    }
                }
            }
        }
        // Add Edge
        public void ToggleEdge()
        {
            isAddingEdge = true;
            UpdateVisual();

        }

        public void Undo()
        {
            undoRedoController.Undo();
        }

        public void Redo()
        {
            undoRedoController.Redo();
        }
        #endregion

        #region MOUSE ACTIONS


        private void MouseUpBackground(MouseButtonEventArgs e)
        {
            UpdateVisual(); //update arrow placement
            e.MouseDevice.Target.ReleaseMouseCapture();
        }

        private void MouseDownBackground(MouseButtonEventArgs e)
        {
            e.MouseDevice.Target.CaptureMouse();
        }

        private void RightClickBackground(MouseButtonEventArgs obj)
        {
            //clears adding edges, fx if the user misclicked on "add connector"
            isAddingEdge = false;
            foreach (var selClass in SelectedClass)
            {
                selClass.IsSelected = false;
            }
            SelectedClass.Clear();
        }


        public void MouseDownEdge(MouseButtonEventArgs e)
        {
            e.MouseDevice.Target.CaptureMouse();
            var _clickedEdge = (FrameworkElement)e.MouseDevice.Target;
            var clickedEdge = (Edge)_clickedEdge.DataContext;

            if (!clickedEdge.IsSelected)
            {
                clickedEdge.IsSelected = true;
            }
            else
            {
                clickedEdge.IsSelected = false;
            }
            
        }
        public void MouseUpEdge(MouseButtonEventArgs e)
        {
            e.MouseDevice.Target.ReleaseMouseCapture();
        }

        public void MouseDownClass(MouseButtonEventArgs e)
        {
            
            e.MouseDevice.Target.CaptureMouse();
            var _movingClass = (FrameworkElement)e.MouseDevice.Target;
            var movingClass = (Class)_movingClass.DataContext;
            var canvas = FindParentOfType<Canvas>(_movingClass);
            if (!isAddingEdge)
            {
                
                offsetPosition = Mouse.GetPosition(canvas);
                oldPosX = movingClass.X;
                oldPosY = movingClass.Y;

                if (SelectedClass.Count == 0)
                {
                    SelectedClass.Add(movingClass);
                }
                else
                {
                    SelectedClass.ElementAt(0).IsSelected = false;
                    SelectedClass.Clear();
                    SelectedClass.Add(movingClass);
                }
                SelectedClass.Clear();
            }
        }
        // Action for Mouse move trigger
        public void MouseMoveClass(MouseEventArgs e)
        {
            // Tjek at musen er fanget og at der ikke er ved at blive tilføjet en kant.
            if (Mouse.Captured != null && !isAddingEdge)
            {
                var _movingClassElement = (FrameworkElement)e.MouseDevice.Target;
                var movingClassElement = (Class) _movingClassElement.DataContext;

                var canvas = FindParentOfType<Canvas>(_movingClassElement);
                var mousePosition = Mouse.GetPosition(canvas);

                mousePosition.X = mousePosition.X - offsetPosition.X;
                mousePosition.Y = mousePosition.Y - offsetPosition.Y;
                
                var maxX = (int) (SystemParameters.WorkArea.Width - 200); //hardcoded width and height
                var maxY = (int) (SystemParameters.WorkArea.Height - 235);

                #region coordinate constraints
                
                //if going more to the left than possible
                if (oldPosX + mousePosition.X >= 0)
                {
                    initialPoint.X = movingClassElement.X = oldPosX + (int)mousePosition.X;
                }else{
                    initialPoint.X = movingClassElement.X = 0;
                }
                
                //if going more to the right than possible
                if (oldPosX + mousePosition.X <= maxX)
                {
                    initialPoint.X = movingClassElement.X = oldPosX + (int)mousePosition.X;
                }else{
                    initialPoint.X = movingClassElement.X = maxX;
                }

                //if going higher than possible
                if (oldPosY + mousePosition.Y >= 0)
                {
                   initialPoint.Y = movingClassElement.Y = oldPosY + (int)mousePosition.Y;
                }else {
                    initialPoint.Y = movingClassElement.Y = 0;
                }

                //if going lower than possible
                if (oldPosY + mousePosition.Y <= maxY)
                {
                    initialPoint.Y = movingClassElement.Y = oldPosY + (int)mousePosition.Y;
                }
                else
                {
                    initialPoint.Y = movingClassElement.Y = maxY;
                }
                #endregion

                foreach (var edge in Edges)
                {
                    if (movingClassElement.Equals(edge.EndA))
                    {
                        edge.Points = new Edge(movingClassElement, edge.EndB).Points;
                    }
                    if (movingClassElement.Equals(edge.EndB))
                    {
                        edge.Points = new Edge(edge.EndA, movingClassElement).Points;
                    }
                }
            }
        }
        // Action for Mouse up trigger on Class element
        // Benyttes til at flytte punkter og tilføje kanter.
        public void MouseUpClass(MouseButtonEventArgs e)
        {
            var _movingClass = (FrameworkElement)e.MouseDevice.Target;
            var movingClass = (Class)_movingClass.DataContext;
            if (isAddingEdge)
            {
                //no classes have been selected
                if (SelectedClass.Count == 0)
                {
                    SelectedClass.Add(movingClass);
                    movingClass.IsSelected = true;
                    //COUNT IS NOW 1
                }
               // 1 class is selecteds
                else if (SelectedClass[0] != movingClass)
                {
                    SelectedClass.Add(movingClass);
                    movingClass.IsSelected = true;
                    undoRedoController.AddAndExecute(new AddEdgeCommand(Edges, SelectedClass[0], SelectedClass[1]));

                    //Clear the selected classes
                    isAddingEdge = false;
                    SelectedClass[0].IsSelected = false;
                    SelectedClass[1].IsSelected = false;
                    SelectedClass.Clear();
                }
            }

            if (initialPoint != default(Point))
            {
                var canvas = FindParentOfType<Canvas>(_movingClass);
                var mousePosition = Mouse.GetPosition(canvas);
                undoRedoController.AddAndExecute(new MoveClassCommand(movingClass, Edges, movingClass.X, movingClass.Y, (int)oldPosX, (int)oldPosY));
                // Nulstil værdier.
                initialPoint = new Point();
                // Musen frigøres.
                e.MouseDevice.Target.ReleaseMouseCapture();
            }
            else
                e.MouseDevice.Target.ReleaseMouseCapture();
        }

        
        private static T FindParentOfType<T>(DependencyObject o) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(o);
            if (parent == null) return null;
            return parent.GetType().IsAssignableFrom(typeof(T)) ? (T)parent : FindParentOfType<T>(parent);
        }

		private void Classes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			throw new NotImplementedException();
		}

    }


    #endregion
}
