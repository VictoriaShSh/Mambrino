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
        }

        /// <summary>
        /// Maneja el evento de carga de la ventana
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Manejador del evento Click del boton "Conectar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void Conectar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = Usuario.Text;
            string puerto = Puerto.Text;
            string servidor = Servidor.Text;
            string contraseña = Contraseña.Password;

            try
            {
                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(puerto) || string.IsNullOrWhiteSpace(servidor) || string.IsNullOrWhiteSpace(contraseña))
                {
                    MessageBox.Show("Por favor, rellena todos los campos");
                    return;
                }
                else
                {
                    baseDeDatos.Usuario = usuario;
                    baseDeDatos.Puerto = puerto;
                    baseDeDatos.Servidor = servidor;
                    baseDeDatos.Contraseña = contraseña;

                    if (baseDeDatos.Conexion())
                    {
                        Inicio inicio = new Inicio();
                        inicio.Show();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al establecer conexion");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear base de datos: " + ex.Message);
            }
        }
    }
}


