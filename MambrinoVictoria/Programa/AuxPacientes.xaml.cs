using MambrinoVictoria.BaseDeDatos;
using System.Collections.Generic;
using System.Windows;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para AuxPacientes.xaml
    /// </summary>
    public partial class AuxPacientes : Window
    {
        BDD baseDeDatos;

        /// <summary>
        /// Constructor que inicializa la ventana auxiliar de pacientes
        /// </summary>
        /// <param name="listaPacientes">Lista de nombres de pacientes</param>
        public AuxPacientes(List<string> listaPacientes)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            baseDeDatos = BDD.InstanciaBDD();

            if (listaPacientes != null)
            {
                foreach (var paciente in listaPacientes)
                {
                    this.pacientes.Items.Add(paciente);
                }
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el boton de seleccion mostrando la informacion detallada del paciente seleccionado
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void seleccionar_Click(object sender, RoutedEventArgs e)
        {
            if (pacientes.SelectedItem != null)
            {
                string pacienteSeleccionado = pacientes.SelectedItem.ToString();

                string nif = baseDeDatos.ObtenerNIFPaciente(pacienteSeleccionado);
                int nhc = baseDeDatos.ObtenerNHCporNIF(nif);

                List<string> pacienteInfo = baseDeDatos.MostrarPaciente(nhc);

                VerPaciente verPaciente = new VerPaciente(pacienteInfo);
                verPaciente.Show();
                this.Close();
            }
        }
    }
}
