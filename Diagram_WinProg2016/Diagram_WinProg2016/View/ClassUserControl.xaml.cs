using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Diagram_WinProg2016
{
	/// <summary>
	/// Interaction logic for ClassUserControl.xaml
	/// </summary>
	public partial class ClassUserControl : UserControl
    {
        protected bool isDragging;
        private Point clickPosition;

        public ClassUserControl()
        {
            InitializeComponent();
			DataContext = new ViewModel.ClassViewModel();

			MouseLeftButtonDown += new MouseButtonEventHandler(Control_MouseLeftButtonDown);
			MouseLeftButtonUp += new MouseButtonEventHandler(Control_MouseLeftButtonUp);
			MouseMove += new MouseEventHandler(Control_MouseMove);
        }

        private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            var draggableControl = sender as UserControl;
            clickPosition = e.GetPosition(this);
            draggableControl.CaptureMouse();
        }

        private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            var draggable = sender as UserControl;
            draggable.ReleaseMouseCapture();
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            var draggableControl = sender as UserControl;

            if (isDragging && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(this.Parent as UIElement);

                var transform = draggableControl.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggableControl.RenderTransform = transform;
                }

                transform.X = currentPosition.X - clickPosition.X;
                transform.Y = currentPosition.Y - clickPosition.Y;
            }
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Grid).Children.Remove(this);
        }
    }
    
}
