using MambrinoVictoria.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para AltaPaciente.xaml
    /// </summary>

    public partial class AltaPaciente : Window
    {
        BDD baseDeDatos;
        int idCama;
        int nhcP;

        /// <summary>
        /// Constructor que inicializa la ventana de alta de paciente
        /// </summary>
        /// <param name="refCama">Referencia de la cama</param>
        /// <param name="idCentro">Referencia del centro</param>
        public AltaPaciente(string refCama, int idCentro)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            baseDeDatos = BDD.InstanciaBDD();

            idCama = baseDeDatos.ObtenerIdCama(refCama, idCentro);
            nhcP = baseDeDatos.ObtenerNHCporIdCama(idCama);

            nhc.Text = nhcP.ToString();

            cama.Text = refCama;

            fecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
            hora.Text = DateTime.Now.ToString("HH:mm");

            List<string> Motivo = new List<string> { " ", "Alta Voluntaria", "Domicilio (fin de cuidados)", "Exitus", "exitus por gripe H1N1", "fuga",
                                                    "Translado a otro centro sanitario", "Translado a centro socio-sanitario", "otros" };
            motivo.ItemsSource = Motivo;

            List<string> Tipo = new List<string> { " ", "Hospitalización", "Urgencias" };
            tipo.ItemsSource = Tipo;
        }

        /// <summary>
        /// Maneja el evento de clic en el botón "aceptar"
        /// </summary>
        /// <param name="sender">Objeto que desencadenó el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaAlta = DateTime.Now;
            TimeSpan horaAlta = DateTime.Now.TimeOfDay;

            baseDeDatos.DarAltaPaciente(idCama, fechaAlta, horaAlta, motivo.Text, tipo.Text, nhcP);

            MessageBox.Show("Paciente dado de alta correctamente");
            this.Close();
        }
    }
}
