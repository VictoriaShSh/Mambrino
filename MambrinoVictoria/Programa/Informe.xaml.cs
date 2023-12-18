using MambrinoVictoria.BaseDeDatos;
using System.Collections.Generic;
using System.Windows;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para Informe.xaml
    /// </summary>
    public partial class Informe : Window
    {
        BDD baseDeDatos;
        int nhc;

        /// <summary>
        /// Constructor que inicializa la ventana de Informe, a la base de datos, establece la posicion inicial de la venta y  rellena los listBox
        /// </summary>
        /// <param name="nhcPaSel">Numero de historia clinica del paciente seleccionado</param>
        public Informe(int nhcPaSel)
        {
            InitializeComponent();
            baseDeDatos = BDD.InstanciaBDD();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            nhc = nhcPaSel;

            List<List<string>> itemsIn = baseDeDatos.ObtenerIngresosPorPaciente(nhcPaSel);
            listaIngresos.ItemsSource = itemsIn;

            List<List<string>> itemsNot = baseDeDatos.ObtenerNotasEnfermeriaPorPaciente(nhcPaSel);
            listaNotas.ItemsSource = itemsNot;

            List<List<string>> itemsBal = baseDeDatos.ObtenerBalancesHidricosPorPaciente(nhcPaSel);
            listaBalances.ItemsSource = itemsBal;

            List<List<string>> itemsAlt = baseDeDatos.ObtenerAltasPorPaciente(nhcPaSel);
            listaAltas.ItemsSource = itemsAlt;
        }

        /// <summary>
        /// Manejador del evento Click del boton "Aceptar"
        /// <summary>
        /// <param name = "sender" > El objeto que desencaden el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
