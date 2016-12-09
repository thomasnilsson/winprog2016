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
        public ObservableCollection<Class> SelectedClassBox { get; set; }
        
        //commands
        //mouse commands
        public ICommand HelpCommand { get; private set; }
        public ICommand MouseDownClassBoxCommand { get; private set; }
        public ICommand MouseMoveClassBoxCommand { get; private set; }
        public ICommand MouseUpClassBoxCommand { get; private set; }
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

        public ObservableCollection<Class> ClassBoxes { get; set; }

        private UndoRedoController undoRedoController = UndoRedoController.GetInstance();
        public object ExportToImage { get; private set; }
        //Punkter når der flyttes rundt. 
        private Point moveClassBoxPoint;// Gemmer det første punkt som punktet har under en flytning.
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
            SelectedClassBox = new ObservableCollection<Class>();
            Edges = new ObservableCollection<Edge>();
            SelectedEdges = new ObservableCollection<Edge>();
            CopiedClasses = new ObservableCollection<Class>();

            //undoable and redoable commands
            AddEdgeCommand = new RelayCommand(ToggleEdge);
            AddClassCommand = new RelayCommand(AddClassBox);
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
            MouseDownClassBoxCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownClassBox);
            MouseMoveClassBoxCommand = new RelayCommand<MouseEventArgs>(MouseMoveClassBox);
            MouseUpClassBoxCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpClassBox);
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
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "Class Diagram";
            dialog.Filter = "XML file (*.xml)|*.xml| All files (*.*) | *.*";

            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                BackgroundWorker serializer = new BackgroundWorker();
                serializer.DoWork += serializer_DoWork;
                serializer.RunWorkerAsync(path);
            }
        }

        //taken from top answer by "mservidio" http://stackoverflow.com/questions/5794386/basic-backgroundworker-usage-with-parameters
        private void serializer_DoWork(object sender, DoWorkEventArgs e)
        {
            string path = e.Argument as string;
            SerializeObjectToXML(path);
            
        }


        //SAVE XML DIAGRAm
        private void LoadXML()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML file (*.xml)|*.xml| All files (*.*) | *.*";
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                DeSerializeXMLToObject(path);
            }
        }



        //SAVE AS PNG IMAGE
        //from http://stackoverflow.com/questions/34821089/extract-image-from-wpf-image-control-and-save-it-to-a-png-file-on-my-local-pcc
        public void SavePNG(Canvas screen)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "Class Diagram Printout";
            dialog.Filter = "PNG file (*.png)|*.png| All files (*.*) | *.*";
            if (dialog.ShowDialog() == true)
            {

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)screen.ActualWidth, (int)screen.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
                renderBitmap.Render(screen);

                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                FileStream stream = new FileStream(dialog.FileName, FileMode.Create);
                encoder.Save(stream);
            }
        }

        //serialization used when saving as XML
        public void SerializeObjectToXML(string filepath)
        {
            ObservableCollection<Class> serializetype = Classes;
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Class>));
            using (StreamWriter wr = new StreamWriter(filepath))
            {
                serializer.Serialize(wr, serializetype);
            }

        }
        //de-serialization used when loading from XMl
        private void DeSerializeXMLToObject(string filepath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Class>));
            using (StreamReader wr = new StreamReader(filepath))
            {
                ObservableCollection<Class> LoadedClasses = (ObservableCollection<Class>)serializer.Deserialize(wr);
                Classes.Clear();
                foreach (Class classItem in LoadedClasses)
                {
                    Classes.Add(classItem);
                    System.Console.WriteLine();
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

        // Remove ClassBox and connected Edges
        public void DeleteEdge()
        {
            if (selectedEdge != null)
            {
                selectedEdge.IsSelected = false;
                undoRedoController.AddAndExecute(new RemoveEdgesCommand(Edges, selectedEdge));
                selectedEdge = null;
            }
            
        }

        private Boolean EdgeSelected()
        {
            return selectedEdge != null;
        }
        // is a ClassBox selected?
        public bool SelectedClass()
        {
            return SelectedClassBox.Count == 1;
        }
        // is anything selected?
        public bool SelectedClassOrEdge()
        {
            if (SelectedClassBox.Count == 1) { return true; }
            else if (selectedEdge != null) { return true; }
            else { return false; }
        }

        #endregion

        #region COMMANDS

        public void AddClassBox()
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
            int index = -1;
            int count = 0;
            while (-1 != (index = str.IndexOf(Environment.NewLine, index + 1)))
                count++;

            return count + 1;
        }
        //updates the visual height of the box, used for connecting the edges visually correctly
        public void UpdateVisual()
        {
            foreach (Class _class in Classes)
            {
                var nameLineCount = CountLines(_class.ClassName);
                var fieldLineCount = CountLines(_class.FieldString);
                var methodLineCount = CountLines(_class.MethodString);
                _class.Height = 25 + (nameLineCount * 16) + (fieldLineCount * 16) + (methodLineCount * 16);
                foreach (Edge edge in Edges)
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
            Trace.WriteLine("Toggle edge was called");
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
            foreach (Class selClass in SelectedClassBox)
            {
                selClass.IsSelected = false;
            }
            SelectedClassBox.Clear();
        }


        public void MouseDownEdge(MouseButtonEventArgs e)
        {
            e.MouseDevice.Target.CaptureMouse();
            FrameworkElement _clickedEdge = (FrameworkElement)e.MouseDevice.Target;
            Edge clickedEdge = (Edge)_clickedEdge.DataContext;

            if (!clickedEdge.IsSelected)
            {
                clickedEdge.IsSelected = true;
            }
            else
            {
                clickedEdge.IsSelected = false;
            }
            Trace.WriteLine("Edge is selected? " + clickedEdge.IsSelected);
            
        }
        public void MouseUpEdge(MouseButtonEventArgs e)
        {
            e.MouseDevice.Target.ReleaseMouseCapture();
        }

        public void MouseDownClassBox(MouseButtonEventArgs e)
        {
            
            e.MouseDevice.Target.CaptureMouse();
            FrameworkElement _movingClass = (FrameworkElement)e.MouseDevice.Target;
            Class movingClass = (Class)_movingClass.DataContext;
            Canvas canvas = FindParentOfType<Canvas>(_movingClass);

            Trace.WriteLine(_movingClass.ActualHeight);
            if (!isAddingEdge)
            {
                
                offsetPosition = Mouse.GetPosition(canvas);
                oldPosX = movingClass.X;
                oldPosY = movingClass.Y;

                if (SelectedClassBox.Count == 0)
                {
                    SelectedClassBox.Add(movingClass);
                }
                else
                {
                    SelectedClassBox.ElementAt(0).IsSelected = false;
                    SelectedClassBox.Clear();
                    SelectedClassBox.Add(movingClass);
                }
                SelectedClassBox.Clear();
            }
        }
        // Action for Mouse move trigger
        public void MouseMoveClassBox(MouseEventArgs e)
        {
            // Tjek at musen er fanget og at der ikke er ved at blive tilføjet en kant.
            if (Mouse.Captured != null && !isAddingEdge)
            {
                FrameworkElement movingClass = (FrameworkElement)e.MouseDevice.Target;
                Class movingClassBox = (Class) movingClass.DataContext;

                Canvas canvas = FindParentOfType<Canvas>(movingClass);
                Point mousePosition = Mouse.GetPosition(canvas);

                mousePosition.X = mousePosition.X - offsetPosition.X;
                mousePosition.Y = mousePosition.Y - offsetPosition.Y;

                //int maxX = (int) SystemParameters.WorkArea.Width - movingClassBox.Width;
                int maxX = (int) (SystemParameters.WorkArea.Width - 200); //hardcoded width and height
                int maxY = (int) (SystemParameters.WorkArea.Height - 235);

                #region coordinate constraints
                
                //if going more to the left than possible
                if (oldPosX + mousePosition.X >= 0)
                {
                    moveClassBoxPoint.X = movingClassBox.X = oldPosX + (int)mousePosition.X;
                }else{
                    moveClassBoxPoint.X = movingClassBox.X = 0;
                }
                
                //if going more to the right than possible
                if (oldPosX + mousePosition.X <= maxX)
                {
                    moveClassBoxPoint.X = movingClassBox.X = oldPosX + (int)mousePosition.X;
                }else{
                    moveClassBoxPoint.X = movingClassBox.X = maxX;
                }

                //if going higher than possible
                if (oldPosY + mousePosition.Y >= 0)
                {
                   moveClassBoxPoint.Y = movingClassBox.Y = oldPosY + (int)mousePosition.Y;
                }else {
                    moveClassBoxPoint.Y = movingClassBox.Y = 0;
                }

                //if going lower than possible
                if (oldPosY + mousePosition.Y <= maxY)
                {
                    moveClassBoxPoint.Y = movingClassBox.Y = oldPosY + (int)mousePosition.Y;
                }
                else
                {
                    moveClassBoxPoint.Y = movingClassBox.Y = maxY;
                }
                #endregion

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
        // Benyttes til at flytte punkter og tilføje kanter.
        public void MouseUpClassBox(MouseButtonEventArgs e)
        {
            FrameworkElement _movingClass = (FrameworkElement)e.MouseDevice.Target;
            Class movingClass = (Class)_movingClass.DataContext;
            Trace.WriteLine("Mouse up, is adding edge? " + isAddingEdge);
            if (isAddingEdge)
            {
                //no classes have been selected
                if (SelectedClassBox.Count == 0)
                {
                    SelectedClassBox.Add(movingClass);
                    movingClass.IsSelected = true;
                    Trace.WriteLine("Mouse up, one class " + SelectedClassBox.Count);
                    //COUNT IS NOW 1
                }
               // 1 class is selecteds
                else if (SelectedClassBox[0] != movingClass)
                {
                    SelectedClassBox.Add(movingClass);
                    movingClass.IsSelected = true;

                    Trace.WriteLine("Mouse up, 2 classes " + SelectedClassBox.Count);
                    Trace.WriteLine(SelectedClassBox[0].X + ", " + SelectedClassBox[0].Y);
                    Trace.WriteLine(SelectedClassBox[1].X + ", " + SelectedClassBox[1].Y);

                    undoRedoController.AddAndExecute(new AddEdgeCommand(Edges, SelectedClassBox[0], SelectedClassBox[1]));

                    Trace.WriteLine("New edge created:");
                    Trace.WriteLine(Edges[Edges.Count - 1].EndA.X + ", " + Edges[Edges.Count - 1].EndA.Y);
                    Trace.WriteLine(Edges[Edges.Count - 1].EndB.X + ", " + Edges[Edges.Count - 1].EndB.Y);

                    //Clear the selected classes
                    isAddingEdge = false;
                    SelectedClassBox[0].IsSelected = false;
                    SelectedClassBox[1].IsSelected = false;
                    SelectedClassBox.Clear();
                }
            }

            if (moveClassBoxPoint != default(Point))
            {
                Canvas canvas = FindParentOfType<Canvas>(_movingClass);
                Point mousePosition = Mouse.GetPosition(canvas);
                undoRedoController.AddAndExecute(new MoveClassBoxCommand(movingClass, Edges, movingClass.X, movingClass.Y, (int)oldPosX, (int)oldPosY));
                // Nulstil værdier.
                moveClassBoxPoint = new Point();
                // Musen frigøres.
                e.MouseDevice.Target.ReleaseMouseCapture();
            }
            else
                e.MouseDevice.Target.ReleaseMouseCapture();
        }

        
        private static T FindParentOfType<T>(DependencyObject o) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(o);
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
