﻿#pragma checksum "..\..\..\..\View\WindowPerson.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "352D0A2036004D6BDC4ABF7101571C6F5536B213"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
using ecl.View;


namespace ecl.View {
    
    
    /// <summary>
    /// WindowPerson
    /// </summary>
    public partial class WindowPerson : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\View\WindowPerson.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbShifer;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\View\WindowPerson.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbInn;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\View\WindowPerson.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbType;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\View\WindowPerson.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker tbData;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\View\WindowPerson.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btOK;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\View\WindowPerson.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btConcel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ecl;component/view/windowperson.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\WindowPerson.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tbShifer = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.tbInn = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.tbType = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbData = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.btOK = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\..\View\WindowPerson.xaml"
            this.btOK.Click += new System.Windows.RoutedEventHandler(this.btOK_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btConcel = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\View\WindowPerson.xaml"
            this.btConcel.Click += new System.Windows.RoutedEventHandler(this.btConcel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
