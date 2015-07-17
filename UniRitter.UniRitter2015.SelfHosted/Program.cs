using System;
using Microsoft.Owin.Hosting;

namespace UniRitter.UniRitter2015.SelfHosted
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
            var baseAddress = "http://localhost:9000/";

            Console.WriteLine("Starting server at {0}. Hit any key to stop it.", baseAddress);
            return WebApp.Start<Startup>(baseAddress);
        }
    }
}