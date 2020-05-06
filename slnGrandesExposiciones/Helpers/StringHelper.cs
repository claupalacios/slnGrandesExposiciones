using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace slnGrandesExposiciones.Helpers
{
    public class StringHelper
    {
        /// <summary>
        /// Metodo que retorna la fecha hora del dia en el formato YYYYMMDDHHMMSS
        /// </summary>
        /// <returns></returns>
        public static string FechaHoraDelDiaFormatoYYYYMMDDHHMMSS()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0'); ;
        }
        
        public static string FormatearImporte(double? valor)
        {
            string importe = string.Empty;
            string importe_aux = new string(valor.ToString().Reverse().ToArray());

            for (int i = 0; i < importe_aux.Length; i++)
            {
                importe += importe_aux.ToString()[i];

                if ((((i + 1) % 3) == 0) && ((i + 1) < importe_aux.Length))
                {
                    importe += ".";
                }
            }

            return new string(importe.Reverse().ToArray());
        }
    }
}