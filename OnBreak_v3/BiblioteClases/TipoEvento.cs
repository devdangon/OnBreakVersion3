using System;
using OnBreak.Dato;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core;

namespace OnBreak.Negocio
{
    public class TipoEvento : ICRUD
    {
        public int tipo_evento { get; set; }
        public int asistente { get; set; }
        public double valor_personal_adicional { get; set; }
        public int valor_asistente_adicional { get; set; }


        public TipoEvento()
        {            
            init();
        }
        //2do contructor para usar en el grid:
        public TipoEvento(int adiAsis, int adiPer)
        {
            adiAsis = 0;
            adiPer = 0;
        }
        private void init()
        {
            tipo_evento = 0;
            valor_personal_adicional = 0;
            valor_asistente_adicional = 0;

        }

        public bool Create()
        {
            //Objeto tipo Modelo de Entidades.
            Dato.TipoEvento datoTipoEvento = new Dato.TipoEvento();
            OnBreakEntities entidades = new OnBreakEntities();
            //Obejto tipo entidad Cliente.
            try
            {
                CommonBC.Sincronyze(this, datoTipoEvento);
                entidades.TipoEvento.Add(datoTipoEvento);
                entidades.SaveChanges();
                return true;
            }
            catch (MetadataException ex)
            {
                entidades.TipoEvento.Remove(datoTipoEvento);
                Console.Write(ex);
                return false;
            }
        }
        public List<TipoEvento> ReadAll()
        {
            var list = new List<TipoEvento>();
            Dato.OnBreakEntities entidades = new Dato.OnBreakEntities();
            try
            {
                //Dato.TipoEvento datoTipoEvento = entidades.TipoEvento.First(tip => tip.tipo_evento == tipo_evento);
                //datoTipoEvento.GetType().GetProperties();
                foreach (var item in entidades.TipoEvento)
                {
                    var tmptipoevento = new TipoEvento();
                    //tmpclidata = Sincro(item, tmpclidata, entidades);
                    CommonBC.Sincronyze(item, tmptipoevento);
                    list.Add(tmptipoevento);
                    //var cla = entidades.TipoEmpresa.Find(1);
                }
                //CommonBC.Sincronyze(datoTipoEvento, this);
                
            }
            catch (Exception)
            {
                
            }


            return list;
        }
        public bool Read()
        {
            bool status = false;
            Dato.OnBreakEntities entidades = new Dato.OnBreakEntities();
            try
            {
                Dato.TipoEvento datoTipoEvento = entidades.TipoEvento.First(tip => tip.tipo_evento == tipo_evento);
                datoTipoEvento.GetType().GetProperties();
                CommonBC.Sincronyze(datoTipoEvento, this);
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
            throw new NotImplementedException();
        }

        public bool Delete()
        {
           
            var entidades = new OnBreakEntities();
            try
            {
                var tipoEvento = entidades.TipoEvento.First(tev => tev.tipo_evento == tipo_evento);
                entidades.TipoEvento.Remove(tipoEvento);
                entidades.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
