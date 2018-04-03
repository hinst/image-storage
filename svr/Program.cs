using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace image_storage
{
    public class Program
    {
        public const string WebRoot = "/images";
        public static readonly string AppDir = Directory.GetCurrentDirectory();

        public static void Main(string[] args)
        {
            if (args.Length > 0) {
                var app = new Program();
                app.Run(args);
            } else {
                var host = BuildWebHost(args);
                host.Run();
            }
        }

        public void Run(string[] args) {
            if (args[0] == "loadFiles") {
                var loader = new FileLoader();
                loader.Dir = args[1];
                loader.Run();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((hosting, log) => {
                    log.ClearProviders();
                    log.AddConfiguration(hosting.Configuration.GetSection("Logging"));
                }).UseNLog()
                .Build();
    }
}
