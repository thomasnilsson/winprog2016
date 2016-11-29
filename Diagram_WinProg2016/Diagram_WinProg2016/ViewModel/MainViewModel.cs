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

namespace Diagram_WinProg2016.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        #region FIELDS
        //bruges til save
        private Thread saveThread;
        //classes som er databindet
        public ObservableCollection<Class> Classes { get; set; }
        public ObservableCollection<Class> CopiedClasses { get; set; }
        public ObservableCollection<Edge> Edges { get; set; }
        public ObservableCollection<Class> SelectedClassBox { get; set; }

        //commands

        public ICommand MouseDownClassBoxCommand { get; private set; }
        public ICommand MouseMoveClassBoxCommand { get; private set; }
        public ICommand MouseUpClassBoxCommand { get; private set; }
        public ICommand MouseDownEdgeCommand { get; private set; }
        public ICommand MouseUpEdgeCommand { get; private set; }

        public ICommand AddClassCommand { get; private set; }
        public ICommand DeleteSelectedClassesCommand { get; private set; }
        public ICommand CutSelectedClassesCommand { get; private set; }
        public ICommand CopySelectedClassesCommand { get; private set; }
        public ICommand PasteSelectedClassesCommand { get; private set; }

        public ICommand UndoCommand { get; set; }
        public ICommand RedoCommand { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }

        public ICommand SavePngCommand { get; private set; }


        public ICommand OpenDiagram { get; private set; }


        public ICommand AddEdgeCommand { get; private set; }
        public ICommand ReverseEdgeCommand { get; private set; }

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
            Classes = new ObservableCollection<Class>();
            SelectedClassBox = new ObservableCollection<Class>();

            Edges = new ObservableCollection<Edge>();
            CopiedClasses = new ObservableCollection<Class>();

            AddEdgeCommand = new RelayCommand(ToggleEdge);
            ReverseEdgeCommand = new RelayCommand(ChangeArrow, EdgeSelected);

            AddClassCommand = new RelayCommand(AddClassBox);
            DeleteSelectedClassesCommand = new RelayCommand(DeleteSelectedClasses);
            CutSelectedClassesCommand = new RelayCommand(CutSelectedClasses);
            CopySelectedClassesCommand = new RelayCommand(CopySelectedClasses);
            PasteSelectedClassesCommand = new RelayCommand(PasteSelectedClasses);

            UndoCommand = new RelayCommand(Undo);
            RedoCommand = new RelayCommand(Redo);


            OpenDiagram = new RelayCommand(OpenNewDiagram);
            SaveCommand = new RelayCommand(SaveXML);
            LoadCommand = new RelayCommand(LoadXML);
            SavePngCommand = new RelayCommand<Canvas>(SavePNG);

            MouseDownClassBoxCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownClassBox);
            MouseMoveClassBoxCommand = new RelayCommand<MouseEventArgs>(MouseMoveClassBox);
            MouseUpClassBoxCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpClassBox);

            MouseDownEdgeCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownEdge);
            MouseUpEdgeCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpEdge);

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
                saveThread = new Thread(() => SerializeObjectToXML(path));
                saveThread.Start();
            }
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
            undoRedoController.AddAndExecute(new DeleteSelectedClassesCommand(Classes));
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

        
        // Add Edge
        public void ToggleEdge()
        {
            Trace.WriteLine("Add edge was called!");
            isAddingEdge = true;
            Trace.WriteLine("adding edge?: " + isAddingEdge);
            //RaisePropertyChanged("ModeOpacity");
        }

        public void Undo()
        {
            undoRedoController.Undo();
        }

        public void Redo()
        {
            undoRedoController.Redo();
        }
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
        #endregion

        #region MOUSE ACTIONS
        
        public void MouseDownEdge(MouseButtonEventArgs e)
        {
            //if (!isAddingEdge)
            //{
            //    if (ClassBoxes.Count == 1)
            //    {
            //        ClassBoxes.ElementAt(0).IsSelected = false;
            //        ClassBoxes.Clear();
            //    }
            //    e.MouseDevice.Target.CaptureMouse();
            //    FrameworkElement edgeElement = (FrameworkElement)e.MouseDevice.Target;
            //}
        }
        public void MouseUpEdge(MouseButtonEventArgs e)
        {
            //e.MouseDevice.Target.ReleaseMouseCapture();
        }

        public void MouseDownClassBox(MouseButtonEventArgs e)
        {
            e.MouseDevice.Target.CaptureMouse();
            FrameworkElement _movingClass = (FrameworkElement)e.MouseDevice.Target;
            Class movingClass = (Class)_movingClass.DataContext;
            Canvas canvas = FindParentOfType<Canvas>(_movingClass);

            if (!isAddingEdge)
            {
                //if (selectedEdge != null)
                //{
                //    selectedEdge.IsSelected = false;
                //    selectedEdge = null;
                //}
                
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
                Class movingClassBox = (Class)movingClass.DataContext;

                Canvas canvas = FindParentOfType<Canvas>(movingClass);
                Point mousePosition = Mouse.GetPosition(canvas);

                mousePosition.X -= offsetPosition.X;
                mousePosition.Y -= offsetPosition.Y;

                if (oldPosX + mousePosition.X >= 0) { moveClassBoxPoint.X = movingClassBox.X = oldPosX + (int)mousePosition.X; }
                else { moveClassBoxPoint.X = movingClassBox.X = 0; }

                if (oldPosY + mousePosition.Y >= 0) { moveClassBoxPoint.Y = movingClassBox.Y = oldPosY + (int)mousePosition.Y; }
                else { moveClassBoxPoint.Y = movingClassBox.Y = 0; }

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

                    // De tilhørende værdier nulstilles.
                    isAddingEdge = false;
                    RaisePropertyChanged("ModeOpacity");
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
    }
    #endregion
}
