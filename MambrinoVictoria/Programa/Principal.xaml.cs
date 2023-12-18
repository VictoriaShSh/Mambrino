using MambrinoVictoria.BaseDeDatos;
using MambrinoVictoria.BaseDeDatos;
using MambrinoVictoria.Paginas;
using MambrinoVictoria.UserCon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interaccion para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        BDD baseDeDatos;

        string refCama;
        int nhcPaSel;
        string rutaImagen;
        int idCentro;
        int idCama;
        string usuario;
        string clave;

        /// <summary>
        /// Constructor que inicializa la ventana principal
        /// </summary>
        /// <param name="u">Nombre de usuario</param>
        /// <param name="c">Clave de acceso</param>
        public Principal(string u, string c)
        {
            InitializeComponent();

            baseDeDatos = BDD.InstanciaBDD();

            usuario = u;
            clave = c;
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

            List<string> Hospitales = baseDeDatos.ObtenerNombresCentros();
            hospital.ItemsSource = Hospitales;

            if (usuario != null && clave != null)
            {
                if (!baseDeDatos.EsAdministrador(usuario, clave))
                {
                    admin.Visibility = Visibility.Hidden;
                }
                else
                {
                    admin.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Maneja el clic en el boton "Inicio"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void inicio_Click(object sender, RoutedEventArgs e)
        {
            Inicio inicio = new Inicio();
            inicio.Show();
            this.Close();
        }

        /// <summary>
        /// Maneja el clic en el boton "Restablecer Base de Datos"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void restablecer_Click(object sender, RoutedEventArgs e)
        {
            baseDeDatos.RestablecerBDD();
        }

        /// <summary>
        /// Maneja el clic en el boton "Modificar Administrador"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void modificar_Click(object sender, RoutedEventArgs e)
        {
            ModificarAdmin mod = new ModificarAdmin();
            mod.Show();
        }

        /// <summary>
        /// Maneja el clic en el boton "Buscar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void buscar_Click(object sender, RoutedEventArgs e)
        {
            BusquedaPaciente buscar = new BusquedaPaciente();
            buscar.Show();
        }

        /// <summary>
        /// Maneja el clic en el boton "Paciente Nuevo"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void pacienteNuevo_Click(object sender, RoutedEventArgs e)
        {
            CrearPaciente crearP = new CrearPaciente();
            crearP.ShowDialog();
        }

        /// <summary>
        /// Maneja el clic en el boton "Asignar Cama"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void asignarCama_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != null)
            {
                Hospitalizacion hos = new Hospitalizacion(refCama, idCentro);
                hos.ShowDialog();

                idCama = baseDeDatos.ObtenerIdCama(refCama, idCentro);
                nhcPaSel = baseDeDatos.ObtenerNHCporIdCama(idCama);

                ActualizarBoton(idCama, nhcPaSel);
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna cama");
            }
        }

        /// <summary>
        /// Maneja el clic en el boton "Candado Rojo"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void candadoRojo_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != null)
            {
                baseDeDatos.BloquearCama(idCama, true);

                botonSeleccionado.Background = Brushes.Red;
                botonSeleccionado.UpdateLayout();
                botonSeleccionado.IsChecked = false;

                botonSeleccionado = null;
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna cama");
            }
        }

        /// <summary>
        /// Maneja el clic en el boton "Candado Verde"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void candadoVerde_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != null)
            {
                baseDeDatos.BloquearCama(idCama, false);

                botonSeleccionado.Background = Brushes.Transparent;
                botonSeleccionado.UpdateLayout();
                botonSeleccionado.IsChecked = false;

                botonSeleccionado = null;
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna cama");
            }
        }

        /// <summary>
        /// Maneja el clic en el boton "Alertas"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void alertas_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != null)
            {
                botonSeleccionado.IsChecked = false;
                botonSeleccionado = null;

                GestionAlertas alertas = new GestionAlertas(nhcPaSel);
                alertas.ShowDialog();

                idCama = baseDeDatos.ObtenerIdCama(refCama, idCentro);
                nhcPaSel = baseDeDatos.ObtenerNHCporIdCama(idCama);

                ActualizarBoton(idCama, nhcPaSel);
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna cama");
            }
        }

        /// <summary>
        /// Maneja el clic en el boton "Informes"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void informes_Click(object sender, RoutedEventArgs e)
        {
            Informe info = new Informe(nhcPaSel);
            info.Show();
        }

        /// <summary>
        /// Maneja el clic en el boton "Alta"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void alta_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != null)
            {
                AltaPaciente alta = new AltaPaciente(refCama, idCentro);
                alta.ShowDialog();

                idCama = baseDeDatos.ObtenerIdCama(refCama, idCentro);
                nhcPaSel = baseDeDatos.ObtenerNHCporIdCama(idCama);

                ActualizarBoton(idCama, nhcPaSel);
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna cama");
            }
        }

        /// <summary>
        /// Maneja el clic en el boton "Consultar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void consultar_Click(object sender, RoutedEventArgs e)
        {
            List<string> usuarios = baseDeDatos.ObtenerUsuarios();

            if (usuarios.Count > 0)
            {
                StringBuilder mensajeUsuarios = new StringBuilder("Usuarios:\n");

                foreach (string usuario in usuarios)
                {
                    mensajeUsuarios.AppendLine(usuario);
                }

                MessageBox.Show(mensajeUsuarios.ToString());
            }
            else
            {
                MessageBox.Show("No se encontraron usuarios");
            }
        }

        /// <summary>
        /// Boton seleccionado
        /// </summary>
        private ToggleButton botonSeleccionado;

        /// <summary>
        /// Maneja el cambio de seleccion en el combo box de plantas
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void planta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            botones.Children.Clear();
            ResetearInterfaz();

            if (planta.SelectedItem != null)
            {
                int p = (int)planta.SelectedItem;
                string h = hospital.SelectedItem.ToString();
                idCentro = baseDeDatos.ObtenerIdCentro(h);

                List<string> camas = baseDeDatos.ObtenerCamasPorPlantas(p, idCentro);

                foreach (string cama in camas)
                {
                    Grid grid = new Grid();
                    Image image = new Image();
                    idCama = baseDeDatos.ObtenerIdCama(cama, idCentro);
                    bool estaBloqueada = baseDeDatos.ComprobarCamaBloqueada(idCama);

                    if (estaBloqueada)
                    {
                        grid.Background = Brushes.Red;
                    }

                    nhcPaSel = baseDeDatos.ObtenerNHCporIdCama(idCama);
                    bool pacienteIngresado = baseDeDatos.VerificarPacienteIngresado(idCama);
                    bool esHombre = baseDeDatos.ObtenerSexoPaciente(nhcPaSel);
                    bool tieneAlerta = baseDeDatos.ObtenerAlertaPaciente(nhcPaSel);

                    if (!pacienteIngresado)
                    {
                        rutaImagen = esHombre ? (tieneAlerta ? "camaHombreAlerta.png" : "camaHombre.png") : (tieneAlerta ? "camaMujerAlerta.png" : "camaMujer.png");
                    }
                    else
                    {
                        rutaImagen = "camaVacia.png";
                    }

                    image.Source = new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", rutaImagen)));
                    image.Width = 120;
                    image.Height = 120;

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = cama;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Right;
                    textBlock.Foreground = Brushes.Yellow;
                    textBlock.FontWeight = FontWeights.Bold;
                    textBlock.TextAlignment = TextAlignment.Center;

                    ToggleButton botonCama = new ToggleButton();
                    botonCama.Content = grid;
                    botonCama.Background = Brushes.Transparent;
                    botonCama.Checked += Button_Checked;
                    botonCama.Unchecked += Button_Unchecked;

                    grid.Children.Add(image);
                    grid.Children.Add(textBlock);

                    botones.Children.Add(botonCama);
                }
            }
        }

        /// <summary>
        /// Maneja el cambio de seleccion en el combo box de hospitales
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void hospital_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            botones.Children.Clear();
            ResetearInterfaz();

            if (hospital.SelectedItem != null)
            {
                List<int> plantas = new List<int> { 1, 2, 3, 4 };
                planta.ItemsSource = plantas;
                pl.Visibility = Visibility.Visible;
                planta.Visibility = Visibility.Visible;

                planta.SelectedItem = null;
            }
            else
            {
                planta.SelectedItem = null;
                pl.Visibility = Visibility.Collapsed;
                planta.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Controla el evento Checked de los botones
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void Button_Checked(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != null && botonSeleccionado != sender)
            {
                botonSeleccionado.IsChecked = false;
            }

            botonSeleccionado = (ToggleButton)sender;
            refCama = ((TextBlock)((Grid)botonSeleccionado.Content).Children[1]).Text;
            cama.Content = "CAMA:\t" + refCama;
            idCama = baseDeDatos.ObtenerIdCama(refCama, idCentro);
            bool pacienteIngresado = baseDeDatos.VerificarPacienteIngresado(idCama);
            nhcPaSel = baseDeDatos.ObtenerNHCporIdCama(idCama);
            bool tieneAlerta = baseDeDatos.ObtenerAlertaPaciente(nhcPaSel);

            if (!pacienteIngresado)
            {
                camaMini.Visibility = Visibility.Visible;
                nombre_completo.Content = "PACIENTE:\t" + baseDeDatos.MostrarInfoPacienteSeleccionado(nhcPaSel);
                edad.Content = "EDAD:\t" + baseDeDatos.ObtenerEdadPaciente(nhcPaSel) + " Años";

                List<string> sintomaDiagnostico = baseDeDatos.ObtenerSintomaDiagnosticoPorPaciente(nhcPaSel);

                if (sintomaDiagnostico.Count == 2)
                {
                    sintomas.Content = "SINTOMAS:\t" + sintomaDiagnostico[0];
                    diagnostico.Content = "DIAGNOSTICO:\t" + sintomaDiagnostico[1];
                }
                else
                {
                    sintomas.Content = " ";
                    diagnostico.Content = " ";
                }

                if (tieneAlerta)
                {
                    alerta.Content = "ALERTA:\t" + baseDeDatos.MostrarInfoAlertaPaciente(nhcPaSel);
                    alertaMini.Visibility = Visibility.Visible;
                }
                else
                {
                    alerta.Content = " ";
                    alertaMini.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                nombre_completo.Content = " ";
                camaMini.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Controla el evento Unchecked de los botones
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void Button_Unchecked(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado == sender)
            {
                botonSeleccionado = null;
                ResetearInterfaz();
            }
        }

        /// <summary>
        /// Metodo para restablecer la interfaz
        /// </summary>
        private void ResetearInterfaz()
        {
            nombre_completo.Content = " ";
            edad.Content = " ";
            alerta.Content = " ";
            sintomas.Content = " ";
            diagnostico.Content = " ";
            nombre_completo.Content = " ";
            alertaMini.Visibility = Visibility.Collapsed;
            camaMini.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// Cambia la imagen de la cama en el boton segun su estado
        /// </summary>
        /// <param name="button">El boton a modificar</param>
        /// <param name="rutaImagen">La ruta de la imagen a asignar</param>
        private void CambiarImagenBoton(ToggleButton button, string rutaImagen)
        {
            Grid grid = (Grid)button.Content;
            Image image = (Image)grid.Children[0];

            string rutaRelativa = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", rutaImagen);

            if (File.Exists(rutaRelativa))
            {
                image.Source = new BitmapImage(new Uri(rutaRelativa));
            }
        }

        /// <summary>
        /// Maneja el clic en el boton "Eliminar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void eliminar_Click(object sender, RoutedEventArgs e)
        {
            EliminarUsuario eliminar = new EliminarUsuario();
            eliminar.ShowDialog();
        }

        /// <summary>
        /// Metodo utilizado para actualizar los bonotes tras los cambios
        /// </summary>
        /// <param name="idCama">Identificador de la cama</param>
        /// <param name="nhcPaSel">Numero de historia del paciente seleccionado</param>
        private void ActualizarBoton(int idCama, int nhcPaSel)
        {
            bool pacienteIngresado = baseDeDatos.VerificarPacienteIngresado(idCama);
            bool esHombre = baseDeDatos.ObtenerSexoPaciente(nhcPaSel);
            bool tieneAlerta = baseDeDatos.ObtenerAlertaPaciente(nhcPaSel);

            string rutaImagen = (!pacienteIngresado)
                ? (esHombre ? (tieneAlerta ? "camaHombreAlerta.png" : "camaHombre.png") : (tieneAlerta ? "camaMujerAlerta.png" : "camaMujer.png"))
                : "camaVacia.png";

            if (botonSeleccionado != null)
            {
                CambiarImagenBoton(botonSeleccionado, rutaImagen);

                botonSeleccionado.IsChecked = false;
                botonSeleccionado = null;
            }
        }

        /// <summary>
        /// Maneja el clic en el boton "Procesos"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void procesos_Click(object sender, RoutedEventArgs e)
        {
            notas.Visibility = Visibility.Visible;
            prescripcion.Visibility = Visibility.Visible;
            patologia.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Maneja el clic en el boton "Notas"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void notas_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != null)
            {
                idCama = baseDeDatos.ObtenerIdCama(refCama, idCentro);
                nhcPaSel = baseDeDatos.ObtenerNHCporIdCama(idCama);

                NotasEnfermeria notas = new NotasEnfermeria(nhcPaSel);
                notas.Show();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna cama");
            }
        }

        /// <summary>
        /// Maneja el clic en el boton "Prescripcion"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void prescripcion_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != null)
            {
                idCama = baseDeDatos.ObtenerIdCama(refCama, idCentro);
                nhcPaSel = baseDeDatos.ObtenerNHCporIdCama(idCama);

                usC2.Visibility = Visibility.Visible;
                usC2.IsEnabled = true;

                List<List<string>> items = baseDeDatos.ObtenerHojasPrescripcionPorPaciente(nhcPaSel);
                usC2.lista.ItemsSource = items;

                /// <summary>
                /// Maneja el evento Clic del boton Añadir
                /// </summary>
                usC2.AñadirClick += TipoAccion =>
                {
                    try
                    {
                        if (TipoAccion == typeof(HojaPrescripcion))
                        {
                            HojaPrescripcion hoja = new HojaPrescripcion(nhcPaSel);
                            hoja.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Excepción en ClicAñadir: " + ex.Message);
                    }
                };

                /// <summary>
                /// Maneja el evento Clic del boton Cancelar
                /// </summary>
                usC2.CancelarClick += TipoAccion =>
                {

                    if (TipoAccion == typeof(UserControl2))
                    {
                        usC2.Visibility = Visibility.Collapsed;
                        usC2.IsEnabled = false;
                    }
                };
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna cama");
            }
        }

        /// <summary>
        /// Maneja el clic en el boton "Patologias"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void patologia_Click(object sender, RoutedEventArgs e)
        {
            if (botonSeleccionado != null)
            {
                idCama = baseDeDatos.ObtenerIdCama(refCama, idCentro);
                nhcPaSel = baseDeDatos.ObtenerNHCporIdCama(idCama);

                usC3.Visibility = Visibility.Visible;
                usC3.IsEnabled = true;

                List<List<string>> items = baseDeDatos.ObtenerPatologiasPorPaciente(nhcPaSel);
                usC3.lista.ItemsSource = items;

                /// <summary>
                /// Maneja el evento Clic del boton Añadir
                /// </summary>
                usC3.AñadirClick += TipoAccion =>
                {

                    if (TipoAccion == typeof(Patologia))
                    {
                        Patologia pato = new Patologia(nhcPaSel);
                        pato.Show();
                    }
                };

                /// <summary>
                /// Maneja el evento Clic del boton Cancelar
                /// </summary>
                usC3.CancelarClick += TipoAccion =>
                {

                    if (TipoAccion == typeof(UserControl2))
                    {
                        usC3.Visibility = Visibility.Collapsed;
                        usC3.IsEnabled = false;
                    }
                };
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna cama");
            }
        }
    }
}
