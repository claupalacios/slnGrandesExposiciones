//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace slnGrandesExposiciones.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Archivos_Exposiciones
    {
        public int ID { get; set; }
        public int ID_PERIODO { get; set; }
        public int TIPOTABLA { get; set; }
        public string NOMBRE { get; set; }
        public byte[] CONTENIDO { get; set; }
    
        public virtual Tablas Tablas { get; set; }
        public virtual Periodos Periodos { get; set; }
    }
}
