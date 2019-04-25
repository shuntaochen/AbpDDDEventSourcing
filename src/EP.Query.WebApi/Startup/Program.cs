using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EP.Commons.ConfigClient;
using EP.Commons.Core;
using Newtonsoft.Json.Linq;
using EP.Commons.Core.Extensions;
using EP.Commons.Core.Configuration;

namespace EP.Query.WebApi.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args).AddCommandLineToCustomConfiguation(System.Environment.CurrentDirectory);

            var serverAddress = new Uri(configuration["App:ServerRootAddress"]);
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls($"http://*:{(serverAddress.Port)}")
                .Build();
        }
    }
}
