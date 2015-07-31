using System.Reflection;
using System.Web.Http;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;
using UniRitter.UniRitter2015.Services.Implementation;
using UniRitter.UniRitter2015.Support;
using System.Web.Http.ExceptionHandling;

namespace UniRitter.UniRitter2015
{
    public class Startup
    {
        public static IKernel kernel { get; private set; }

        static Startup()
        {
            CreateKernel();
        }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}",
                new {id = RouteParameter.Optional, controller = "values"});
            
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Services.Add(typeof(IExceptionLogger), new NLogExceptionLogger());
            app.UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);
        }

        private static void CreateKernel()
        {
            kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Bind<IApiConfig>().To<ApiConfig>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(MongoRepository<>));
        }
    }
}
