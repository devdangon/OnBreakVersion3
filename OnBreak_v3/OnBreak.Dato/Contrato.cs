//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnBreak.Dato
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contrato
    {
        public string rut_cliente { get; set; }
        public string numero_contrato { get; set; }
        public System.DateTime creacion_contrato { get; set; }
        public System.DateTime termino_contrato { get; set; }
        public System.DateTime fecha_hora_inicio { get; set; }
        public System.DateTime fecha_hora_termino { get; set; }
        public string direccion_contrato { get; set; }
        public bool estado_vigencia_contrato { get; set; }
        public int tipo_evento { get; set; }
        public string Observaciones_contrato { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual TipoEvento TipoEvento { get; set; }
    }
}
