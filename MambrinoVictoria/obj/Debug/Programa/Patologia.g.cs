﻿#pragma checksum "..\..\..\Programa\Patologia.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C617158630580F497AA29E39257D4AFE95AA62A40B7BB16C81488DB6E0B22D64"
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
    /// Patologia
    /// </summary>
    public partial class Patologia : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 57 "..\..\..\Programa\Patologia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox fechaSintomas;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Programa\Patologia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox fechaDiagnostico;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Programa\Patologia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox sintomas;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\Programa\Patologia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox diagnostico;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Programa\Patologia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox especialidad;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Programa\Patologia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox codificacion;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Programa\Patologia.xaml"
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
            System.Uri resourceLocater = new System.Uri("/MambrinoVictoria;component/programa/patologia.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Programa\Patologia.xaml"
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
            this.fechaSintomas = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.fechaDiagnostico = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.sintomas = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.diagnostico = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.especialidad = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.codificacion = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.aceptar = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\..\Programa\Patologia.xaml"
            this.aceptar.Click += new System.Windows.RoutedEventHandler(this.aceptar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
