<<<<<<< HEAD
﻿#pragma checksum "..\..\..\View\ClassUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5BCF18D73C5A32D5FD2ED33FB9B223BC"
=======
﻿#pragma checksum "..\..\..\View\ClassUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "57E630FEA219C8D96324CF44E29D0798"
>>>>>>> origin/master
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GalaSoft.MvvmLight.Command;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Diagram_WinProg2016.View {
    
    
    /// <summary>
    /// ClassUserControl
    /// </summary>
    public partial class ClassUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\View\ClassUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border borderBox;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\View\ClassUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ClassBox;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\View\ClassUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border mask;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\View\ClassUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton SelectedButton;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\View\ClassUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameContainer;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\View\ClassUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FieldContainer;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\View\ClassUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MethodContainer;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Diagram_WinProg2016;component/view/classusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\ClassUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.borderBox = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.ClassBox = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.mask = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.SelectedButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 5:
            this.NameContainer = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.FieldContainer = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.MethodContainer = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

