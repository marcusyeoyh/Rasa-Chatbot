﻿#pragma checksum "..\..\..\..\View\Chatbot.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2987ACE8153F8F567E128E19A94FC4BD7595186A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace BaseWPFApp.View {
    
    
    /// <summary>
    /// Chatbot
    /// </summary>
    public partial class Chatbot : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 64 "..\..\..\..\View\Chatbot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer ResultsScroller;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\View\Chatbot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ResultsPanel;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\View\Chatbot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtInput;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\View\Chatbot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGo;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BaseWPFApp;V1.0.0.0;component/view/chatbot.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Chatbot.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ResultsScroller = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 2:
            this.ResultsPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.txtInput = ((System.Windows.Controls.TextBox)(target));
            
            #line 70 "..\..\..\..\View\Chatbot.xaml"
            this.txtInput.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TxtInput_PreviewMouseDown);
            
            #line default
            #line hidden
            
            #line 70 "..\..\..\..\View\Chatbot.xaml"
            this.txtInput.GotFocus += new System.Windows.RoutedEventHandler(this.TxtInput_GotFocus);
            
            #line default
            #line hidden
            
            #line 70 "..\..\..\..\View\Chatbot.xaml"
            this.txtInput.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.TxtInput_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnGo = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\..\..\View\Chatbot.xaml"
            this.btnGo.Click += new System.Windows.RoutedEventHandler(this.BtnGo_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

