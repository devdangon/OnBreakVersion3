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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contrato()
        {
            this.Cenas = new HashSet<Cenas>();
            this.Cocktail = new HashSet<Cocktail>();
            this.Coffee = new HashSet<Coffee>();
        }
    
        public string rut_cliente { get; set; }
        public string numero_contrato { get; set; }
        public System.DateTime creacion_contrato { get; set; }
        public System.DateTime termino_contrato { get; set; }
        public System.DateTime fecha_hora_inicio { get; set; }
        public System.DateTime fecha_hora_termino { get; set; }
        public string direccion_contrato { get; set; }
        public bool estado_vigencia_contrato { get; set; }
        public int tipo_evento { get; set; }
        public string observaciones_contrato { get; set; }
        public int personal_adicional { get; set; }
        public int asistentes_adicional { get; set; }
        public bool estado_contrato { get; set; }
        public double valor_total_evento { get; set; }
        public int IdModalidad { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cenas> Cenas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cocktail> Cocktail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Coffee> Coffee { get; set; }
        public virtual ModalidadServicio ModalidadServicio { get; set; }
    }
}
