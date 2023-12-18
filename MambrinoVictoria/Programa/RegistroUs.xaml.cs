using MambrinoVictoria.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para RegistroUs.xaml
    /// </summary>
    public partial class RegistroUs : Window
    {
        BDD baseDeDatos;

        string nom;
        string apes;
        string em;
        int per;
        string con;
        int cent;

        /// <summary>
        /// Constructor que inicializa la ventana de registro de usuario, establece la posicion de la veata, inicializa la base de datos y obtiene los roles
        /// </summary>
        public RegistroUs()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            baseDeDatos = BDD.InstanciaBDD();

            List<string> Roles = baseDeDatos.ObtenerRoles();
            perfil.ItemsSource = Roles;

            List<string> Centros = baseDeDatos.ObtenerNombresCentros();
            centro.ItemsSource = Centros;

        }

        /// <summary>
        /// Manejador del evento Click del boton "Aceptar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            nom = nombre.Text;
            apes = apellidos.Text;
            em = email.Text;
            per = perfil.SelectedIndex + 1;
            con = contraseña.Text;
            cent = centro.SelectedIndex + 1;

            if (per != 0 && cent != 0)
            {
                try
                {
                    BDD baseDeDatos = BDD.InstanciaBDD();
                    baseDeDatos.AgregarUsuario(nom, apes, em, per, con, cent);

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(apes) || string.IsNullOrWhiteSpace(con))
                {
                    MessageBox.Show("Por favor, rellena todos los campos");
                    return;
                }
            }
        }
    }
}

