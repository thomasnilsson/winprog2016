using GalaSoft.MvvmLight;
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
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Class> Classes{ get; set; }

        public ICommand AddClassCommand { get; private set; }

        public MainViewModel()
        {
            Classes = new ObservableCollection<Class>();
            AddClassCommand = new RelayCommand(AddClassBox);


        }
        public void AddClassBox()
        {
            //undoRedoController.AddAndExecute(new AddClassCommand(ClassBoxs));
        }
    }
}