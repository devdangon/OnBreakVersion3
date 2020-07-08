using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak
{
    public class CoffeeBreak : AbstractEvento 
    {
        public override float GetValorTotalEvento()
        {
            return base.GetValorTotalEvento();
        }
        public override float RecargoAsistentes()
        {
            return base.RecargoAsistentes();
        }
        public override float RecargoPersonal()
        {
            return base.RecargoPersonal();
        }
    }
}
