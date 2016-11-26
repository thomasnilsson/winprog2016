using GalaSoft.MvvmLight;
using Diagram_WinProg2016.Commands;
using Diagram_WinProg2016.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging ;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;

namespace Diagram_WinProg2016.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //bruges til save
        private Thread saveThread;
        //classes som er databindet
        public ObservableCollection<Class> Classes{ get; set; }
		public ObservableCollection<Class> CopiedClasses { get; set; }

        private EdgeType type = EdgeType.NOR;
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

        public ICommand AddAGGCommand { get; private set; }
        public ICommand AddASSCommand { get; private set; }
        public ICommand AddCOMCommand { get; private set; }
        public ICommand AddDEPCommand { get; private set; }
        public ICommand AddGENCommand { get; private set; }
        public ICommand AddNORCommand { get; private set; }

        public ICommand SavePngCommand { get; private set; }

        
        public ICommand OpenDiagram { get; private set; }

        public ObservableCollection<Class> ClassBoxes { get; set; }

	public ObservableCollection<Edge> Arrows { get; set; }

        private UndoRedoController undoRedoController = UndoRedoController.GetInstance();
        //public ObservableCollection<Class> SelectedClassBox { get; set; }
        public object ExportToImage { get; private set; }
        private Class focusedClass = null;
        //public Class FocusedClass { get { return focusedClass; } private set { if (focusedClass != null) { FocusedClass.IsSelected = false; } focusedClass = value; if (focusedClass == null) { IsFocused = false; } else { IsFocused = true; FocusedClass.Selected = true; FocusedEdge = null; }; } }


        private bool edgeIsFocused = false;
        //public bool EdgeIsFocused { get { return edgeIsFocused; } set { edgeIsFocused = value; RaisePropertyChanged(() => EdgeIsFocused); RaisePropertyChanged(() => DeleteActive); } }
        private Edge focusedEdge = null;
        //public Edge FocusedEdge { get { return focusedEdge; } private set { if (focusedEdge != null) { FocusedEdge.Selected = false; } focusedEdge = value; if (focusedEdge == null) { EdgeIsFocused = false; } else { EdgeIsFocused = true; FocusedEdge.Selected = true; FocusedClass = null; }; } }

        public ObservableCollection<Edge> Edges { get; set; }
        public Edge selectedArrow;
        // Er der ved at blive tilfojet en kant?
        private bool isAddingEdge = false;
        private Class startEdge;

        //Punkter når der flyttes rundt. 
        private Point moveClassBoxPoint;// Gemmer det første punkt som punktet har under en flytning.
        private Point offsetPosition; //Bruges så klassen bliver flyttet flot rundt
	private Class addingEdgeFromA;
        private int oldPosX; // bruges naar moveClassCommand kaldes
        private int oldPosY;// bruges naar moveClassCommand kaldes

        public MainViewModel()
        {
            Classes = new ObservableCollection<Class>();

			CopiedClasses = new ObservableCollection<Class>();

            AddClassCommand = new RelayCommand(AddClassBox);
			DeleteSelectedClassesCommand = new RelayCommand(DeleteSelectedClasses);
			CutSelectedClassesCommand = new RelayCommand(CutSelectedClasses);
			CopySelectedClassesCommand = new RelayCommand(CopySelectedClasses);
			PasteSelectedClassesCommand = new RelayCommand(PasteSelectedClasses);

			UndoCommand = new RelayCommand(Undo);
			RedoCommand = new RelayCommand(Redo);
     
			
            Edges = new ObservableCollection<Edge>();
            OpenDiagram = new RelayCommand(OpenNewDiagram);
            SaveCommand = new RelayCommand(Save);
            LoadCommand = new RelayCommand(Load);
            SavePngCommand = new RelayCommand<Canvas>(saveScreen);

			MouseDownClassBoxCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownClassBox);
			MouseMoveClassBoxCommand = new RelayCommand<MouseEventArgs>(MouseMoveClassBox);
			MouseUpClassBoxCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpClassBox);

			isAddingEdge = false;

        }

        //SAVE AS DIAGRAM

        public class SaveLoadCollection
        {
            public ObservableCollection<Class> tempClasses = new ObservableCollection<Class>();
            //public ObservableCollection<EdgeViewModel> tempEdges = new ObservableCollection<EdgeViewModel>();
            public SaveLoadCollection(ObservableCollection<Class> classes)
            {
                tempClasses = classes;
            }
            public SaveLoadCollection() { }

        }

        public void SerializeObjectToXML(string filepath)
        {
            SaveLoadCollection serializetype = new SaveLoadCollection(Classes);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveLoadCollection));
            using (StreamWriter wr = new StreamWriter(filepath))
            {
                serializer.Serialize(wr, serializetype);
            }

        }

        private void DeSerializeXMLToObject(string filepath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveLoadCollection));
            using (StreamReader wr = new StreamReader(filepath))
            {
                SaveLoadCollection Load = (SaveLoadCollection)serializer.Deserialize(wr);
                Classes.Clear();
                //ClassIndex = Load.tempNodes.Count + 1;
                foreach (Class tempClass in Load.tempClasses)
                {
                    Classes.Add(tempClass);
                    System.Console.WriteLine();
                }         
                undoRedoController.Reset();
            }
        }

        private void Load()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Title = "Load diagram",
                Filter = "XML (*.xml)|*.xml"
            };
            if (dialog.ShowDialog() != true)
                return;

            string path = dialog.FileName;
            DeSerializeXMLToObject(path);
        }

        private void Save()
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Title = "Save diagram",
                FileName = "classdiagram",
                Filter = " XML (*.xml)|*.xml| All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() != true)
                return;

            string path = dialog.FileName;
            saveThread = new Thread(() => SerializeObjectToXML(path));
            saveThread.Start();
        }

        //SAVE AS PNG IMAGE
        public void saveScreen(Canvas screen)
        {
           new SavePngCommand(screen);
        }

        private void CreateSaveBitmap(Canvas canvas, string filename)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
             (int)canvas.Width, (int)canvas.Height,
             96d, 96d, PixelFormats.Pbgra32);
            // needed otherwise the image output is black
            canvas.Measure(new Size((int)canvas.Width, (int)canvas.Height));
            canvas.Arrange(new Rect(new Size((int)canvas.Width, (int)canvas.Height)));

            renderBitmap.Render(canvas);

            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream file = File.Create(filename))
            {
                encoder.Save(file);
            }
        }
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindParent<T>(parentObject);
            }
        }
        //Add new arrow


        public void AddEdge()
        {
            //StatusBar = "Adding edge, press at the start node";
            isAddingEdge = true;
            //FocusedClass = null;
            //FocusedEdge = null;
        }
        public void AddAGG()
        {
            type = EdgeType.AGG;
            AddEdge();
        }

        public void AddASS()
        {
            type = EdgeType.ASS;
            AddEdge();
        }

        public void AddCOM()
        {
            type = EdgeType.COM;
            AddEdge();
        }

        public void AddDEP()
        {
            type = EdgeType.DEP;
            AddEdge();
        }

        public void AddGEN()
        {
            type = EdgeType.GEN;
            AddEdge();
        }

        public void AddNOR()
        {
            type = EdgeType.NOR;
            AddEdge();
        }

        //ADD NEW CLASS
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

		public void Undo() {
			undoRedoController.Undo();
		}

		public void Redo() {
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

		//////////////////Mouse actions//////////////////////////////////



 public void MouseDownEdge(MouseButtonEventArgs e)
        {
            if (!isAddingEdge)
            {
                if (ClassBoxes.Count == 1)
                {
                    ClassBoxes.ElementAt(0).IsSelected = false;
                    ClassBoxes.Clear();
                }
                e.MouseDevice.Target.CaptureMouse();
                FrameworkElement edgeElement = (FrameworkElement)e.MouseDevice.Target;
                //Edge edge = (Edge)edgeElement.DataContext;
                //edge.IsSelected = true;
                //if (selectedArrow != null)
                //{
                //    selectedArrow.IsSelected = false;
                //}
                //selectedArrow = edge;
            }
        }
        public void MouseUpEdge(MouseButtonEventArgs e)
        {
            e.MouseDevice.Target.ReleaseMouseCapture();
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

                //if (SelectedClassBox.Count == 0)
                //{
                //    SelectedClassBox.Add(movingClassBox);
                //}
                //else
                //{
                //    SelectedClassBox.ElementAt(0).IsSelected = false;
                //    SelectedClassBox.Clear();
                //    SelectedClassBox.Add(movingClassBox);
                //}
                //movingClassBox.IsSelected = true;
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
        private static T FindParentOfType<T>(DependencyObject o) where T: DependencyObject
        {

            DependencyObject parent = VisualTreeHelper.GetParent(o);
            if (parent == null) return null;
            return parent.GetType().IsAssignableFrom(typeof(T)) ? (T)parent : FindParentOfType<T>(parent);
        }
    }
}
