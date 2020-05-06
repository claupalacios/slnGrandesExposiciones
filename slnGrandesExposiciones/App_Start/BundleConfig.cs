using System.Web;
using System.Web.Optimization;

namespace slnGrandesExposiciones
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /////////////////Original////////////////
            /*
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            */

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/moment.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Detalles").Include(
                      "~/Scripts/detalles.js"));

            bundles.Add(new ScriptBundle("~/Scripts/funciones_adicionales").Include(
                      "~/Scripts/funciones_adicionales.js"));

            bundles.Add(new StyleBundle("~/Content").Include(
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/bootstrap.css",
                      "~/Content/Layout.css",
                      "~/Content/Images/Logos/Logo+Galicia.jpg",
                      "~/Content/PagedList.css"));

            bundles.Add(new StyleBundle("~/CSS/Detalles").Include(
                "~/Content/Detalles.css",
                "~/Content/Table_Detalles.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
