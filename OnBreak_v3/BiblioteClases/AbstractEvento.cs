using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak
{
    public abstract class AbstractEvento
    {

        public int personal_base { get; set; }
        public int valor_base { get; set; }
        public int asistentes_adicionales { get; set; }
        public int personal_adicional { get; set; }

        public virtual float GetValorTotalEvento()
        {
            float total=0;
            return total;
        } 
        public virtual float RecargoAsistentes()
        {
            float recargo=0;
            return recargo;
        }
        public virtual float RecargoPersonal()
        {
            float recargo=0;
            return recargo;
        }
        public virtual bool AlimentacionVegetarina(bool veg)
        {
            return veg;
        }




    }
}
