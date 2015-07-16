using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace UniRitter.UniRitter2015.SelfHosted
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (StartApi())
            {
                Console.ReadLine(); 
            }
        }

        public static IDisposable StartApi()
        {
            string baseAddress = "http://localhost:9000/";
            
            Console.WriteLine("Starting server at {0}. Hit any key to stop it.", baseAddress);
            return WebApp.Start<Startup>(url: baseAddress);
        }
    }
}
