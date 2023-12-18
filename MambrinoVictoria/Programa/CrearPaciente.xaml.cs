using MambrinoVictoria.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para CrearPaciente.xaml
    /// </summary>

    public partial class CrearPaciente : Window
    {
        BDD baseDeDatos;

        /// <summary>
        /// Constructor que inicializa la ventana de creacion de pacientes
        /// </summary>
        public CrearPaciente()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            baseDeDatos = BDD.InstanciaBDD();
        }

        /// <summary>
        /// Maneja el evento de carga de la ventana
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fechaNacimiento.Text = DateTime.MaxValue.ToString("dd-MM-yyyy");

            List<string> AreaSalud = baseDeDatos.ObtenerAreaSalud();
            areaSalud.ItemsSource = AreaSalud;

            List<string> s = new List<string> { " ", "Hombre", "Mujer" };
            sexo.ItemsSource = s;

            List<string> Regimen = new List<string> { " ", "Seguridad Social", "ISFAS", "MUFACE", "MUGEJU" };
            regimen.ItemsSource = Regimen;

            List<string> EstadoCivil = new List<string> { " ", "Soltero/a", "Casado/a", "Divorciado/a", "Viudo/a", "Otro" };
            estadoCivil.ItemsSource = EstadoCivil;

            List<string> cAutonomas = baseDeDatos.ObtenerComunidadesA();
            cAutonoma.ItemsSource = cAutonomas;
            cAutonomaNac.ItemsSource = cAutonomas;
        }

        /// <summary>
        /// Maneja el evento de cambio de texto en los TextBox donde se usa
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (!int.TryParse(textBox.Text, out _))
                {
                    textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }

            // Verifica si el texto no esta vacio y tiene una longitud mayor o igual a 9
            if (!string.IsNullOrEmpty(textBox.Text) && textBox.Text.Length >= 10)
            {
                // Elimina el ultimo caracter ingresado si la longitud es mayor o igual a 9
                textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        /// <summary>
        /// Maneja el evento de cambio de texto en los TextBox donde se usa
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (!int.TryParse(textBox.Text, out _))
                {
                    textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        /// <summary>
        /// Maneja el evento de check del checkBox titular
        /// Si el titular esta marcado habilita el textBox esTitulaNombre
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void titular_Checked(object sender, RoutedEventArgs e)
        {
            esTitularNombre.IsEnabled = true;
        }

        /// <summary>
        /// Maneja el evento de cambio del comboBox cAutonoma
        /// Actualiza las opciones del coboBox provincia segun la comunidad autonoma seleccionada
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void cAutonoma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cAutonoma.SelectedItem != null)
            {
                string comunidadSeleccionada = cAutonoma.SelectedItem.ToString();

                List<string> provincias = baseDeDatos.ObtenerProvinciasPorComunidad(comunidadSeleccionada);

                provincia.ItemsSource = provincias;
            }
        }

        /// <summary>
        /// Maneja el evento de cambio del comboBox cAutonomaNac
        /// Actualiza las opciones del coboBox provincia de nacimiento segun la comunidad autonoma de nacimiento seleccionada
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void cAutonomaNac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cAutonomaNac.SelectedItem != null)
            {
                string comunidadSeleccionada = cAutonomaNac.SelectedItem.ToString();

                List<string> provincias = baseDeDatos.ObtenerProvinciasPorComunidad(comunidadSeleccionada);

                provinciaNac.ItemsSource = provincias;
            }
        }

        /// <summary>
        /// Maneja el evento de cambio del comboBox areaSalud
        /// Actualiza las opciones del coboBox xonaBasicaSalud segun el area de salud seleccionada
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void areaSalud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (areaSalud.SelectedItem != null)
            {
                string areaSeleccionada = areaSalud.SelectedItem.ToString();

                List<string> zonasBasicas = baseDeDatos.ObtenerZonaBasicaporArea(areaSeleccionada);

                zonaBasicaSalud.ItemsSource = zonasBasicas;
            }
        }

        /// <summary>
        /// Maneja el evento de cambio del comboBox zonaBasicaSalud
        /// Actualiza el texto del textBox cap segun la zona basica de salud seleccionada
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void zonaBasicaSalud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (zonaBasicaSalud.SelectedItem != null)
            {
                string zonaBasicaSeleccionada = zonaBasicaSalud.SelectedItem.ToString();

                string centrosalud = baseDeDatos.ObtenerCAP(zonaBasicaSeleccionada);

                cap.Text = centrosalud;
            }
        }

        /// <summary>
        /// Maneja el evento de check en el checBox nacionalNac
        /// Habilita los comboBox y textBox cAutonomaNac, provinciaNac y PoblacionNac cuando nacionalNac esta marcado
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void nacionalNac_Checked(object sender, RoutedEventArgs e)
        {
            if (nacionalNac.IsChecked == true)
            {
                if (cAutonomaNac != null)
                    cAutonomaNac.IsEnabled = true;
                if (provinciaNac != null)
                    provinciaNac.IsEnabled = true;
                if (PoblacionNac != null)
                    PoblacionNac.IsEnabled = true;
            }
        }

        /// <summary>
        /// Maneja el evento de check en el checBox extranjeroNac
        /// Deshabilita y establece el valor a null de los comboBox y textBox cAutonomaNac, provinciaNac y PoblacionNac cuando extranjeroNac esta marcado
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void extranjeroNac_Checked(object sender, RoutedEventArgs e)
        {
            if (extranjeroNac.IsChecked == true)
            {
                if (cAutonomaNac != null)
                    cAutonomaNac.IsEnabled = false;
                if (provinciaNac != null)
                    provinciaNac.IsEnabled = false;
                if (PoblacionNac != null)
                    PoblacionNac.IsEnabled = false;

                if (cAutonomaNac != null)
                    cAutonomaNac.Text = null;
                if (provinciaNac != null)
                    provinciaNac.Text = null;
                if (PoblacionNac != null)
                    PoblacionNac.Text = null;
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el boton "Aceptar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            string fallecido = "No";

            string nacio = nacional.IsChecked == true ? "Nacional" : (extranjero.IsChecked == true ? "Extranjero" : "");
            string nacioNac = nacionalNac.IsChecked == true ? "Nacional" : (extranjeroNac.IsChecked == true ? "Extranjero" : "");

            if (DateTime.TryParse(fechaNacimiento.Text, out DateTime fechaNac))
            {
                int codigoPostal;

                if (int.TryParse(cp.Text, out codigoPostal))
                {
                    string nifValue = nif.Text;
                    string numeroSSValue = numeross.Text;
                    string tisValue = tis.Text;

                    bool existeNIF = baseDeDatos.VerificarExistenciaNIF(nifValue);
                    bool existeNumeroSS = baseDeDatos.VerificarExistenciaNumeroSS(numeroSSValue);
                    bool existeTIS = baseDeDatos.VerificarExistenciaTIS(tisValue);

                    if (!existeNIF)
                    {
                        if (!existeNumeroSS)
                        {
                            if (!existeTIS)
                            {
                                baseDeDatos.AgregarPaciente(
                                    nif.Text,
                                    numeross.Text,
                                    nombre.Text,
                                    apellido1.Text,
                                    string.IsNullOrEmpty(apellido2.Text) ? null : apellido2.Text,
                                    sexo.Text,
                                    fechaNac,
                                    string.IsNullOrEmpty(telefono1.Text) ? null : telefono1.Text,
                                    string.IsNullOrEmpty(apellido2.Text) ? null : telefono2.Text,
                                    string.IsNullOrEmpty(movil.Text) ? null : movil.Text,
                                    string.IsNullOrEmpty(estadoCivil.Text) ? null : estadoCivil.Text,
                                    string.IsNullOrEmpty(estudios.Text) ? null : estudios.Text,
                                    fallecido,
                                    nacio,
                                    cAutonoma.Text,
                                    provincia.Text,
                                    poblacion.Text,
                                    codigoPostal,
                                    string.IsNullOrEmpty(direccion.Text) ? null : direccion.Text,
                                    esTitularNombre.Text,
                                    regimen.Text,
                                    tis.Text,
                                    medicoPrimaria.Text,
                                    cap.Text,
                                    zonaBasicaSalud.Text,
                                    areaSalud.Text,
                                    nacioNac,
                                    cAutonomaNac.Text,
                                    provinciaNac.Text,
                                    PoblacionNac.Text);

                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Error: El TIS ya existe");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error: El Numero de la Seguridad Social ya existe");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: El NIF ya existe");
                        return;
                    }
                }
            }
        }
    }
}
