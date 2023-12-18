using MambrinoVictoria.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interaccion para Hospitalizacion.xaml
    /// </summary>
    public partial class Hospitalizacion : Window
    {
        BDD baseDeDatos;
        int idCama;

        /// <summary>
        /// Constructor que inicializa la ventana de hospitalizacion
        /// </summary>
        /// <param name="refCama">Referencia de la cama asociada a la hospitalizacion</param>
        /// <param name="idCentro">Referencia del centro asociado a la hospitalizacion</param>
        public Hospitalizacion(string refCama, int idCentro)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            baseDeDatos = BDD.InstanciaBDD();

            fecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
            hora.Text = DateTime.Now.ToString("HH:mm");

            List<string> ambitos = new List<string> { " ", "Hospitalización", "Urgencias", "Consultas" };
            ambito.ItemsSource = ambitos;

            List<string> estados = new List<string> { " ", "Provisional" };
            estado.ItemsSource = estados;

            cama.Text = refCama;
            idCama = baseDeDatos.ObtenerIdCama(refCama, idCentro);
        }

        /// <summary>
        /// Maneja el evento de clic en el boton "Aceptar" para realizar la hospitalizacion
        /// </summary>
        /// <param name="sender">El objeto que desencaden el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (nif.Text == null)
            {
                string nifPaciente = nif.Text;
                int nhcPaciente = baseDeDatos.ObtenerNHCporNIF(nifPaciente);
                if (baseDeDatos.PacienteIngresadoEnCama(nhcPaciente))
                {
                    MessageBox.Show("El paciente ya esta ingresado en otra cama", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (baseDeDatos.VerificarExistenciaNIF(nifPaciente))
                    {
                        if (DateTime.TryParse(fecha.Text, out DateTime fechaIngreso) && TimeSpan.TryParse(hora.Text, out TimeSpan horaIngreso))
                        {
                            baseDeDatos.IngresarPaciente(estado.Text, ambito.SelectedIndex, fechaIngreso, horaIngreso, idCama, nhcPaciente);
                            MessageBox.Show("Ingreso realizado correctamente");

                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se ha encontrado ningun paciente con el NIF introducido", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                int nhcPaciente = int.Parse(nhc.Text);
                if (baseDeDatos.PacienteIngresadoEnCama(nhcPaciente))
                {
                    MessageBox.Show("El paciente ya esta ingresado en otra cama", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    string nifPaciente = baseDeDatos.ObtenerNIFporHNC(nhcPaciente);

                    if (baseDeDatos.VerificarExistenciaNIF(nifPaciente))
                    {
                        if (DateTime.TryParse(fecha.Text, out DateTime fechaIngreso) && TimeSpan.TryParse(hora.Text, out TimeSpan horaIngreso))
                        {
                            baseDeDatos.IngresarPaciente(estado.Text, ambito.SelectedIndex, fechaIngreso, horaIngreso, idCama, nhcPaciente);
                            MessageBox.Show("Ingreso realizado correctamente");

                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se ha encontrado ningun paciente con el NHC introducido", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }
    }
}
