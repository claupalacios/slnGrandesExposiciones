using System;
using System.ComponentModel.DataAnnotations;

namespace slnGrandesExposiciones.Dtos
{ 
    public class ParametrosDto
    {
        public int Id { get; set; }
        public int ID_PERIODO { get; set; }
        public double TIER_1 { get; set; }
        public double TIER_2 { get; set; }
        public double TIER_3 { get; set; }

        public double SMVM { get; set; }
    }

}