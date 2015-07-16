using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
            string baseAddress = "http://localhost:49556/";

            WebApp.Start<Startup>(url: baseAddress);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
        }
    }
}
