using System.Windows;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para Acceso.xaml
    /// </summary>
    public partial class Acceso : Window
    {
        string u;
        string c;

        /// <summary>
        /// Constructor que inicia la ventana de acceso
        /// </summary>
        /// <param name="usuario">Nombre de usuario</param>
        /// <param name="clave">Contraseña de acceso</param>
        public Acceso(string usuario, string clave)
        {
            InitializeComponent();
            u = usuario;
            c = clave;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Maneja el evento de clic en el boton "aceptar"
        /// </summary>
        /// <param name="sender">Objeto que activa el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (ambito.SelectedIndex == 0 && idioma.SelectedIndex == 0)
            {
                Principal principal = new Principal(u, c);
                principal.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Ambito no disponible en este momento");
            }
        }
    }
}
