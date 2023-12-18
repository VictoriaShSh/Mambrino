using MambrinoVictoria.BaseDeDatos;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MambrinoVictoria.Programa
{
    /// <summary>
    /// Lógica de interacción para NotasEnfermeria.xaml
    /// </summary>
    public partial class NotasEnfermeria : Window
    {
        BDD baseDeDatos;
        int nhc;

        /// <summary>
        /// Constructor que inicializa la ventana de notas de enfermeria
        /// </summary>
        /// <param name="nhcPaSel">Numero de historia clinica del paciente seleccionado</param>
        public NotasEnfermeria(int nhcPaSel)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            baseDeDatos = BDD.InstanciaBDD();

            nhc = nhcPaSel;

            fecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
            hora.Text = DateTime.Now.ToString("HH:mm");

            peso.TextChanged += ActualizarIMC;
            talla.TextChanged += ActualizarIMC;

            balAlimentos.TextChanged += ActualizarBalHid;
            balLiquidos.TextChanged += ActualizarBalHid;
            balFluidoterapia.TextChanged += ActualizarBalHid;
            balHemoderivados.TextChanged += ActualizarBalHid;
            balOtros.TextChanged += ActualizarBalHid;
            balDiuresis.TextChanged += ActualizarBalHid;
            balVomitos.TextChanged += ActualizarBalHid;
            balHeces.TextChanged += ActualizarBalHid;
            balSonda.TextChanged += ActualizarBalHid;
            balDrenajes.TextChanged += ActualizarBalHid;
            balOtrasPedidas.TextChanged += ActualizarBalHid;
        }

        /// <summary>
        /// Manejador del evento Click del boton "Aceptar"
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DateTime.TryParse(fecha.Text, out DateTime fechaRegistro) && TimeSpan.TryParse(hora.Text, out TimeSpan horaRegistro))
                {
                    decimal.TryParse(temperatura.Text, out decimal temperaturaValor);
                    int sistolicaValor = ConvertToInt(sistolica.Text);
                    int diastolicaValor = ConvertToInt(diastolica.Text);
                    int frecuenciaCardValor = ConvertToInt(frecuenciaCard.Text);
                    int frecuenciaRespValor = ConvertToInt(frecuenciaResp.Text);
                    decimal.TryParse(peso.Text, out decimal pesoValor);
                    decimal.TryParse(talla.Text, out decimal tallaValor);
                    decimal imcValor = CalcularIMC(pesoValor, tallaValor) * 10;
                    decimal.TryParse(glucemia.Text, out decimal glucemiaValor);

                    int totalBalanceHidrico = CalcularTotalBalanceHidrico(
                        ConvertToInt(balAlimentos.Text),
                        ConvertToInt(balLiquidos.Text),
                        ConvertToInt(balFluidoterapia.Text),
                        ConvertToInt(balHemoderivados.Text),
                        ConvertToInt(balOtros.Text),
                        ConvertToInt(balDiuresis.Text),
                        ConvertToInt(balVomitos.Text),
                        ConvertToInt(balHeces.Text),
                        ConvertToInt(balSonda.Text),
                        ConvertToInt(balDrenajes.Text),
                        ConvertToInt(balOtrasPedidas.Text)
                    );

                    baseDeDatos.RegistrarNotasEnfermeria(
                        nhc,
                        fechaRegistro,
                        horaRegistro,
                        temperaturaValor / 10,
                        sistolicaValor,
                        diastolicaValor,
                        frecuenciaCardValor,
                        frecuenciaRespValor,
                        pesoValor / 10,
                        tallaValor / 10,
                        imcValor,
                        glucemiaValor / 10,
                        ingestaAl.Text,
                        deposicion.Text,
                        nauseas.Text,
                        prurito.Text,
                        observaciones.Text,
                        ConvertToInt(balAlimentos.Text),
                        ConvertToInt(balLiquidos.Text),
                        ConvertToInt(balFluidoterapia.Text),
                        ConvertToInt(balHemoderivados.Text),
                        ConvertToInt(balOtros.Text),
                        ConvertToInt(balDiuresis.Text),
                        ConvertToInt(balVomitos.Text),
                        ConvertToInt(balHeces.Text),
                        ConvertToInt(balSonda.Text),
                        ConvertToInt(balDrenajes.Text),
                        ConvertToInt(balOtrasPedidas.Text),
                        totalBalanceHidrico
                    );

                    totBalHid.Text = totalBalanceHidrico.ToString();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo utilizado para establecer los campos de los textBox como int
        /// </summary>
        /// <param name="campo">Campo al que se aplica la transformacion</param>
        /// <returns></returns>
        private int ConvertToInt(string campo)
        {
            if (int.TryParse(campo, out int res))
            {
                return res;
            }
            return 0;
        }

        /// <summary>
        /// Metodo utilizado para calcular el IMC
        /// </summary>
        /// <param name="peso">Peso introducido del paciente</param>
        /// <param name="altura">Altura introducida del paciente</param>
        /// <returns></returns>
        private decimal CalcularIMC(decimal peso, decimal altura)
        {
            try
            {
                if (altura > 0)
                {
                    altura = altura / 100.0m;

                    int pesoEntero = Convert.ToInt32(peso);
                    int alturaEntero = Convert.ToInt32(altura);

                    return Math.Round(pesoEntero / ((decimal)alturaEntero * (decimal)alturaEntero), 1);
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Metodo que calcula el total del balance hidrico
        /// </summary>
        /// <param name="entradaAlimentos"></param>
        /// <param name="entradaLiquidos"></param>
        /// <param name="entradaFluidoterapia"></param>
        /// <param name="entradaHemoderivados"></param>
        /// <param name="entradaOtros"></param>
        /// <param name="salidaDiuresis"></param>
        /// <param name="salidaVomitos"></param>
        /// <param name="salidaHeces"></param>
        /// <param name="salidaSonda"></param>
        /// <param name="salidaDrenajes"></param>
        /// <param name="salidaOtrasPerdidas"></param>
        /// <returns></returns>
        private int CalcularTotalBalanceHidrico(int entradaAlimentos, int entradaLiquidos, int entradaFluidoterapia, int entradaHemoderivados, int entradaOtros,
            int salidaDiuresis, int salidaVomitos, int salidaHeces, int salidaSonda, int salidaDrenajes, int salidaOtrasPerdidas)
        {
            int sumaEntradas = entradaAlimentos + entradaLiquidos + entradaFluidoterapia + entradaHemoderivados + entradaOtros;
            int sumaSalidas = salidaDiuresis + salidaVomitos + salidaHeces + salidaSonda + salidaDrenajes + salidaOtrasPerdidas;

            int totalBalanceHidrico = sumaEntradas - sumaSalidas;

            return totalBalanceHidrico;
        }

        /// <summary>
        /// Metodo que actuliza el IMC
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void ActualizarIMC(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(peso.Text, out decimal pesoP) && decimal.TryParse(talla.Text, out decimal alturaP))
            {
                decimal imcP = CalcularIMC(pesoP, alturaP) * 10;

                if (imcP > 0)
                {
                    imc.Text = imcP.ToString();
                }
                else
                {
                    imc.Text = "Error";
                }
            }
            else
            {
                imc.Text = "No Valido";
            }
        }

        /// <summary>
        /// Metodo que actuliza el total del balance hidrico
        /// </summary>
        /// <param name="sender">El objeto que desencadeno el evento</param>
        /// <param name="e">Los argumentos del evento</param>
        private void ActualizarBalHid(object sender, TextChangedEventArgs e)
        {
            int totalBalanceHidrico = CalcularTotalBalanceHidrico(
                ConvertToInt(balAlimentos.Text),
                ConvertToInt(balLiquidos.Text),
                ConvertToInt(balFluidoterapia.Text),
                ConvertToInt(balHemoderivados.Text),
                ConvertToInt(balOtros.Text),
                ConvertToInt(balDiuresis.Text),
                ConvertToInt(balVomitos.Text),
                ConvertToInt(balHeces.Text),
                ConvertToInt(balSonda.Text),
                ConvertToInt(balDrenajes.Text),
                ConvertToInt(balOtrasPedidas.Text)
            );
            totBalHid.Text = totalBalanceHidrico.ToString();
        }
    }
}
