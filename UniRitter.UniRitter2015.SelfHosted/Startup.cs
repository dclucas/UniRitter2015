using System.Collections.Generic;
using Owin;
using System.Web.Http;
using UniRitter.UniRitter2015.SelfHosted.Support;

namespace UniRitter.UniRitter2015.SelfHosted
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new MyHttpConfiguration();
            app.UseWebApi(config);
        }
    }
}