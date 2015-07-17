using System.Reflection;
using System.Web.Http;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services;
using UniRitter.UniRitter2015.Services.Implementation;

namespace UniRitter.UniRitter2015.SelfHosted
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = new HttpConfiguration();
            webApiConfiguration.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}",
                new {id = RouteParameter.Optional, controller = "values"});

            webApiConfiguration.Formatters.Remove(webApiConfiguration.Formatters.XmlFormatter);

            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(webApiConfiguration);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Bind<IRepository<PersonModel>>().To<MongoPersonRepository>();

            return kernel;
        }
    }
}