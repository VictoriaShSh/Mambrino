using MambrinoVictoria.BaseDeDatos;
using MambrinoVictoria.Paginas;
using System;
using System.Windows;

namespace MambrinoVictoria
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BDD baseDeDatos;

        /// <summary>
        /// Constructor de la clase MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            baseDeDatos = BDD.InstanciaBDD();

            // Asignar datos de conexión automáticamente
            baseDeDatos.Usuario = "root";
            baseDeDatos.Puerto = "3306";
            baseDeDatos.Servidor = "localhost";
            baseDeDatos.Contraseña = "1234";

            // Intentar conectar a la base de datos directamente
            ConectarBaseDeDatos();
        }

        /// <summary>
        /// Intenta conectar a la base de datos con los datos predefinidos.
        /// </summary>
        private void ConectarBaseDeDatos()
        {
            try
            {
                if (baseDeDatos.Conexion())
                {
                    Inicio inicio = new Inicio();
                    inicio.Show();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al establecer conexión");
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear base de datos: " + ex.Message);
                Application.Current.Shutdown();
            }
        }
    }
}


