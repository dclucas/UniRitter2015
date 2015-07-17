using Microsoft.Owin.Hosting;
using TechTalk.SpecFlow;
using UniRitter.UniRitter2015.SelfHosted;

namespace UniRitter.UniRitter2015.Specs
{
    [Binding]
    public sealed class GlobalHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var baseAddress = "http://localhost:49556/";

            WebApp.Start<Startup>(baseAddress);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
        }
    }
}