﻿#pragma checksum "..\..\..\Programa\Hospitalizacion.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "62505C87E7F2254D7C12804EF9F0E939EF012FC402A97225F32B5F1277E2D58D"
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
    /// Hospitalizacion
    /// </summary>
    public partial class Hospitalizacion : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 54 "..\..\..\Programa\Hospitalizacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nhc;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Programa\Hospitalizacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nif;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Programa\Hospitalizacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox fecha;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Programa\Hospitalizacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox hora;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\Programa\Hospitalizacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cama;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Programa\Hospitalizacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox estado;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\Programa\Hospitalizacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ambito;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\Programa\Hospitalizacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button aceptar;
        
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
            System.Uri resourceLocater = new System.Uri("/MambrinoVictoria;component/programa/hospitalizacion.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Programa\Hospitalizacion.xaml"
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
            this.nhc = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.nif = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.fecha = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.hora = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.cama = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.estado = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.ambito = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.aceptar = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\..\Programa\Hospitalizacion.xaml"
            this.aceptar.Click += new System.Windows.RoutedEventHandler(this.aceptar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

