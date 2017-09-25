using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Server;

namespace QueryAnalyzer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = new WebHostBuilder()
            //    .UseKestrel()
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .Build();
            var host = new WebHostBuilder()
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseStartup<Startup>()
             //.UseWebListener()
             .UseKestrel()
             .UseIISIntegration()
             .Build();

            host.Run();


        }
    }
}
