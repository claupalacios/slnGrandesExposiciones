using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace slnGrandesExposiciones.Helpers
{
    public class Configuracion
    {




        public static string ObtenerCarpetaPresentacion()
        {
            return @ConfigurationManager.AppSettings["CarpetaPresentaciones"];
        }

        public static string ObtenerCarpetaPresentacionHistorico()
        {
            return @ConfigurationManager.AppSettings["CarpetaPresentacionesHistorico"];
        }

        public static string ObtenerNombreArchivoPresentacionCompras()
        {
            return ConfigurationManager.AppSettings["archivoPresentacionCompras"];
        }

        public static string ObtenerNombreArchivoPresentacionVentas()
        {
            return ConfigurationManager.AppSettings["archivoPresentacionVentas"];
        }

        public static string ObtenerNombreArchivoNuevaRectificativa()
        {
            return ConfigurationManager.AppSettings["archivoNuevaRectificativa"];
        }

        public static string ObtenerNombreArchivoCierrePeriodo()
        {
            return ConfigurationManager.AppSettings["archivoCierrePeriodo"];
        }

        public static string ObtenerRutaArchivoBATAjustes()
        {
            return ConfigurationManager.AppSettings["rutaBATAjustes"];
        }

        public static string ObtenerRutaArchivoBATRectificativa()
        {
            return ConfigurationManager.AppSettings["rutaBATRectificactiva"];
        }

    }
}