using OnBreak.Dato;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class Contrato : ICRUD
    {        
        public string rut_cliente { get; set; }
        public string numero_contrato { get; set; }
        public DateTime creacion_contrato { get; set; }
        public DateTime termino_contrato { get; set; }
        public DateTime fecha_hora_inicio { get; set; }
        public DateTime fecha_hora_termino { get; set; }
        public String direccion_contrato { get; set; }
        public bool estado_vigencia_contrato { get; set; }
        public int tipo_evento { get; set; }
        public string Observaciones_contrato { get; set; }

        public Contrato()
        {
            init();
        }
        private void init()
        {
            rut_cliente = string.Empty;
            numero_contrato = string.Empty;
            creacion_contrato = DateTime.Today;
            termino_contrato = DateTime.Today;
            fecha_hora_inicio = DateTime.Today;
            fecha_hora_termino = DateTime.Today;
            direccion_contrato = String.Empty;
            estado_vigencia_contrato = true;
            tipo_evento = 0;
            Observaciones_contrato = String.Empty;
           
        }

        public bool Create()
        {
            //Objeto tipo Modelo de Entidades.
            Dato.Contrato datoContrato = new Dato.Contrato();
            OnBreakEntities entidades = new OnBreakEntities();
            //Obejto tipo entidad Cliente.
            try
            {
                CommonBC.Sincronyze(this, datoContrato);

                entidades.Contrato.Add(datoContrato);
                entidades.SaveChanges();
                return true;
            }
            catch (MetadataException ex)
            {
                entidades.Contrato.Remove(datoContrato);
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
                Dato.Contrato datoContrato = entidades.Contrato.First(con => con.numero_contrato == numero_contrato);
                datoContrato.GetType().GetProperties();
                CommonBC.Sincronyze(datoContrato, this);
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool Update()
        {
            OnBreakEntities entidades = new OnBreakEntities();
            Dato.Contrato datoContrato = new Dato.Contrato();
            //Obejto tipo entidad Cliente.
            try
            {
                datoContrato = entidades.Contrato.First(cli => cli.numero_contrato == numero_contrato);
                CommonBC.Sincronyze(this, datoContrato);
                entidades.SaveChanges();
                return true;
            }
            catch (MetadataException ex)
            {
                entidades.Contrato.Remove(datoContrato);
                Console.Write(ex);
                return false;
            }
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }
        public bool ValidarNumeroContrato()
        {
            Dato.Contrato contrato = new Dato.Contrato();
            OnBreakEntities entidades = new OnBreakEntities();
            bool status = true;
            try
            {
                foreach (Dato.Contrato dato in entidades.Contrato)
                {
                    if (this.numero_contrato.Equals(dato.numero_contrato))
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


        public List<Contrato> ReadAll()
        {
            //var
            var tmplist = new List<Contrato>();
            Dato.OnBreakEntities entidades = new Dato.OnBreakEntities();
            //Logic
            foreach (var item in entidades.Contrato)
            {
                var tmpclidata = new Contrato();
                //tmpclidata = Sincro(item, tmpclidata, entidades);
                CommonBC.Sincronyze(item, tmpclidata);
                tmplist.Add(tmpclidata);
                //var cla = entidades.TipoEmpresa.Find(1);
            }
            //rtn
            return tmplist;
        }

        public List<Contrato> ReadRut(string var)
        {
            var tmplist = new List<Contrato>();

            Dato.OnBreakEntities entidades = new Dato.OnBreakEntities();
            //var con = new Contrato();
            try
            {

                Dato.Contrato datoContrato = entidades.Contrato.First(cont => cont.rut_cliente == var || cont.numero_contrato == var);
                datoContrato.GetType().GetProperties();
                CommonBC.Sincronyze(datoContrato, this);
                tmplist.Add(this);
                //status = true;
            }
            catch (Exception)
            {
                //status = false;
            }
            return tmplist;
        }

        public List<Contrato> ReadFiltro(int var)
        {
            var tmplist = new List<Contrato>();

            Dato.OnBreakEntities entidades = new Dato.OnBreakEntities();
            //var con = new Contrato();

            try
            {


                Dato.Contrato datoContrato = entidades.Contrato.First(cont => cont.tipo_evento == var);
                datoContrato.GetType().GetProperties();
                CommonBC.Sincronyze(datoContrato, this);
                tmplist.Add(this);
                //status = true;
            }
            catch (Exception)
            {
                //status = false;
            }
            return tmplist;

        }

        
    }
}

