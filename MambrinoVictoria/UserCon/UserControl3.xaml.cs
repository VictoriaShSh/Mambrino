using MambrinoVictoria.Programa;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MambrinoVictoria.UserCon
{
    /// <summary>
    /// Lógica de interacción para UserControl3.xaml
    /// </summary>
    public partial class UserControl3 : UserControl
    {
        /// <summary>
        /// Delegado que representa una accion con un parametro de tipo Type
        /// </summary>
        /// <param name="tipo">El tipo de dato utilizado como parametro</param>
        public delegate void ActionType(Type tipo);

        /// <summary>
        /// Evento que se dispara cuando se realiza un clic en el boton Añadir
        /// </summary>
        public event Action<Type> AñadirClick;

        /// <summary>
        /// Evento que se dispara cuando se realiza un clic en el boton Cancelar
        /// </summary>
        public event Action<Type> CancelarClick;

        /// <summary>
        /// Constructor de la clase UserControl3
        /// </summary>
        public UserControl3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Establece los elementos del ListBox con la lista de listas proporcionad
        /// </summary>
        /// <param name="items">Lista de listas para agregar al ListBox</param>
        public void SetListBoxItems(List<List<string>> items)
        {
            lista.Items.Clear();

            foreach (var item in items)
            {
                string formattedItem = string.Join(", ", item);
                lista.Items.Add(formattedItem);
            }
        }

        /// <summary>
        /// Manejador del evento Click del boton "Añadir"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void añadir_Click(object sender, RoutedEventArgs e)
        {
            if (AñadirClick != null)
            {
                AñadirClick.Invoke(typeof(Patologia));
            }
        }

        /// <summary>
        /// Manejador del evento Click del boton "Cancelar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            if (CancelarClick != null)
            {
                CancelarClick.Invoke(typeof(UserControl2));
            }
        }
    }
}
