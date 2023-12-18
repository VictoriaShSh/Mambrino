using MambrinoVictoria.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para GestionAlertas.xaml
    /// </summary>
    public partial class GestionAlertas : Window
    {
        BDD baseDeDatos;
        int nhc;

        /// <summary>
        /// Constructor que inicializa la ventana de gestion de alertas
        /// </summary>
        /// <param name="nhc">Identificador de la cama asociada a la alerta</param>
        public GestionAlertas(int nhc)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            baseDeDatos = BDD.InstanciaBDD();

            this.nhc = nhc;

            fecha.Text = DateTime.Now.ToString("dd-MM-yyyy");

            if (baseDeDatos.ObtenerAlertaPaciente(nhc))
            {
                CargarInformacionAlerta();
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el botón "Aceptar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParseExact(fecha.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaAlerta))
            {
                baseDeDatos.CrearAlerta(descripcion.Text, observaciones.Text, fechaAlerta, nhc);

                this.Close();
            }
            else
            {
                MessageBox.Show("Formato fecha invalido");
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el boton "Modificar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void modificar_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParseExact(fecha.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaAlerta))
            {
                baseDeDatos.ActualizarAlerta(descripcion.Text, observaciones.Text, fechaAlerta, nhc);

                this.Close();
            }
            else
            {
                MessageBox.Show("Formato de fecha invalido");
            }
        }

        /// <summary>
        /// Metodo que carga la informacion de la alerta en los campos correspondientes
        /// </summary>
        private void CargarInformacionAlerta()
        {
            List<string> alertaInfo = baseDeDatos.MostrarAlerta(nhc);

            if (alertaInfo.Count > 0)
            {
                string[] info = alertaInfo[0].Split(',');

                if (info.Length == 2)
                {
                    string descripcion = info[0]?.Split(':')[1]?.Trim() ?? string.Empty;
                    string observaciones = info[1]?.Split(':')[1]?.Trim() ?? string.Empty;

                    this.descripcion.Text = descripcion;
                    this.observaciones.Text = observaciones;
                }
            }
        }
    }
}
