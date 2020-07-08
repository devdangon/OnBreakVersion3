using OnBreak;
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

namespace OnBreakWPF
{
    /// <summary>
    /// Lógica de interacción para AuxiliarWindows.xaml
    /// </summary>
    public partial class AuxiliarWindows : Window
    {
        MainWindow main = null;
        

        public AuxiliarWindows(MainWindow vista)
        {
            InitializeComponent();
            main = vista;
            cargarDataGrid();
             
        }

        public void cargarDataGrid()
        {
            var copia = new Cliente().ReadAll();

            dg_filtros_listado.ItemsSource = copia;
        }

        private void Btn_agregar_cliente_contrato_Click(object sender, RoutedEventArgs e)
        {
            //main.txt_rut_contrato.Text = txt_rut_aux.Text;
            main.Visibility = Visibility.Visible;
            // main.txt_rut_contrato.Text = txt_busqueda_listado.Text;

            var cli = (ClienteDataGrid)dg_filtros_listado.SelectedItem;
            if (cli != null)
            {
                main.txt_rut_contrato.Text = cli.rut_cliente;

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                main.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Btn_buscar_cliente_aux_Click(object sender, RoutedEventArgs e)
        {

            var copia = new Cliente().ReadAll();
            //var copia1 = new List<ClienteDataGrid>();

            if (txt_busqueda_listado.Text.Equals(""))//-->Si el text esta vacio
            {
                if (porTipoEmpresa())//-->Si dentro de los filtros a considerar se encuentra algun tipo de empresa de ser asi se creara una nueva coleccion #return true/false
                {
                    copia = filtroTipoEmpresa(copia);//-->Este metodo devolvera una coleccion con los nuevos clientes segun los filtros
                }
                

                if (porActividadEmpresa())//-->Este metodo validara que dentro de los filtros tambien se encuentre la ectividad de empresa
                {
                    copia = filtroActividadEmpresa(copia);//-->Nos devolvera una coleccion con los datos filtrados
                }
            }
           
            dg_filtros_listado.ItemsSource = copia;//-->Al finalizar el algoritmo se mostrara una datagrid con los datos filtrados
        }
        private List<ClienteDataGrid> filtroActividadEmpresa(List<ClienteDataGrid> lista)//-->#return ColeccionCliente
        /*Recorrer una coleccion, filtrar datos segun distintos parametros, crear una copia y almacenar ahi los datos filtrados*/
        {
            List<ClienteDataGrid> copia = new List<ClienteDataGrid>();//-->Creamos una nueva coleccion donde almacenaremos todos los datos recuperados
            if (chk_agropecuaria_listado.IsChecked.Equals(true))//-->Consultamos el estado del check de la actividad "agropecuario" #return true/false
            {
                foreach (var item in lista)//-->Recorremos la coleccion y cada elemento lo almacenamos en "item" #returm item = objeto Cliente 
                {
                    if (item.actividad_cliente.ToUpper().Equals(Cliente.ActividadEmpresa.AGROPECUARIA.ToString()))//-->Filtrar por los Cliente con la actividad "AGROPECUARIO" 
                    {
                        copia.Add(item);//-->Añadimos el cliente a la coneccion "copia"
                    }
                }
            }//--> Reutilizamos este codigo para el resto de opciones
            if (chk_mineria_listado.IsChecked.Equals(true))
            {
                foreach (var item in lista)
                {
                    if (item.actividad_cliente.ToUpper().Equals(Cliente.ActividadEmpresa.MINERIA.ToString()))
                    {
                        copia.Add(item);
                    }
                }
            }
            if (chk_manufactura_listado.IsChecked.Equals(true))
            {
                foreach (var item in lista)
                {
                    if (item.actividad_cliente.ToUpper().Equals(Cliente.ActividadEmpresa.MANUFACTURA.ToString()))
                    {
                        copia.Add(item);
                    }
                }
            }
            if (chk_comercio_listado.IsChecked.Equals(true))
            {
                foreach (var item in lista)
                {
                    if (item.actividad_cliente.ToUpper().Equals(Cliente.ActividadEmpresa.COMERCIO.ToString()))
                    {
                        copia.Add(item);
                    }
                }
            }
            if (chk_hoteleria_listado.IsChecked.Equals(true))
            {
                foreach (var item in lista)
                {
                    if (item.actividad_cliente.ToUpper().Equals(Cliente.ActividadEmpresa.HOTELERIA.ToString()))
                    {
                        copia.Add(item);
                    }
                }
            }
            if (chk_alimentos_listado.IsChecked.Equals(true))
            {
                foreach (var item in lista)
                {
                    if (item.actividad_cliente.ToUpper().Equals(Cliente.ActividadEmpresa.ALIMENTOS.ToString()))
                    {
                        copia.Add(item);
                    }
                }
            }
            if (chk_transporte_listado.IsChecked.Equals(true))
            {
                foreach (var item in lista)
                {
                    if (item.actividad_cliente.ToUpper().Equals(Cliente.ActividadEmpresa.TRANSPORTES.ToString()))
                    {
                        copia.Add(item);
                    }
                }
            }
            if (chk_servicio_limitada.IsChecked.Equals(true))
            {
                foreach (var item in lista)
                {
                    if (item.actividad_cliente.ToUpper().Equals(Cliente.ActividadEmpresa.SERVICIOS.ToString()))
                    {
                        copia.Add(item);
                    }
                }
            }

            return copia;//--> Colleccion que almacena todos los datos filtrados 
        }


        ////Metodo para saber si dentro de los filtros debemos considerar el tipo de empresas
        public Boolean porTipoEmpresa()
        {
            var retorno = false;
            if (chk_spa_listado.IsChecked == true || chk_sociedad_listado.IsChecked == true || chk_limitada_listado.IsChecked == true || chk_eirl_listado.IsChecked == true)
            {
                retorno = true;
            }

            return retorno;
        }

        ////Metodo para saber si dentro de los filtros debemos  considerar la actividad de empresa
        public Boolean porActividadEmpresa()
        {
            var retorno = false;
            if (chk_agropecuaria_listado.IsChecked.Equals(true) || chk_alimentos_listado.IsChecked.Equals(true) || chk_comercio_listado.IsChecked.Equals(true) || chk_hoteleria_listado.IsChecked.Equals(true) || chk_manufactura_listado.IsChecked.Equals(true) || chk_mineria_listado.IsChecked.Equals(true) || chk_transporte_listado.IsChecked.Equals(true) || chk_servicio_limitada.IsChecked.Equals(true))
            {
                retorno = true;
            }

            return retorno;
        }

        public List<ClienteDataGrid> filtroTipoEmpresa(List<ClienteDataGrid> copia)
        {
            var tmpcopia = new List<ClienteDataGrid>();
            if (chk_spa_listado.IsChecked.Equals(true))
            {
                foreach (ClienteDataGrid item in copia)
                {

                    if (item.tipo_cliente.Equals(Cliente.TipoEmpresa.SPA.ToString()))
                    {
                        tmpcopia.Add(item);
                    }
                }
            }
            if (chk_sociedad_listado.IsChecked.Equals(true))
            {
                foreach (ClienteDataGrid item in copia)
                {
                    if (item.tipo_cliente.Equals(Cliente.TipoEmpresa.SA.ToString()))
                    {
                        tmpcopia.Add(item);
                    }
                }
            }
            if (chk_limitada_listado.IsChecked.Equals(true))
            {
                foreach (ClienteDataGrid item in copia)
                {
                    if (item.tipo_cliente.Equals(Cliente.TipoEmpresa.LIMITADA.ToString()))
                    {
                        tmpcopia.Add(item);
                    }
                }
            }
            if (chk_eirl_listado.IsChecked.Equals(true))
            {
                foreach (var item in copia)
                {
                    if (item.tipo_cliente.Equals(Cliente.TipoEmpresa.EIRL.ToString()))
                    {
                        tmpcopia.Add(item);
                    }
                }
            }

            return tmpcopia;

        }


    }
}
