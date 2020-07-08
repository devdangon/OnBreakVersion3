using OnBreak;
using OnBreak.Negocio;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnBreakWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        //AuxiliarWindows auxiliarWindows = new AuxiliarWindows();
        public MainWindow()
        {
            InitializeComponent();
            txt_numero_evento_contrato.IsEnabled = false;
            //*cmb_tipo_adm_contrato.SelectedIndex = 0;
            AgregarEnumeradores();//Método para agregar enumeradores de Actividad de empresa.              
            limpiarLblValidacion();//Método para limpiar contenido de lbl_error al iniciar el soft.
            dg_filtros_adm_contrato.IsReadOnly=true;
            dg_filtros_administracion.IsReadOnly = true;
            mostrarGridAdministracion(dg_filtros_administracion);//Mostrar grid con cliente
            mostrarGridContrato(dg_filtros_adm_contrato);//Mostrar grid con contratos
            cmb_actividad_administracion.SelectedItem = Cliente.ActividadEmpresa.SELECCIONAR;
            cmb_tipo_administracion.SelectedItem = Cliente.TipoEmpresa.SELECCIONAR;

            //agregar contrato a la coleccion
            Contrato contrato_1 = new Contrato
            {
                numero_contrato = DateTime.Now.ToString("yyyyMMddHHmm"),
                direccion_contrato = "Av. Vicuña Mackena 1452",
                creacion_contrato = DateTime.Now,
                estado_vigencia_contrato = true,
                fecha_hora_inicio = DateTime.Today,
                fecha_hora_termino = DateTime.Today,
                Observaciones_contrato = "Contrato a plazo figjo"
            };
        }
        
        private void Btn_buscar_listado_Click(object sender, RoutedEventArgs e)
        {//-->Carlos Luna 
         //    //-->creamos la coleccion donde se iran guardando los datos
            
            var copia = new Cliente().ReadAll();
            //var copia1 = new List<ClienteDataGrid>();

            if (txt_busqueda_listado.Text.Equals(""))//-->Si el text esta vacio
            {
                if (porTipoEmpresa())//-->Si dentro de los filtros a considerar se encuentra algun tipo de empresa de ser asi se creara una nueva coleccion #return true/false
                {
                    copia = filtroTipoEmpresa(copia);//-->Este metodo devolvera una coleccion con los nuevos clientes segun los filtros
                }
                //else
                //{

                //    copia = volcarList();//-->En el caso de no seleccionarse ningun filtro a nivel de tipo de empresa se volcara la lista completa
                //}

                if (porActividadEmpresa())//-->Este metodo validara que dentro de los filtros tambien se encuentre la ectividad de empresa
                {
                    copia = filtroActividadEmpresa(copia);//-->Nos devolvera una coleccion con los datos filtrados
                }
            }
            dg_filtros_listado.ItemsSource = copia;//-->Al finalizar el algoritmo se mostrara una datagrid con los datos filtrados

        }

        //CArlos luna
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

        public List<ClienteDataGrid> filtroTipoEmpresa(List<ClienteDataGrid> copia )
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



        ///----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------///
        ///----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------///
        ///devdangon
        ///Bloque de código: Administración de Cliente
        ///
        /// 
        /// 
        ///Esta parte del codigo corresponde a los Eventos del formulario de administración de cliente:
        ///       


        //Evento del boton registrar, permite registrar un cliente
        private void Btn_registrar_administracion_Click(object sender, RoutedEventArgs e)
        {
            limpiarLblValidacion();
            Cliente nuevo = new Cliente();
            if (validarCampos() == true)
            {
                if (nuevo.ValidarRut(txt_rut_administracion.Text) == true)
                {
                    nuevo.rut_cliente = txt_rut_administracion.Text;
                    nuevo.razon_cliente = txt_razon_social_administracion.Text;
                    nuevo.nombre_cliente = txt_nombre_contacto_administracion.Text;
                    nuevo.mail_cliente = txt_mail_contacto_administracion.Text;
                    nuevo.direccion_cliente = txt_direccion_administracion.Text;
                    nuevo.actividad_cliente = int.Parse(cmb_actividad_administracion.SelectedIndex.ToString());
                    nuevo.tipo_cliente = int.Parse(cmb_tipo_administracion.SelectedIndex.ToString());
                    try
                    {
                        nuevo.telefono_cliente = int.Parse(txt_telefono_administracion.Text);
                        nuevo.Create();
                        limpiarCampos();
                        mostrarGridAdministracion(dg_filtros_administracion);
                        MessageBox.Show("Datos Guardados Correctamente");
                    }
                    catch(Exception)
                    {
                        lbl_error_telefono_administracion.Content = "Ingrese solo números";
                    }
                    //coleccion.coleccion.Add(nuevo);
                    

                    
                }
                else
                {
                    MessageBox.Show("El rut ya esta ingresado en OnBreak, consulte este rut", "Rut Ingresado ya existe",MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        //Evento del boton Buscar, permite Buscar un cliente o refrescar el dataGrid y sus cambios, mostrando a todos los clientes.
        private void Btn_buscar_cliente_administracion_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();
            mostrarGridAdministracion(dg_filtros_administracion);
            limpiarLblValidacion();

            if (txt_buscar_cliente_administracion.Text != string.Empty)
            {
                cliente.rut_cliente = txt_buscar_cliente_administracion.Text;
                if (cliente.Read() == true)
                {
                    txt_rut_administracion.IsEnabled = false;
                    txt_rut_administracion.Text = cliente.rut_cliente;
                    txt_razon_social_administracion.Text = cliente.razon_cliente;
                    txt_nombre_contacto_administracion.Text = cliente.nombre_cliente;
                    txt_mail_contacto_administracion.Text = cliente.mail_cliente;
                    txt_direccion_administracion.Text = cliente.direccion_cliente;
                    txt_telefono_administracion.Text = cliente.telefono_cliente.ToString();
                    cmb_actividad_administracion.SelectedIndex = cliente.actividad_cliente;
                    cmb_tipo_administracion.SelectedIndex = cliente.tipo_cliente;
                }
                else
                {
                    lbl_error_buscar_cliente.Content = "¡Rut no registrado!";
                }

            }
            else
            {
                sinRut();
            }
        }


        //Evento del boton Actualizar, permite modificar los datos del cliente  almacenados en la colección.
        private void Btn_actualizar_cliente_administracion_Click(object sender, RoutedEventArgs e)
        {
            limpiarLblValidacion();
            Cliente cliente = new Cliente();
            cliente.rut_cliente = txt_rut_administracion.Text;
            if (validarCampos() == true && cliente.Read() == true)
            {
                try
                {
                    cliente.razon_cliente = txt_razon_social_administracion.Text;
                    cliente.nombre_cliente = txt_nombre_contacto_administracion.Text;
                    cliente.mail_cliente = txt_mail_contacto_administracion.Text;
                    cliente.direccion_cliente = txt_direccion_administracion.Text;
                    cliente.telefono_cliente = int.Parse(txt_telefono_administracion.Text);
                    cliente.actividad_cliente = cmb_actividad_administracion.SelectedIndex;
                    cliente.tipo_cliente = cmb_tipo_administracion.SelectedIndex;
                    cliente.Update();
                    mostrarGridAdministracion(dg_filtros_administracion);
                    limpiarCampos();
                }
                catch (UpdateException ex)
                {
                    Console.WriteLine("Error" + ex);
                }
            }
            else
            {
                sinRut();
            }
        }

        private void sinRut()
        {
            lbl_error_buscar_cliente.Content = "Ingrese un rut ";
        }


        //Evento Boton Eliminar, permite eliminar un registro completo. La accion de eliminar esta protegido bajo un cuadro de diálogo de desicion "ok.cancel".
        private void Btn_eliminar_cliente_administracion_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();
            limpiarLblValidacion();
            if (txt_buscar_cliente_administracion.Text != string.Empty || txt_rut_administracion.Text != string.Empty)
            {                
                if (cliente.ValidarRut(txt_rut_administracion.Text) == false)
                {
                    //Aún falta agregar una nueva condicion del requerimiento •	No se puede eliminar un Cliente que tenga Contratos asociados, estén estos vigentes o no.
                    MessageBoxResult desicion = MessageBox.Show("¿Desea Eliminar el registro selecionado?",
                    "Eliminar Registro de cliente", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (desicion == MessageBoxResult.OK)
                    {

                        try
                        {

                            cliente.rut_cliente = txt_rut_administracion.Text;
                            if (cliente.Delete() == true)
                            {
                                MessageBox.Show("Registro eliminado");
                                limpiarCampos();
                                mostrarGridAdministracion(dg_filtros_administracion);
                            }
                            else
                            {
                                MessageBox.Show("El cliente tiene asociado un contranto, no es posible eliminar", "Error al Eliminar", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        catch (Exception)
                        {
                            lbl_error_buscar_cliente.Content = "¡rut no encontrado!";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("¡El rut ingresado no es cliente OnBreak!");
                }
            }
            else
            {
                sinRut();
            }
        }

        //Evento boton limpiar administración de cliente:
        private void Btn_limpiar_administra_Click(object sender, RoutedEventArgs e)
        {
            limpiarLblValidacion();
            txt_rut_administracion.IsEnabled = true;
            txt_rut_administracion.Text = string.Empty;
            txt_razon_social_administracion.Text = string.Empty;
            txt_nombre_contacto_administracion.Text = string.Empty;
            txt_mail_contacto_administracion.Text = string.Empty;
            txt_direccion_administracion.Text = string.Empty;
            txt_telefono_administracion.Text = string.Empty;
            cmb_actividad_administracion.SelectedItem = Cliente.ActividadEmpresa.SELECCIONAR;
            cmb_tipo_administracion.SelectedItem = Cliente.TipoEmpresa.SELECCIONAR;
            txt_buscar_cliente_administracion.Text = string.Empty;
        }


        ///Eventos:


        //Evento click en celda: devuelve a cada uno de los textbox los datos de la fila donde se ha realizado el click
        private void Dg_filtros_administracion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OnBreak.Dato.Cliente cliente = (OnBreak.Dato.Cliente)dg_filtros_administracion.SelectedItems[0];
                txt_rut_administracion.IsEnabled = false;
                txt_buscar_cliente_administracion.Text = cliente.rut_cliente;
                txt_rut_administracion.Text = cliente.rut_cliente;
                txt_razon_social_administracion.Text = cliente.razon_cliente;
                txt_nombre_contacto_administracion.Text = cliente.nombre_cliente;
                txt_mail_contacto_administracion.Text = cliente.mail_cliente;
                txt_direccion_administracion.Text = cliente.direccion_cliente;
                cmb_actividad_administracion.SelectedIndex = cliente.actividad_cliente;
                cmb_tipo_administracion.SelectedIndex = cliente.tipo_cliente;
                txt_telefono_administracion.Text = cliente.telefono_cliente.ToString();
            }
            catch(Exception){}
        }

        ///devdangon
        ///Esta parte del codigo corresponde a los métodos empleados para el funcionamiento del formulario de administración de cliente:
        ///

        //Método para agregar enumeradores
        private void AgregarEnumeradores()
        {
            foreach (var i in Enum.GetValues(typeof(Cliente.ActividadEmpresa)))
            {
                cmb_actividad_administracion.Items.Add(i);
            }
            foreach (var i in Enum.GetValues(typeof(Cliente.TipoEmpresa)))
            {
                cmb_tipo_administracion.Items.Add(i);
            }
        }

        //Método que limpia el contenido de los lbl de error, usados para notificar cuando uno o más campos estan vacíos o ya se encuentra
        //un registro, en el caso del rut del cliente.

        private void limpiarLblValidacion()
        {
            lbl_error_rut_administracion.Content = "";
            lbl_error_razon_social_administracion.Content = "";
            lbl_error_nombre_administracion.Content = "";
            lbl_error_mail_administracion.Content = "";
            lbl_error_direccion_administracion.Content = "";
            lbl_error_telefono_administracion.Content = "";
            lbl_error_tipo_administracion.Content = "";
            lbl_error_actividad_administracion.Content = "";
            lbl_error_buscar_cliente.Content = "";
        }

        //Método que muestra la grilla:
        private void mostrarGridAdministracion(DataGrid datagrid)
        {
            //Modelo de entidades 
            OnBreak.Dato.OnBreakEntities entidades = new OnBreak.Dato.OnBreakEntities();

            List<OnBreak.Dato.Cliente> listaEntidades = new List<OnBreak.Dato.Cliente>();

            foreach (OnBreak.Dato.Cliente i in entidades.Cliente)
            {
                listaEntidades.Add(i);
            }
            datagrid.ItemsSource = listaEntidades;
            datagrid.Items.Refresh();

        }

        //Método que limpia todos los campos del formulario de administración de cliente.
        private void limpiarCampos()
        {
            txt_buscar_cliente_administracion.Text = String.Empty;
            txt_rut_administracion.IsEnabled = true;
            txt_rut_administracion.Text = String.Empty;
            txt_razon_social_administracion.Text = String.Empty;
            txt_nombre_contacto_administracion.Text = String.Empty;
            txt_mail_contacto_administracion.Text = String.Empty;
            txt_direccion_administracion.Text = String.Empty;
            txt_telefono_administracion.Text = String.Empty;
            cmb_tipo_administracion.SelectedIndex = 0;
            cmb_actividad_administracion.SelectedIndex = 0;
        }

        //Método que valída todos los campos del formulario están o no vacios.
        private bool validarCampos()
        {
            int numero_validaciones = 0;
            bool validacion = false;
            bool revisar = false;
            while (revisar == false)
            {
                if (txt_rut_administracion.Text == String.Empty)
                {
                    lbl_error_rut_administracion.Content = "Ingrese un rut";
                    numero_validaciones++;
                }
                if (txt_razon_social_administracion.Text == String.Empty)
                {
                    lbl_error_razon_social_administracion.Content = "Ingrese la razón social";
                    numero_validaciones++;
                }
                if (txt_nombre_contacto_administracion.Text == String.Empty)
                {
                    lbl_error_nombre_administracion.Content = "Ingrese un nombre \n de contacto";
                }
                if (txt_mail_contacto_administracion.Text == String.Empty)
                {
                    lbl_error_mail_administracion.Content = "Ingrese un e-mail \n de contacto";
                }
                if (txt_direccion_administracion.Text == String.Empty)
                {
                    lbl_error_direccion_administracion.Content = "Ingrese la dirección";
                    numero_validaciones++;
                }
                if (txt_telefono_administracion.Text == String.Empty)
                {
                    lbl_error_telefono_administracion.Content = "Ingrese el número \n de telefono";
                    numero_validaciones++;
                }
                if (cmb_actividad_administracion.SelectedItem.Equals(Cliente.ActividadEmpresa.SELECCIONAR))
                {
                    lbl_error_actividad_administracion.Content = "Elíja la Activida \n de la empresa";
                    numero_validaciones++;
                }
                if (cmb_tipo_administracion.SelectedItem.Equals(Cliente.TipoEmpresa.SELECCIONAR))
                {
                    lbl_error_tipo_administracion.Content = "Elíja el tipo \n de empresa";
                    numero_validaciones++;
                }
                revisar = true;
            }
            if (numero_validaciones == 0)
            {
                validacion = true;
            }
            return validacion;
        }

        ///----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------///
        ///----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------///
        ///----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------///
        ///----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------///
        ///devdangon
        ///Bloque de código: Administración de Contrato
        ///
        /// 
        /// 
        ///Esta parte del codigo corresponde a los métodos empleados para el funcionamiento del formulario de administración de contrato:
        /// 


        //Evento Registrar contrato:
        private void Btn_registrar_adm_contrato_Click(object sender, RoutedEventArgs e)
        {
            //ElejirEvento();
            //txt_n_contrato_adm_contrato.Text = DateTime.Now.ToString("yyyyMMddHHmm");
            Contrato contrato = new Contrato();
            Cliente cliente = new Cliente();
            contrato.numero_contrato = txt_n_contrato_adm_contrato.Text;
            if (validacionContrato() == true)
            {
                if (cliente.ValidarRut(txt_rut_contrato.Text) == false)
                {
                    if (contrato.ValidarNumeroContrato() == true)
                    {
                        contrato.numero_contrato = txt_n_contrato_adm_contrato.Text;
                        contrato.direccion_contrato = txt_direccion_adm_contrato.Text;

                        contrato.rut_cliente = txt_rut_contrato.Text;

                        contrato.fecha_hora_inicio = DateTime.Now;
                        //fecha creacion contrato.
                        contrato.creacion_contrato = (DateTime)dp_creacion_admin_contrato.SelectedDate;
                        //fecha termino contrato.
                        contrato.termino_contrato = (DateTime)dp_termino_admin_contrato.SelectedDate;
                        //fecha y hora de inicio.
                        contrato.fecha_hora_inicio = (DateTime)dp_fecha_hora_inicio.SelectedDate;
                        //fecha y hora de termino.
                        contrato.fecha_hora_termino = (DateTime)dp_fecha_hora_termino.SelectedDate;
                        //validado con método 
                        contrato.estado_vigencia_contrato = VigenciaContrato(chk_vigencia_adm_contrato);
                        //ElejirEvento();
                        contrato.tipo_evento = int.Parse(txt_numero_evento_contrato.Text);
                        //*contrato.tipo_evento = cmb_tipo_adm_contrato.SelectedIndex;
                        contrato.Observaciones_contrato = txt_observaciones_adm_cotrato.Text;
                        contrato.Create();
                        limpiarCamposContrato();
                        mostrarGridContrato(dg_filtros_adm_contrato);
                    }
                    else
                    {
                        txt_n_contrato_adm_contrato.Text = string.Empty;
                        MessageBox.Show("¡El contrato ya existe!",
                        "Error de registro", MessageBoxButton.OK, MessageBoxImage.Stop);
                        limpiarCamposContrato();
                    }
                }
                else
                {
                    MessageBox.Show("¡El rut ingresado no es cliente OnBreak, debes ingresar un rut valido!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        //Evento Buscar Contrato:
        private void Btn_buscar_adm_contrato_Click(object sender, RoutedEventArgs e)
        {
            //this.main_window.Show();
            limpiarLblValidacionContrato();
            if (txt_buscar_adm_contrato.Text != String.Empty)
            {
                try
                {
                    OnBreak.Dato.OnBreakEntities entidades = new OnBreak.Dato.OnBreakEntities();
                    OnBreak.Dato.Contrato contrato = entidades.Contrato.First(con => con.numero_contrato == txt_buscar_adm_contrato.Text);

                    txt_n_contrato_adm_contrato.IsEnabled = false;
                    txt_n_contrato_adm_contrato.Text = contrato.numero_contrato;
                    dp_creacion_admin_contrato.SelectedDate = contrato.creacion_contrato;
                    dp_termino_admin_contrato.SelectedDate = contrato.termino_contrato;
                    dp_fecha_hora_inicio.SelectedDate = contrato.fecha_hora_inicio;
                    /*cmb_tipo_adm_contrato.SelectedIndex*/
                    txt_numero_evento_contrato.Text = contrato.tipo_evento.ToString();
                    dp_fecha_hora_termino.SelectedDate = contrato.fecha_hora_termino;
                    txt_direccion_adm_contrato.Text = contrato.direccion_contrato;
                    chk_vigencia_adm_contrato.IsChecked = contrato.estado_vigencia_contrato;
                    VigenciaContrato(chk_vigencia_adm_contrato);
                    //txt_per_asistente_adm_contrato.Text =
                    txt_observaciones_adm_cotrato.Text = contrato.Observaciones_contrato;
                    txt_rut_contrato.Text = contrato.rut_cliente;
                    mostrarGridContrato(dg_filtros_adm_contrato);
                }
                catch (Exception)
                {
                    MessageBox.Show("Número de contrato no existe", "Error de busqueda", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                sinNumeroContrato();
            }
        }

        //Evento boton Actualizar contrato:
        private void Btn_actualizar_adm_contrato_Click(object sender, RoutedEventArgs e)
        {            
            Actualizar(true);
        }
        //Método Actualizar:
        private void Actualizar(bool vigente)
        {
            Contrato contrato = new Contrato();
            contrato.numero_contrato = txt_n_contrato_adm_contrato.Text;
            if (validacionContrato() == true && contrato.ValidarNumeroContrato() == false)
            {
                try
                {
                    contrato.direccion_contrato = txt_direccion_adm_contrato.Text;
                    contrato.rut_cliente = txt_rut_contrato.Text;
                    contrato.fecha_hora_inicio = DateTime.Now;
                    //fecha creacion contrato.
                    contrato.creacion_contrato = (DateTime)dp_creacion_admin_contrato.SelectedDate;
                    //fecha termino contrato.
                    contrato.termino_contrato = (DateTime)dp_termino_admin_contrato.SelectedDate;
                    //fecha y hora de inicio.
                    contrato.fecha_hora_inicio = (DateTime)dp_fecha_hora_inicio.SelectedDate;
                    //fecha y hora de termino.
                    contrato.fecha_hora_termino = (DateTime)dp_fecha_hora_termino.SelectedDate;
                    //validar estado de vigencia:
                    if (vigente == true)
                    {
                        if (contrato.estado_vigencia_contrato == false)
                        {
                            chk_vigencia_adm_contrato.IsEnabled = false;
                        }
                        else
                        {
                            contrato.estado_vigencia_contrato = VigenciaContrato(chk_vigencia_adm_contrato);
                        }
                    }
                    else
                    {
                        contrato.estado_vigencia_contrato = false;
                        chk_vigencia_adm_contrato.IsEnabled = false;
                    }
                    //ElejirEvento();
                    contrato.tipo_evento = int.Parse(txt_numero_evento_contrato.Text); /*cmb_tipo_adm_contrato.SelectedIndex;*/
                    contrato.Observaciones_contrato = txt_observaciones_adm_cotrato.Text;
                    contrato.Update();
                    mostrarGridContrato(dg_filtros_adm_contrato);
                    limpiarCamposContrato();
                    limpiarLblValidacionContrato();
                }
                catch (UpdateException ex)
                {
                    Console.WriteLine("Error" + ex);
                }
            }
            else
            {
                sinRut();
            }

        }
        //Evento boton terminar contrato
        private void Btn_terminar_contrato_adm_contrato_Click(object sender, RoutedEventArgs e)
        {
            Actualizar(false);            
        }
        //Método que valida los campos de contrato
        private bool validacionContrato()
        {
            int contador = 0;
            bool validar = false;
            bool revisar = false;

            OnBreak.Dato.OnBreakEntities entidades = new OnBreak.Dato.OnBreakEntities();
            OnBreak.Dato.Contrato contrato = new OnBreak.Dato.Contrato();
            limpiarLblValidacionContrato();
            while (revisar == false)
            {    
                if(txt_n_contrato_adm_contrato.Text == string.Empty)
                {
                    lbl_error_numero_contrato.Content = "Genere un número";
                    contador++;
                }
                if (txt_rut_contrato.Text == string.Empty)
                {
                    lbl_error_rut_contrato.Content = "Debes Asociar un rut";
                    contador++;
                }
                if (dp_creacion_admin_contrato.ToString() == string.Empty)
                {
                    lbl_error_creacion.Content = "Indicar fecha incio";
                    contador++;
                }
                if (dp_termino_admin_contrato.ToString() == string.Empty)
                {
                    lbl_error_termino.Content = "Indicar fecha termino";
                    contador++;
                }
                if (dp_fecha_hora_inicio.ToString() == string.Empty)
                {
                    lbl_error_fecha_hora_inicio.Content = "Indicar hora de inicio";
                    contador++;
                }
                if (dp_fecha_hora_termino.ToString() == string.Empty)
                {
                    lbl_error_fecha_hora_termino.Content = "Indicar hora de termino";
                    contador++;
                }
                if (dp_fecha_hora_termino.ToString() == string.Empty)
                {
                    lbl_error_fecha_hora_termino.Content = "Indicar hora de termino";
                    contador++;
                }
                if (txt_direccion_adm_contrato.Text == string.Empty)
                {
                    lbl_error_direccion_contrato.Content = "Ingrese una dirección";
                    contador++;
                }
                if (chk_vigencia_adm_contrato.IsChecked == false && chk_vigencia_adm_contrato.IsEnabled == true)
                {
                    //chk_vigencia_adm_contrato.IsChecked = true;
                    lbl_error_vigencia_contrato.Content = "Chequear vigencia";
                    contador++;
                }
                if(txt_numero_evento_contrato.Text == string.Empty)
                {
                    btn_calcular_adm_contrato.Content = "Selecciona un tipo";
                    btn_calcular_adm_contrato.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    //btn_calcular_adm_contrato.Background.Transform.;
                    //lbl_error_tipo_contrato.Content = "Selecciona un tipo";
                    contador++;
                }
                /*
                if (cmb_tipo_adm_contrato.SelectedIndex <= 0)
                {
                    lbl_error_tipo_contrato.Content = "Selecciona un tipo";
                    contador++;
                }
                */
                if (txt_observaciones_adm_cotrato.Text == String.Empty)
                {
                    lbl_error_observaciones_contrato.Content = "Ingrese la observación";
                    contador++;
                }
                revisar = true;
            }
            if (contador == 0)
            {
                validar = true;
            }            
            return validar;
        }
        public bool VigenciaContrato(CheckBox vigencia )
        {
            bool estado = false;
            if (vigencia.IsChecked == true)
            { estado = true; lbl_error_vigencia_contrato.Content = "Vigente"; chk_vigencia_adm_contrato.IsEnabled = true; btn_actualizar_adm_contrato.IsEnabled = true; }
            else
            {estado = false; chk_vigencia_adm_contrato.IsEnabled = false; lbl_error_vigencia_contrato.Content = "No vigente"; btn_actualizar_adm_contrato.IsEnabled = false; }
            return estado;
        }

        //metodo para limpiar notificaciones de campos vacios:
        private void limpiarLblValidacionContrato()
        {
            lbl_error_rut_contrato.Content = string.Empty;
            lbl_error_creacion.Content = string.Empty;
            lbl_error_termino.Content = string.Empty;
            lbl_error_fecha_hora_inicio.Content = string.Empty;
            lbl_error_fecha_hora_termino.Content = string.Empty;
            lbl_error_numero_contrato.Content = string.Empty;
            lbl_error_direccion_contrato.Content = string.Empty;
            lbl_error_vigencia_contrato.Content = string.Empty;
            //lbl_error_personal_adicional.Content = string.Empty;
            lbl_error_tipo_administracion.Content = string.Empty;
            //lbl_error_tipo_contrato.Content = string.Empty;
            btn_calcular_adm_contrato.Content = "Configurar Evento";
            lbl_error_observaciones_contrato.Content = string.Empty;
            lbl_error_buscar_contrato.Content = string.Empty;
            btn_calcular_adm_contrato.Background = new SolidColorBrush(Color.FromArgb(255, 33, 223, 33));
        }

        //Método limpiar campos contrato:
        private void limpiarCamposContrato()
        {
            limpiarLblValidacionContrato();
            txt_n_contrato_adm_contrato.Text = string.Empty;
            //DatePicker.SetValue(dp_creacion_admin_contrato, DateTime.Today);
            txt_direccion_adm_contrato.Text = string.Empty;
            txt_observaciones_adm_cotrato.Text = string.Empty;
            chk_vigencia_adm_contrato.IsChecked = false;
            //*cmb_tipo_adm_contrato.SelectedIndex = 0;
            txt_observaciones_adm_cotrato.Text = string.Empty;
            //txt_per_asistente_adm_contrato.Text = string.Empty;
            txt_rut_contrato.Text = string.Empty;
            txt_numero_evento_contrato.Text = string.Empty;
        }

        //método que muestra el grid de contrato:
        private void mostrarGridContrato(DataGrid datagrid)
        {
            //Modelo de entidades 
            OnBreak.Dato.OnBreakEntities entidades = new OnBreak.Dato.OnBreakEntities();
            List<OnBreak.Dato.Contrato> listaEntidades = new List<OnBreak.Dato.Contrato>();

            foreach (OnBreak.Dato.Contrato i in entidades.Contrato)
            {
                listaEntidades.Add(i);
            }
            datagrid.ItemsSource = listaEntidades;
            datagrid.Items.Refresh();  
        }


        //Evento que bloquea el campo numero de contrato, ya que este es autogenerado.
        private void Txt_n_contrato_adm_contrato_Initialized(object sender, EventArgs e)
        {
            txt_n_contrato_adm_contrato.IsEnabled = false;
        }
        
        private void sinNumeroContrato()
        {
            lbl_error_buscar_contrato.Content = "Ingrese un numero de contrato";
        }

        //metodo que devuelve un contrato seleccionando una celda del grid contrato
        private void Dg_filtros_adm_contrato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OnBreak.Dato.Contrato contrato = (OnBreak.Dato.Contrato)dg_filtros_adm_contrato.SelectedItems[0];
                txt_n_contrato_adm_contrato.IsEnabled = false;
                txt_n_contrato_adm_contrato.Text = contrato.numero_contrato;
                dp_creacion_admin_contrato.SelectedDate = contrato.creacion_contrato;
                dp_termino_admin_contrato.SelectedDate = contrato.termino_contrato;
                dp_fecha_hora_inicio.SelectedDate = contrato.fecha_hora_inicio;
                dp_fecha_hora_termino.SelectedDate = contrato.fecha_hora_termino;
                txt_direccion_adm_contrato.Text = contrato.direccion_contrato;
                chk_vigencia_adm_contrato.IsChecked = contrato.estado_vigencia_contrato;
                VigenciaContrato(chk_vigencia_adm_contrato);
                //*
                txt_numero_evento_contrato.Text = contrato.tipo_evento.ToString();
                //*
                //cmb_tipo_adm_contrato.SelectedIndex = contrato.tipo_evento; 
                txt_observaciones_adm_cotrato.Text = contrato.Observaciones_contrato;
                txt_rut_contrato.Text = contrato.rut_cliente;
            }
            catch(Exception){ }
        }
        //Inicializar datagrid contratos
        private void Tab_admin_contratos_Initialized(object sender, EventArgs e)
        {
            mostrarGridContrato(dg_filtros_adm_contrato);
        }
        //boton limpiar:
        private void Btn_limpiar_adm_contrato_Click(object sender, RoutedEventArgs e)
        {
            //limpiarLblValidacionContrato();
            limpiarCamposContrato();/*
            txt_n_contrato_adm_contrato.Text = String.Empty;
            txt_direccion_adm_contrato.Text = String.Empty;
            chk_vigencia_adm_contrato.IsChecked = false;            
            txt_observaciones_adm_cotrato.Text = String.Empty;
            chk_vigencia_adm_contrato.IsEnabled = true;
            */
        }

        public Boolean porFiltro()
        {

            Boolean retorno = (chk_tipo_a.IsChecked.Equals(true) || chk_tipo_b.IsChecked.Equals(true) || chk_tipo_c.IsChecked.Equals(true)) ? true : false;
            return retorno;

        }
        private void Txt_rut_administracion_SelectionChanged(object sender, RoutedEventArgs e)
        {
            lbl_error_rut_administracion.Content  = string.Empty;
        }

        private void Btn_agregar_contrato_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            TabItem tabControl = main.tab_admin_contratos;
           // tabControl.MouseLeftButtonDown
            if (txt_rut_administracion.Text != string.Empty)
            {
                txt_rut_contrato.Text = txt_rut_administracion.Text;
            }
            else
            {
                lbl_Rut_Administracion.Content = "Debe buscar un rut";
            }
        }
        private void Btn_generar_numero_contrato_Click(object sender, RoutedEventArgs e)
        {
            txt_n_contrato_adm_contrato.Text = DateTime.Now.ToString("yyyyMMddHHmm");
        }        

        private void Btn_agregar_cliente_contrato_Click(object sender, RoutedEventArgs e)
        {
            AuxiliarWindows auxiliarWindows = new AuxiliarWindows(main_window);
            auxiliarWindows.Show();
            this.Visibility = Visibility.Collapsed;

            //var rtn = auxiliarWindows.rtnRut.rut_cliente.ToString();
            //txt_rut_contrato.Text = rtn;
        }

        private void Btn_rut_contrato_lis_contrato_Click(object sender, RoutedEventArgs e)
        {
            var tempcon = new Contrato();
            var templist = new List<Contrato>();

            var tipocEvento = new TipoEvento();

            var tipoeventolist = tipocEvento.ReadAll();



            if (txt_rut_contrato_lis_contrato.Text.Equals(""))
            {
                if (isFiltro())
                {
                    if (chk_tipo_a.IsChecked.Equals(true))
                    {
                        foreach (var item in tempcon.ReadAll())
                        {
                            var num = item.tipo_evento;
                            var stri = tipoeventolist.Find(nu => nu.tipo_evento == num).nombre_evento;
                            if (stri.Equals("de 1 a 20"))
                            {
                                templist.Add(item);
                            }
                        }

                    }
                    if (chk_tipo_b.IsChecked.Equals(true))
                    {
                        foreach (var item in tempcon.ReadAll())
                        {
                            var num = item.tipo_evento;
                            var stri = tipoeventolist.Find(nu => nu.tipo_evento == num).nombre_evento;
                            if (stri.Equals("de 21 a 50"))
                            {
                                templist.Add(item);
                            }
                        }

                    }
                    if (chk_tipo_c.IsChecked.Equals(true))
                    {
                        foreach (var item in tempcon.ReadAll())
                        {
                            var num = item.tipo_evento;
                            var stri = tipoeventolist.Find(nu => nu.tipo_evento == num).nombre_evento;
                            if (stri.Equals("más de 50"))
                            {
                                templist.Add(item);
                            }
                        }

                    }

                    dg_filtros_lis_contrato.ItemsSource = templist;
                }
                else
                {
                    dg_filtros_lis_contrato.ItemsSource = tempcon.ReadAll();

                }
            }
            else
            {
                var dato = txt_rut_contrato_lis_contrato.Text;


                dg_filtros_lis_contrato.ItemsSource = tempcon.ReadRut(dato);



            }
        }

        private bool isFiltro()
        {

            return (chk_tipo_a.IsChecked.Equals(true) || chk_tipo_b.IsChecked.Equals(true) || chk_tipo_c.IsChecked.Equals(true)) ? true : false;

        }

        private void Btn_calcular_adm_contrato_Click(object sender, RoutedEventArgs e)
        {
            ConfigEventoWindows configEvento = new ConfigEventoWindows(main_window);
            configEvento.Show();
            this.Visibility = Visibility.Collapsed;
            /*
            if (cmb_tipo_adm_contrato.SelectedValue.ToString() != "seleccionar")
            {
                ConfigEventoWindows configEvento = new ConfigEventoWindows(main_window);
                configEvento.Show();
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                lbl_error_tipo_contrato.Content = "Elija un tipo de evento";
            }
            */
        }

        private void Btn_rut_contrato_lis_contrato_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}






////ESTA ES LA ULTIMA VERSION EDITADA DEVDANGON ;) 
///08/07/2020 - ----