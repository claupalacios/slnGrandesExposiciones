using System;
using System.ComponentModel.DataAnnotations;

namespace slnGrandesExposiciones.Dtos
{ 
    public class ExposicionesDto
    {
        public int Id { get; set; }
        public int ID_PERIODO { get; set; }
        public DateTime FECHA_ALTA { get; set; }
        public DateTime FECHA_MOD { get; set; }

        public string Legajo { get; set; }
        public bool Validado { get; set; }
    }

}