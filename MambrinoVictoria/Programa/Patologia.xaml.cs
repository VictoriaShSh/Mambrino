using MambrinoVictoria.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para Patologia.xaml
    /// </summary>
    public partial class Patologia : Window
    {
        BDD baseDeDatos;
        int nhc;

        /// <summary>
        /// Constructor que inicializa la ventana de patologia, establece la posicion de la veata, inicializa la base de datos, establece la fecha y rellena los comboBox
        /// </summary>
        /// <param name="nhcPaSel">Numero de historia clinica del paciente seleccionado</param>
        public Patologia(int nhcPaSel)
        {
            InitializeComponent();

            baseDeDatos = BDD.InstanciaBDD();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            fechaDiagnostico.Text = DateTime.Now.ToString("dd-MM-yyyy");

            List<string> e = new List<string> { " ", "Medicina", "Enfermeria", "Cirugia" };
            especialidad.ItemsSource = e;

            List<string> c = new List<string> { " ", "No", "Si" };
            codificacion.ItemsSource = c;

            nhc = nhcPaSel;
        }

        /// <summary>
        /// Maneja el clic en el boton "Aceptar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(fechaSintomas.Text, out DateTime fechaSin) && DateTime.TryParse(fechaDiagnostico.Text, out DateTime fechaDiag))
            {
                baseDeDatos.RegistrarPatologia(nhc, fechaSin, fechaDiag, sintomas.Text, diagnostico.Text, especialidad.Text, codificacion.Text);
                this.Close();
            }
        }
    }
}
