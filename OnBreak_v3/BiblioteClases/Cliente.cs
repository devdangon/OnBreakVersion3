using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.Negocio;
using OnBreak.Dato;
using System.Data.Entity.Validation;
using System.Data.Entity.Core;

namespace OnBreak
{
    public class Cliente : ICRUD
    {
        public string rut_cliente { get; set; }
        public string razon_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string mail_cliente { get; set; }
        public string direccion_cliente { get; set; }
        public int telefono_cliente { get; set; } 
        public int actividad_cliente { get; set; }
        public int tipo_cliente { get; set; }

        public Cliente()
        {
            init();
        }
        private void init()
        {
            rut_cliente = string.Empty;
            razon_cliente = string.Empty;
            nombre_cliente = string.Empty;
            mail_cliente = string.Empty;
            direccion_cliente = string.Empty;
            telefono_cliente = 0;
            actividad_cliente = 0;
            tipo_cliente = 0;
        }
        public enum TipoEmpresa
        {
            SELECCIONAR,
            SPA,
            EIRL,
            LIMITADA,
            SA
        }
        public enum ActividadEmpresa
        {
            SELECCIONAR,
            AGROPECUARIA,
            MINERIA,
            MANUFACTURA,
            COMERCIO,
            HOTELERIA,
            ALIMENTOS,
            TRANSPORTES,
            SERVICIOS
        }
        public bool Create()
        {
            //Objeto tipo Modelo de Entidades.
            Dato.Cliente datoCliente = new Dato.Cliente();
            OnBreakEntities entidades = new OnBreakEntities();
            //Obejto tipo entidad Cliente.
            try
            {
                CommonBC.Sincronyze(this, datoCliente);

                entidades.Cliente.Add(datoCliente);
                entidades.SaveChanges();
                return true;
            }
            catch (MetadataException ex)
            {
                entidades.Cliente.Remove(datoCliente);
                Console.Write(ex);
                return false;
            }
        }
       

        public bool Read()
        {
            bool status = false;            
            Dato.OnBreakEntities entidades = new Dato.OnBreakEntities();
            try
            {
                Dato.Cliente datoCliente = entidades.Cliente.First(cli => cli.rut_cliente == rut_cliente);
                datoCliente.GetType().GetProperties();
                CommonBC.Sincronyze(datoCliente, this);
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public List<ClienteDataGrid> ReadAll()
        {
            //var
            var tmplist = new List<ClienteDataGrid>();
            Dato.OnBreakEntities entidades = new Dato.OnBreakEntities();
            //Logic
            foreach (var item in entidades.Cliente)
            {                
                var tmpclidata = new ClienteDataGrid();
                tmpclidata = Sincro(item, tmpclidata, entidades);
                //CommonBC.Sincronyze(item, tmpcli);  
                tmplist.Add(tmpclidata);
                //var cla = entidades.TipoEmpresa.Find(1);
            }
            //rtn
            return tmplist;
        }

        private ClienteDataGrid Sincro(Dato.Cliente orign, ClienteDataGrid dest, Dato.OnBreakEntities entidades )
        {
            dest.nombre_cliente = orign.nombre_cliente;
            dest.mail_cliente = orign.mail_cliente;
            dest.razon_cliente= orign.razon_cliente;
            dest.rut_cliente = orign.rut_cliente;
            dest.telefono_cliente = orign.telefono_cliente;
            dest.direccion_cliente = orign.direccion_cliente;
            dest.tipo_cliente = entidades.TipoEmpresa.Find(orign.tipo_cliente).Descripcion.ToString();
            dest.actividad_cliente = entidades.ActividadEmpresa.Find(orign.actividad_cliente).Descripcion.ToString();

            return dest;
        }

        public bool Update()
        {
            OnBreakEntities entidades = new OnBreakEntities();
            Dato.Cliente datoCliente = new Dato.Cliente();
            //Obejto tipo entidad Cliente.
            try
            {
                datoCliente = entidades.Cliente.First(cli => cli.rut_cliente == rut_cliente);
                CommonBC.Sincronyze(this, datoCliente);
                entidades.SaveChanges();
                return true;
            }
            catch (MetadataException ex)
            {
                entidades.Cliente.Remove(datoCliente);
                Console.Write(ex);
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                OnBreakEntities entidades = new OnBreakEntities();
                Dato.Cliente cliente = entidades.Cliente.First(cli => cli.rut_cliente == rut_cliente);
                try
                {
                    Dato.Contrato contrato = entidades.Contrato.First(con => con.rut_cliente == cliente.rut_cliente);
                    return false;
                }
                catch
                {
                    entidades.Cliente.Remove(cliente);
                    entidades.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ValidarRut(string rut)
        {
            Dato.Cliente cliente = new Dato.Cliente();
            OnBreakEntities entidades = new OnBreakEntities();
            bool status = true;
            try
            {
                foreach (Dato.Cliente dato in entidades.Cliente)
                {
                    if (rut.Equals(dato.rut_cliente))
                    {
                        status = false;
                        break;
                    }
                }

            }
            catch (Exception)
            {
                status = true;
            }
            return status;
        }
    }
}
