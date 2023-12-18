using MambrinoVictoria.BaseDeDatos;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para BusquedaPaciente.xaml
    /// </summary>
    public partial class BusquedaPaciente : Window
    {
        BDD baseDeDatos;

        /// <summary>
        /// Constructor que inicializa la ventana de busqueda de pacientes
        /// </summary>
        public BusquedaPaciente()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            baseDeDatos = BDD.InstanciaBDD();

            areaSalud.ItemsSource = baseDeDatos.ObtenerAreaSalud();
        }

        /// <summary>
        /// Maneja el evento de cambio de seleccion de area de salud reiniciando los campos de NHC y NIF
        /// y actualizando las opciones de zona basica segun el area de salud seleccionada
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void areaSalud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nhc.Text = null;
            nif.Text = null;

            if (areaSalud.SelectedItem != null)
            {
                string areaSeleccionada = areaSalud.SelectedItem.ToString();

                List<string> zonasBasicas = baseDeDatos.ObtenerZonaBasicaporArea(areaSeleccionada);

                zonaBasica.ItemsSource = zonasBasicas;
            }
        }

        /// <summary>
        /// Maneja el evento de clic del boton buscar y mostran informacion detallada del paciente segun los campos proporcionados.
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void buscar_Click(object sender, RoutedEventArgs e)
        {
            List<string> paciente;

            if (!string.IsNullOrEmpty(nif.Text))
            {
                int nhcPciente = baseDeDatos.ObtenerNHCporNIF(nif.Text);

                paciente = baseDeDatos.MostrarPaciente(nhcPciente);
                VerPaciente verPaciente = new VerPaciente(paciente);
                verPaciente.Show();

                this.Close();
            }

            else if (!string.IsNullOrEmpty(nhc.Text))
            {
                int nhcP = int.Parse(nhc.Text);
                paciente = baseDeDatos.MostrarPaciente(nhcP);
                VerPaciente verPaciente = new VerPaciente(paciente);
                verPaciente.Show();
                this.Close();
            }

            else if (!string.IsNullOrEmpty(zonaBasica.Text))
            {
                paciente = baseDeDatos.BuscarPacientesPorZonaBasica(zonaBasica.Text);
                AuxPacientes aux = new AuxPacientes(paciente);
                aux.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Maneja el cambio de texto en el campo NIF reiniciando el campo NHC
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void nif_TextChanged(object sender, TextChangedEventArgs e)
        {
            nhc.Text = null;
        }

        /// <summary>
        /// Maneja el cambio de texto en el campo NHC reiniciando el campo NIF
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void nhc_TextChanged(object sender, TextChangedEventArgs e)
        {
            nif.Text = null;
        }
    }
}


