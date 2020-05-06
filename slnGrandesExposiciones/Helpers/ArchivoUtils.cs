using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace slnGrandesExposiciones.Helpers
{
    public class ArchivoUtils
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Crear carpeta y backup de archivos
        /// </summary>
        public static void RealizarBackupPresentaciones()
        {

            log.Info("Realizando backup presentaciones");

            if (!Directory.Exists(Configuracion.ObtenerCarpetaPresentacion()))
            {
                System.IO.Directory.CreateDirectory(Configuracion.ObtenerCarpetaPresentacion());
            }
            else
            {
                if (System.IO.File.Exists(Configuracion.ObtenerCarpetaPresentacion() + Configuracion.ObtenerNombreArchivoPresentacionCompras()))
                { System.IO.File.Copy(Configuracion.ObtenerCarpetaPresentacion() + Configuracion.ObtenerNombreArchivoPresentacionCompras(), Configuracion.ObtenerCarpetaPresentacionHistorico() + StringUtils.FechaHoraDelDiaFormatoYYYYMMDDHHMMSS() + Configuracion.ObtenerNombreArchivoPresentacionCompras(), true); }

                if (System.IO.File.Exists(Configuracion.ObtenerCarpetaPresentacion() + Configuracion.ObtenerNombreArchivoPresentacionVentas()))
                { System.IO.File.Copy(Configuracion.ObtenerCarpetaPresentacion() + Configuracion.ObtenerNombreArchivoPresentacionVentas(), Configuracion.ObtenerCarpetaPresentacionHistorico() + StringUtils.FechaHoraDelDiaFormatoYYYYMMDDHHMMSS() + Configuracion.ObtenerNombreArchivoPresentacionVentas(), true); }
            }

            log.Debug("Backup realizado correctamente.");
        }

        /// <summary>
        /// Agrega un registro 
        /// </summary>
        /// <param name="contenido"></param>
        /// <param name="rutaArchivo"></param>
        /// <param name="sobrescribir"></param>
        public static void EscribeEnArchivo(string contenido, string rutaArchivo, bool sobrescribir = false)
        {
            StreamWriter sw = new StreamWriter(rutaArchivo, !sobrescribir);
            sw.Write(contenido);
            sw.Close();
        }

    }
}