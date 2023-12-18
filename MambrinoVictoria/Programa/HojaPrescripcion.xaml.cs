using MambrinoVictoria.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para HojaPrescripcion.xaml
    /// </summary>
    public partial class HojaPrescripcion : Window
    {
        BDD baseDeDatos;
        int nhc;

        /// <summary>
        /// Constructor que inicializa la ventana de prescripcion medica
        /// </summary>
        /// <param name="nhcPaSel">Numero de historia clinica del paciente seleccionado</param>
        public HojaPrescripcion(int nhcPaSel)
        {
            InitializeComponent();

            baseDeDatos = BDD.InstanciaBDD();

            List<string> v = new List<string> { " ", "IV", "VO", "SC" };
            via.ItemsSource = v;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            nhc = nhcPaSel;
        }

        /// <summary>
        /// Manejador del evento Click del boton "Aceptar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(fechaFinTrat.Text, out DateTime fechaFin))
            {
                baseDeDatos.RegistrarHojaPrescripcion(nhc, especialidad.Text, principioActivo.Text, dosis.Text, via.Text, frecuencia.Text, fechaFin);
                this.Close();
            }
        }
    }
}
