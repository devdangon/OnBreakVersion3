using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.Dato;

namespace OnBreak
{
    public class ModalidadServicio : ICRUD
    {
        public int IdModalidad { get; set; }
        public string nombre_evento { get; set; }
        public double valor_base { get; set; }
        public int personal_base { get; set; }
        public int tipo_evento { get; set; }

        public ModalidadServicio()
        {
            IdModalidad = 0;
            nombre_evento = string.Empty;
            valor_base = 0;
            personal_base = 0;
            tipo_evento = 0;
        }

        public bool Create()
        {
            //Objeto tipo Modelo de Entidades.
            Dato.ModalidadServicio modalidadServicio = new Dato.ModalidadServicio();
            OnBreakEntities entidades = new OnBreakEntities();
            //Obejto tipo entidad Cliente.
            try
            {
                CommonBC.Sincronyze(this, modalidadServicio);

                entidades.ModalidadServicio.Add(modalidadServicio);
                entidades.SaveChanges();
                return true;
            }
            catch (MetadataException ex)
            {
                entidades.ModalidadServicio.Remove(modalidadServicio);
                Console.Write(ex);
                return false;
            }
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }
    }
}
