using MambrinoVictoria.BaseDeDatos;
using System;
using System.Windows;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para ModificarAdmin.xaml
    /// </summary>
    public partial class ModificarAdmin : Window
    {
        BDD baseDeDatos;

        string us;
        string con;

        /// <summary>
        /// Constructor que inicializa la ventana de Modificar Admin
        /// </summary>
        public ModificarAdmin()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            baseDeDatos = BDD.InstanciaBDD();
        }

        /// <summary>
        /// Manejador del evento Click del boton "Aceptar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            us = usuario.Text;
            con = contraseña.Text;

            if (string.IsNullOrWhiteSpace(us) || string.IsNullOrWhiteSpace(con))
            {
                MessageBox.Show("Por favor rellena todos los campos");
                return;
            }

            try
            {
                BDD baseDeDatos = BDD.InstanciaBDD();
                baseDeDatos.ModificarUsuarioAdmin(us, con);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
