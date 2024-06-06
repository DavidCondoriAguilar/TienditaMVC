using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace serviciosrest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //este es el metodo que ejecuta la aplicacion
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //creamos un procedimiento para configurar la ruta
        public static void Register(HttpConfiguration config)
        {
            //Rutas de la Api
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api-tiendita/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
        }
    }
}