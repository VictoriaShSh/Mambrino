﻿#pragma checksum "..\..\..\Programa\GestionAlertas.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "AC837BD0D21CCB312F5E2739F0269D412C12020EB6009A1D192EAF5287AC2E0C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MambrinoVictoria.Programa;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace MambrinoVictoria.Programa {
    
    
    /// <summary>
    /// GestionAlertas
    /// </summary>
    public partial class GestionAlertas : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\..\Programa\GestionAlertas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descripcion;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Programa\GestionAlertas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox fecha;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Programa\GestionAlertas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox observaciones;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Programa\GestionAlertas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button aceptar;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Programa\GestionAlertas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button modificar;
        
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
            System.Uri resourceLocater = new System.Uri("/MambrinoVictoria;component/programa/gestionalertas.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Programa\GestionAlertas.xaml"
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
            this.descripcion = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.fecha = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.observaciones = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.aceptar = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\Programa\GestionAlertas.xaml"
            this.aceptar.Click += new System.Windows.RoutedEventHandler(this.aceptar_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.modificar = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\Programa\GestionAlertas.xaml"
            this.modificar.Click += new System.Windows.RoutedEventHandler(this.modificar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

