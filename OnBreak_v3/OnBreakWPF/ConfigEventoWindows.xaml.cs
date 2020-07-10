using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using OnBreak.Dato;
using OnBreak.Negocio;

namespace OnBreakWPF
{
    /// <summary>
    /// Lógica de interacción para ConfigEventoWindows.xaml
    /// </summary>
    public partial class ConfigEventoWindows : Window
    {
        
        MainWindow main = null;
        //OnBreak.Negocio.TipoEvento tipoEvento = new OnBreak.Negocio.TipoEvento();
        //modalidad evento:
        ModalidadServicio modalidad = new ModalidadServicio();
        //Contrato local
        OnBreak.Negocio.Contrato localContrato;

        OnBreak.FactoryProducer factoryProducer = new OnBreak.FactoryProducer();


        public ConfigEventoWindows(MainWindow main, OnBreak.Negocio.TipoEvento tipoEvento, OnBreak.ModalidadServicio modalidad,
            OnBreak.Negocio.Contrato contrato)
        {
            InitializeComponent();
            //asignacion de contrato local
            localContrato = contrato;
            //mostrar grid de evento:
            //MostrarEvento();
            //Agregar valores a combobox de eventos:
            AgregarEventos();
            cmb_tipo_evento.SelectedIndex = 0;
            this.main = main;
            //campos deshabilitados:
            //*txt_tipo_evento_evento.IsEnabled = false;
            txt_personal_base_evento.IsEnabled = false;
            txt_valor_base_evento.IsEnabled = false;
            txt_valor_total_evento.IsEnabled = false;
            txt_valor_asistente_adicional.IsEnabled = false;
            txt_valor_personal_adicional.IsEnabled = false;


            //*txt_tipo_evento_evento.Text = main.cmb_tipo_adm_contrato.SelectedValue.ToString();
            //evento = cmb_tipo_evento.SelectedValue.ToString();   /*txt_tipo_evento_evento.Text;*/

            //indice por defecto de los combobox:
            cmb_asist_adici_evento.SelectedIndex = 0;
            cmb_perso_adici_evento.SelectedIndex = 0;

           

            //arreglos int condatos para combobox:
            int[] asistentes = { 0, 20, 40, 60, 80, 100, 120, 140, 160, 180, 200 };
            int[] personal = { 0, 2, 3, 4, 5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};

            //Llenar los combobox:
            foreach (int i in asistentes)
            {
                cmb_asist_adici_evento.Items.Add(i);
            }
            foreach (int i in personal)
            {
                cmb_perso_adici_evento.Items.Add(i);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            main.Visibility = Visibility.Visible;
            //main.lbl_observaciones_adm_contrato.Content = main.contrato.valor_total_evento.ToString();
        }
        //agregar tipo de evento al combobox tipo:
        public void AgregarEventos()
        {
            List<string> eventos = new List<String>();
            eventos.Add("seleccionar");
            eventos.Add("LightBreak");
            eventos.Add("DayBreak");
            eventos.Add("JournalBreak");
            eventos.Add("Ejecutiva");
            eventos.Add("Celebracion");
            eventos.Add("QuickCocktail");
            eventos.Add("AmbientCocktail");

            foreach (string item in eventos)
            {
                cmb_tipo_evento.Items.Add(item);
            }

        }

        private void Btn_calcular_evento_Click(object sender, RoutedEventArgs e)
        {
            //Creacion de als fabricas
            var factoryCoffe = factoryProducer.getFactory("CoffeBreak");
            var factoryCena = factoryProducer.getFactory("Cena");
            var factoryCocktail = factoryProducer.getFactory("Cocktail");

            var TipoEventos = factoryCoffe.getAbstractEvento("LightBreak");


            if (cmb_tipo_evento.SelectedIndex != -1 && cmb_tipo_evento.SelectedIndex != 0)
            {
                var C_tipoEvento = new List<OnBreak.Negocio.TipoEvento>();

                //int asistenteAdicional = 0;
                //int personalAdicional = 0;
                //int personalBase = 0;
                //int valorBaseEvento = 0;
                //int valorAsistenteBase = 0;
                //int valorPersonalBase = 0;
                //int valorAsistenteAdicional = 0;
                //float valorPersonalAdicional = 0;
                //float valorTotalEvento = 0;

                string evento = cmb_tipo_evento.SelectedValue.ToString();
                if (evento.Equals("LightBreak"))
                {
                    TipoEventos = factoryCoffe.getAbstractEvento("LightBreak");
                }
                if (evento.Equals("DayBreak"))
                {
                    TipoEventos = factoryCoffe.getAbstractEvento("DayBreak");
                }
                if (evento.Equals("JournalBreak"))
                {
                    TipoEventos = factoryCoffe.getAbstractEvento("JournalBreak");
                }
                if (evento.Equals("Ejecutiva"))
                {
                    TipoEventos = factoryCena.getAbstractEvento("Ejecutiva");
                }
                if (evento.Equals("Celebracion"))
                {
                    TipoEventos = factoryCena.getAbstractEvento("Celebracion");
                }
                if (evento.Equals("QuickCocktail"))
                {
                    TipoEventos = factoryCocktail.getAbstractEvento("QuickCocktail");
                }
                if (evento.Equals("AmbientCocktail"))
                {
                    TipoEventos = factoryCocktail.getAbstractEvento("AmbientCocktail");
                }



                TipoEventos.Asistentes = int.Parse(cmb_asist_adici_evento.SelectedValue.ToString());
                TipoEventos.PersonalAdicional = int.Parse(cmb_perso_adici_evento.SelectedValue.ToString());



                //Asistentes adicionales:
                //asistenteAdicional = int.Parse(cmb_asist_adici_evento.SelectedValue.ToString());--
                //valorAsistenteAdicional = int.Parse(cmb_asist_adici_evento.SelectedValue.ToString()) / 10;--

                //personal_adicional adicional:
                //personalAdicional = int.Parse(cmb_perso_adici_evento.SelectedValue.ToString());--

                //if (personalAdicional == 0 || personalAdicional == 2 || personalAdicional == 3)--
                //{
                //    valorPersonalAdicional = float.Parse(personalAdicional.ToString());--
                //}
                //if (personalAdicional == 4)--
                //{
                //    valorPersonalAdicional = 3.5f;--
                //}
                //if (personalAdicional > 4)--
                //{
                //    int adicional = 4;--
                //    valorPersonalAdicional = personalAdicional;--
                //    valorPersonalAdicional -= 4;--
                //    valorPersonalAdicional *= 0.5f;--
                //    valorPersonalAdicional += 3.5f;--
                //}
                //tipo de evento:
                //if (evento == "de 1 a 20")
                //{
                //    valorPersonalBase = 2;
                //    valorAsistenteBase = 3;

                //}
                //if (evento == "de 21 a 50" || evento == "más de 50")
                //{
                //    valorPersonalBase = 3;
                //    valorAsistenteBase = 5;
                //}
                //if (cmb_perso_adici_evento.SelectedIndex == 0)
                //{
                //    personalAdicional = 0;

                //}
                //else
                //{
                //    personalAdicional = int.Parse(cmb_perso_adici_evento.SelectedValue.ToString());
                //}


                //calculo total del evento:
                //valorBaseEvento = valorAsistenteBase + valorPersonalBase;
                //valorTotalEvento = valorBaseEvento + valorAsistenteAdicional + valorPersonalAdicional;

                //salida de datos:            
                //txt_valor_base_evento.Text = valorBaseEvento + " UF";
                txt_valor_base_evento.Text = TipoEventos.ValorBase + " UF";
                //txt_valor_asistente_base.Text = valorAsistenteBase + " UF";
                //txt_valor_asistente_base.Text = valorAsistenteBase + " UF";
                //txt_personal_base_evento.Text = valorPersonalBase + " UF";
                txt_personal_base_evento.Text = TipoEventos.PersonalBase + "";
                txt_valor_personal_adicional.Text = TipoEventos.recargoPersonal() + " UF";
                //txt_valor_personal_adicional.Text = valorPersonalAdicional + " UF";
                txt_valor_asistente_adicional.Text = TipoEventos.recargoAsistentes() + " UF";
                //txt_valor_asistente_adicional.Text = valorAsistenteAdicional + " UF";
                txt_valor_total_evento.Text = TipoEventos.getValorTotalEvento().ToString() + " UF";
                //txt_valor_total_evento.Text = valorTotalEvento.ToString() + " UF";

                //set de propiedades del objeto tipoevento:
                //main.tipoEvento.valor_personal_adicional = valorPersonalAdicional;
                main.tipoEvento.valor_personal_adicional = TipoEventos.recargoPersonal();

                //main.tipoEvento.valor_asistente_adicional = valorAsistenteAdicional;
                main.tipoEvento.valor_asistente_adicional = (int)TipoEventos.recargoAsistentes();

                //set de prop del objeto modalidad:
                /*
                 * public int IdModalidad { get; set; }1
                public string nombre_evento { get; set; }1
                public double valor_base { get; set; }1
                public int personal_base { get; set; }1
                public int tipo_evento { get; set; }

                 */
                //main.modalidad.personal_base = valorPersonalBase;
                main.modalidad.personal_base = TipoEventos.PersonalBase;

                //main.modalidad.nombre_evento = evento;
                main.modalidad.nombre_evento = TipoEventos.NombreEvento;

                //main.modalidad.valor_base = valorBaseEvento;
                main.modalidad.valor_base = TipoEventos.ValorBase;




                //main.tipoEvento.valor_personal_adicional = valorPersonalAdicional;
                main.tipoEvento.valor_personal_adicional = TipoEventos.recargoPersonal();

                //main.tipoEvento.valor_asistente_adicional = valorAsistenteAdicional;
                main.tipoEvento.valor_asistente_adicional = (int) TipoEventos.recargoAsistentes();

                //main.contrato.valor_total_evento = valorTotalEvento;
                main.contrato.valor_total_evento = TipoEventos.getValorTotalEvento();

                lbl_error_calcular_evento.Content = string.Empty;
                lbl_error_calcular_evento.Content = string.Empty;
                //C_tipoEvento.Add(tipoEvento);
                //dg_tipo_evento.ItemsSource = C_tipoEvento;
            }
            else { lbl_error_calcular_evento.Content = "*Elige un tipo de evento"; }

        }

        private void Btn_ingresar_evento_Click(object sender, RoutedEventArgs e)
        {
            int id_evento=0;
            var entidades = new OnBreakEntities();
            if (cmb_tipo_evento.SelectedIndex != 0 && txt_valor_total_evento.Text != string.Empty)
            {
                foreach (OnBreak.Dato.TipoEvento i in entidades.TipoEvento)
                {
                    id_evento = i.tipo_evento;
                }
                id_evento++;
                main.tipoEvento.tipo_evento = id_evento;
                //set de idModalidad y tipoevento clase ModalidadServicio:
                main.modalidad.IdModalidad = main.tipoEvento.tipo_evento;
                main.modalidad.tipo_evento = modalidad.IdModalidad;
                //tipoEvento.Create();
                main.txt_numero_evento_contrato.Text = main.tipoEvento.tipo_evento.ToString();
                this.Close();
                main.Visibility = Visibility.Visible;
                main.btn_calcular_adm_contrato.Background = new SolidColorBrush(Color.FromArgb(255, 33, 223, 33));
            }
            else
            {
                lbl_error_calcular_evento.Content = "*Debes Calcular un evento";
            }            
        }

        private void Btn_deshacer_evento_Click(object sender, RoutedEventArgs e)
        {
            main.tipoEvento.Delete();
        }

        public void CleanControl()
        {
            txt_personal_base_evento.Text = string.Empty;
            txt_valor_base_evento.Text = string.Empty;
            txt_personal_base_evento.Text = string.Empty;
            txt_valor_personal_adicional.Text = string.Empty;
            txt_valor_asistente_adicional.Text = string.Empty;
            txt_valor_asistente_base.Text = string.Empty;
            txt_valor_total_evento.Text = string.Empty;
            cmb_asist_adici_evento.SelectedIndex = 0;
            cmb_perso_adici_evento.SelectedIndex = 0;
            cmb_tipo_evento.SelectedItem = 0;
            lbl_error_calcular_evento.Content = string.Empty;
            cmb_tipo_evento.SelectedIndex = 0;
        }

        private void Btn_limpiar_evento_Click(object sender, RoutedEventArgs e)
        {
            CleanControl();
        }

        private void Cmb_tipo_evento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbl_error_calcular_evento.Content = string.Empty;
        }
    }
}
