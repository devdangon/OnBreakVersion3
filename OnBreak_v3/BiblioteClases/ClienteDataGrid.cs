using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak
{
    public class ClienteDataGrid
    {
        public string rut_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string actividad_cliente { get; set; }
        public string tipo_cliente { get; set; }
        public string razon_cliente { get; set; }
        public string mail_cliente { get; set; }
        public string direccion_cliente { get; set; }
        public int telefono_cliente { get; set; }
        

        public ClienteDataGrid()
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
            actividad_cliente = string.Empty; 
            tipo_cliente = string.Empty; 
        }
    }
}
