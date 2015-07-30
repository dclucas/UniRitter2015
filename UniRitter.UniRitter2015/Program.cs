using System;
using Microsoft.Owin.Hosting;
using UniRitter.UniRitter2015.Support;

namespace UniRitter.UniRitter2015
{
    public class Program
    {
        private static void Main(string[] args)
        {
            using (StartApi())
            {
                Console.ReadLine();
            }
        }

        public static IDisposable StartApi()
        {
            var cfg = (IApiConfig)Startup.kernel.GetService(typeof(IApiConfig));
            // todo: also get base dns/ip from config
            var baseAddress = cfg.FullUrl;

            Console.WriteLine("Starting server at {0}. Hit any key to stop it.", baseAddress);
            return WebApp.Start<Startup>(baseAddress);
        }
    }
}