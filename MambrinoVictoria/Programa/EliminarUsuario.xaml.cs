using MambrinoVictoria.BaseDeDatos;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para EliminarUsuario.xaml
    /// </summary>
    public partial class EliminarUsuario : Window
    {
        BDD baseDeDatos;

        /// <summary>
        /// Constructor que inicializa la ventana de eliminacion de usuario
        /// </summary>
        public EliminarUsuario()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            baseDeDatos = BDD.InstanciaBDD();

            List<string> Roles = baseDeDatos.ObtenerRoles();
            perfil.ItemsSource = Roles;

        }

        /// <summary>
        /// Maneja el evento de cambio de seleccion del comboBox perfil
        /// Carga los nombres de usuarios asociados al perfil seleccionado en el comboBox de usuario
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void perfil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> Usuarios = baseDeDatos.ObtenerNombresUsuarios(perfil.SelectedIndex + 1);
            usuario.ItemsSource = Usuarios;
        }

        /// <summary>
        /// Maneja el evento de clic en el boton eliminar
        /// Elimina el usuario seleccionado y muestra un mensaje de confirmacion
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (usuario.SelectedIndex != -1)
            {
                baseDeDatos.EliminarUsuario(usuario.SelectedItem.ToString());
                this.Close();
            }
            else
            {
                MessageBox.Show("Selecciona un usuario para eliminarlo");
            }
        }
    }
}
