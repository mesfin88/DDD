using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

using Boven.Areas.WData.IdentityDB;
using Boven.Areas.WData.BovenDB;

namespace Boven
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            Program.InitializeDatabase(host);
            Program.InitializeIdentity(host);
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();


        private static void InitializeIdentity(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    ISeedIdentity seedIdentity = services.GetRequiredService<ISeedIdentity>();
                    seedIdentity.EnsurePopulated(services).Wait();
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occured while seeding the Identity DB.");
                }
            }
        }

        private static void InitializeDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    //Only dbcontext needs to be sent, not the whole IServiceProvider
                    //Loosly coupled. We dont have to know the implementation of ISeedData, impl is dependency injected instead.
                    BovenContext bovenContext = services.GetRequiredService<BovenContext>();
                    ISeedBoven seedBoven = services.GetRequiredService<ISeedBoven>();
                    seedBoven.EnsurePopulated(bovenContext);
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occured while seeding the database.");
                }
            }
        }



    }
}
