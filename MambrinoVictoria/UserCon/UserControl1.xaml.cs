using MambrinoVictoria.Programa;
using System;
using System.Windows;
using System.Windows.Controls;


namespace MambrinoVictoria.UserCon
{
    /// <summary>
    /// Lógica de interacción para UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {

        /// <summary>
        /// Delegado que representa una accion con un parametro de tipo Type
        /// </summary>
        /// <param name="tipo">El tipo de dato utilizado como parametro</param>
        public delegate void Action(Type tipo);

        /// <summary>
        /// Evento que se dispara cuando se realiza un clic.
        /// </summary>
        public event Action<Type> Clic;

        /// <summary>
        /// Constructor de la clase UserControl1
        /// </summary>
        public UserControl1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Manejador del evento Click del boton "Aceptar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            Clic.Invoke(typeof(Acceso));
        }
    }
}
