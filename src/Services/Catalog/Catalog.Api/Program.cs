﻿using Catalog.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Catalog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build()
                .MigrateDbContext<CatalogContext>((context, services) =>
                {
                    var env = services.GetService<IHostingEnvironment>();
                    var settings = services.GetService<IOptions<CatalogSettings>>();
                    var logger = services.GetService<ILogger<CatalogContextSeed>>();

                    new CatalogContextSeed()
                        .SeedAsync(context, env, settings, logger)
                        .Wait();
                })
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot("Pics")
                .UseStartup<Startup>();
    }
}
