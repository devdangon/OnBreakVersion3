using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak
{
    public interface ICRUD
    {
        bool Create();
        bool Read();
        bool Update();
        bool Delete();

    }    
}
