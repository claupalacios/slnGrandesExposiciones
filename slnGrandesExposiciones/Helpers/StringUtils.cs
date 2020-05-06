using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace slnGrandesExposiciones.Helpers
{
    public class StringUtils
    {
        /// <summary>
        /// Metodo que retorna la fecha hora del dia en el formato YYYYMMDDHHMMSS
        /// </summary>
        /// <returns></returns>
        public static string FechaHoraDelDiaFormatoYYYYMMDDHHMMSS()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0'); ;
        }


        /// <summary>
        /// Método para formatear los importes
        /// </summary>
        /// <param name="importe"></param>
        /// <param name="longitudEntero"></param>
        /// <param name="longitudDecimal"></param>
        /// <returns></returns>
        public static string FormatearImporte(string importe, int longitudEntero, int longitudDecimal)
        {
            string valor = Convert.ToString(importe) != string.Empty ? Convert.ToString(importe) : "0"; //Si es "" || null es 0
            string numero = valor;
            ulong numEntero = ulong.Parse(numero.Split('.')[0]); //Separo el número entero del decimal
            float numDecimal = float.Parse(numero.Split('.')[1]); //Separo el número decimal del entero
            importe = Convert.ToString(numEntero).PadLeft(longitudEntero, '0') + Convert.ToString(numDecimal).PadRight(longitudDecimal, '0');

            return importe;
        }

    }
}