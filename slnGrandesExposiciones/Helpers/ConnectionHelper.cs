using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using Com.Bgba.Arqtec.Contextdelivery;
using System.Web.Configuration;
using System.Web;
using slnGrandesExposiciones.Models;

namespace slnGrandesExposiciones.Helpers
{
    public class ConnectionHelper
    {
        public static string CreateConnectionString()
        {
            string metaData = WebConfigurationManager.AppSettings["metaData"];
            string dataSource = WebConfigurationManager.AppSettings["dataSource"];
            string initialCatalog = WebConfigurationManager.AppSettings["initialCatalog"];

            const string appName = "EntityFramework";
            const string providerName = "System.Data.SqlClient";

            string AppPath = HttpContext.Current.Server.MapPath("~/Application.xml");
            ContextDelivery context = new ContextDelivery(AppPath);
            string ContextDeliveryApplicationAlias = WebConfigurationManager.AppSettings["ContextDeliveryApplicationAlias"];
            string ContextDeliveryResourceAlias = WebConfigurationManager.AppSettings["ContextDeliveryResourceAlias"];
            string ContextSet = WebConfigurationManager.AppSettings["ContextEnvironment"];

            User usuario = context.GetUser(
                ContextDeliveryApplicationAlias,
                ContextDeliveryResourceAlias, ContextSet
                );

            string nombre = usuario.GetUserName();
            string password = usuario.GetPassword();

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = dataSource;
            sqlBuilder.InitialCatalog = initialCatalog;
            sqlBuilder.MultipleActiveResultSets = true;
            sqlBuilder.IntegratedSecurity = false;
            sqlBuilder.UserID = nombre;
            sqlBuilder.Password = password;
            sqlBuilder.ApplicationName = appName;

            EntityConnectionStringBuilder efBuilder = new EntityConnectionStringBuilder();
            efBuilder.Metadata = metaData;
            efBuilder.Provider = providerName;
            efBuilder.ProviderConnectionString = sqlBuilder.ConnectionString;

            return efBuilder.ConnectionString;
        }
        public static GrandesExposicionesEntities CreateDbContextConnection()
        {
            //return new GrandesExposicionesEntities(ConnectionHelper.CreateConnectionString());
            return new GrandesExposicionesEntities();
        }

        public static string ConectarSQL()
        {
            SqlConnection cn = new SqlConnection();
            string dataSource = WebConfigurationManager.AppSettings["dataSource"];
            string initialCatalog = WebConfigurationManager.AppSettings["initialCatalog"];

            const string appName = "EntityFramework";
            const string providerName = "System.Data.SqlClient";

            string AppPath = HttpContext.Current.Server.MapPath("~/Application.xml");
            ContextDelivery context = new ContextDelivery(AppPath);
            string ContextDeliveryApplicationAlias = WebConfigurationManager.AppSettings["ContextDeliveryApplicationAlias"];
            string ContextDeliveryResourceAlias = WebConfigurationManager.AppSettings["ContextDeliveryResourceAlias"];
            string ContextSet = WebConfigurationManager.AppSettings["ContextEnvironment"];

            User usuario = context.GetUser(
                ContextDeliveryApplicationAlias,
                ContextDeliveryResourceAlias, ContextSet
                );

            string nombre = usuario.GetUserName();
            string password = usuario.GetPassword();

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = dataSource;
            sqlBuilder.InitialCatalog = initialCatalog;
            sqlBuilder.IntegratedSecurity = false;
            sqlBuilder.UserID = nombre;
            sqlBuilder.Password = password;

            

            return sqlBuilder.ConnectionString;
        }


        public static string GetUserName (){

            string AppPath = HttpContext.Current.Server.MapPath("~/ApplicationImpersonate.xml");
            ContextDelivery context = new ContextDelivery(AppPath);
            string ContextDeliveryApplicationAlias = WebConfigurationManager.AppSettings["ContextDeliveryApplicationAliasImpersonate"];
            string ContextDeliveryResourceAlias = WebConfigurationManager.AppSettings["ContextDeliveryResourceAliasImpersonate"];
            string ContextSet = WebConfigurationManager.AppSettings["ContextEnvironment"];
                
           User usuario = context.GetUser(ContextDeliveryApplicationAlias,ContextDeliveryResourceAlias, ContextSet);

            return usuario.GetUserName();
            }

        public static string GetUserPassword()
        {

            string AppPath = HttpContext.Current.Server.MapPath("~/ApplicationImpersonate.xml");
            ContextDelivery context = new ContextDelivery(AppPath);
            string ContextDeliveryApplicationAlias = WebConfigurationManager.AppSettings["ContextDeliveryApplicationAliasImpersonate"];
            string ContextDeliveryResourceAlias = WebConfigurationManager.AppSettings["ContextDeliveryResourceAliasImpersonate"];
            string ContextSet = WebConfigurationManager.AppSettings["ContextEnvironment"];

            User usuario = context.GetUser(ContextDeliveryApplicationAlias, ContextDeliveryResourceAlias, ContextSet);

            return usuario.GetPassword();
        }

    }
}
