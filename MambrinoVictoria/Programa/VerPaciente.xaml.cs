using MambrinoVictoria.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para VerPaciente.xaml
    /// </summary>
    public partial class VerPaciente : Window
    {
        BDD baseDeDatos;

        /// <summary>
        /// Constructor de la clase que inicializa la ventana para ver la informacion del paciente
        /// </summary>
        /// <param name="paciente">Lista con la informacion del paciente</param>
        public VerPaciente(List<string> paciente)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            baseDeDatos = BDD.InstanciaBDD();

            CargarDatosPaciente(paciente);
        }

        /// <summary>
        /// Metodo para cargar los datos del paciente en los campos de la ventana
        /// </summary>
        /// <param name="paciente">Lista con la informacion del paciente</param>
        public void CargarDatosPaciente(List<string> paciente)
        {
            List<TextBox> textBoxes = new List<TextBox>
            {
                nhc,
                nif,
                numeross,
                nombre,
                apellido1,
                apellido2,
                sexo,
                fechaNacimiento,
                telefono1,
                telefono2,
                movil,
                estadoCivil,
                estudios,
                fallecido,
                nacionalidad,
                cAutonoma,
                provincia,
                poblacion,
                cp,
                direccion,
                esTitularNombre,
                regimen,
                tis,
                medicoPrimaria,
                cap,
                zonaBasicaSalud,
                areaSalud,
                nacionalidadNac,
                cAutonomaNac,
                provinciaNac,
                poblacionNac
            };

            for (int i = 0; i < paciente.Count && i < textBoxes.Count; i++)
            {
                textBoxes[i].Text = paciente[i];
            }
        }

        /// <summary>
        /// Manejador del evento Click del boton "Aceptar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Manejador del evento Click del boton "Modificar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void modificar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nif.Text))
            {
                string telefono1 = string.IsNullOrEmpty(this.telefono1.Text) ? null : this.telefono1.Text;
                string telefono2 = string.IsNullOrEmpty(this.telefono2.Text) ? null : this.telefono2.Text;
                string movil = string.IsNullOrEmpty(this.movil.Text) ? null : this.movil.Text;
                string estadoCivil = string.IsNullOrEmpty(this.estadoCivil.Text) ? null : this.estadoCivil.Text;
                string estudios = string.IsNullOrEmpty(this.estudios.Text) ? null : this.estudios.Text;
                string fallecido = string.IsNullOrEmpty(this.fallecido.Text) ? null : this.fallecido.Text;
                string cAutonoma = string.IsNullOrEmpty(this.cAutonoma.Text) ? null : this.cAutonoma.Text;
                string provincia = string.IsNullOrEmpty(this.provincia.Text) ? null : this.provincia.Text;
                string poblacion = string.IsNullOrEmpty(this.poblacion.Text) ? null : this.poblacion.Text;
                string direccion = string.IsNullOrEmpty(this.direccion.Text) ? null : this.direccion.Text;
                string codigoPostal = string.IsNullOrEmpty(cp.Text) ? null : cp.Text;

                baseDeDatos.ModificarPaciente(nif.Text, telefono1, telefono2, movil, estadoCivil, estudios, fallecido, cAutonoma, provincia, poblacion, direccion, codigoPostal);

                Console.WriteLine("Modificacion realizada correctamente");
            }
        }
    }
}
