using System.Collections.Generic;
using System.Reflection;
using Owin;
using System.Web.Http;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.SelfHosted.Support;
using UniRitter.UniRitter2015.Services;
using UniRitter.UniRitter2015.Services.Implementation;

namespace UniRitter.UniRitter2015.SelfHosted
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new MyHttpConfiguration();
            app.UseWebApi(config);
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);
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