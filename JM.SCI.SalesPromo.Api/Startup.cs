using Microsoft.Practices.Unity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity.WebApi;

namespace JM.SCI.SalesPromo.Api
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            //cors configuration 
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            //Create the unity container
            var container = UnityConfig.GetContainer();
            config.DependencyResolver = new UnityDependencyResolver(container);

            // api configuration
            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "api/{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );
            appBuilder.UseWebApi(config);
           
        }
    }
}
