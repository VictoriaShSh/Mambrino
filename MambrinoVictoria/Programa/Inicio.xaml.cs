using MambrinoVictoria.BaseDeDatos;
using MambrinoVictoria.Programa;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MambrinoVictoria.Paginas
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        UserControl UsCon1;
        BDD baseDeDatos;

        string usuario;
        string clave;

        /// <summary>
        /// Constructor que inicializa la ventana de inicio
        /// </summary>
        public Inicio()
        {
            InitializeComponent();

            // Asigna el UserControl uc1 a UsCon1 y suscribe el evento Clic
            UsCon1 = new UserControl();
            UsCon1 = uc1;
            uc1.Clic += uc1_Acceso;

            baseDeDatos = BDD.InstanciaBDD();
        }

        /// <summary>
        /// Maneja el evento de carga de la ventana
        /// Maximiza la ventana actual al cargarla
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Manejador del evento Clic del boton "Aceptar" del "UserControl1"
        /// </summary>
        /// <param name="tipoPagina">Tipo de la pagina que se debe mostrar</param>
        private void uc1_Acceso(Type tipoPagina)
        {
            if (tipoPagina == typeof(Acceso) && uc1 != null)
            {
                usuario = uc1.usu.Text;
                clave = uc1.clav.Text;

                if (baseDeDatos.IniciarSesion(usuario, clave))
                {
                    Acceso acceso = new Acceso(usuario, clave);
                    acceso.ShowDialog();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña incorrectos");
                }
            }
        }

        /// <summary>
        /// Manejador del evento Click del boton "Ayuda"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Introduce el Usuario y la Contraseña, o pulsa en Registrar");
        }

        /// <summary>
        /// Manejador del evento Click del boton "Registro"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void registro_Click(object sender, RoutedEventArgs e)
        {
            RegistroUs registro = new RegistroUs();
            registro.Show();
        }
    }
}
