﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Ourstudio.IdentityServer.EntityFramework.Storage;
using Microsoft.EntityFrameworkCore;
using Ourstudio.IdentityServer.EntityFramework;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "server=(localdb)\\mssqllocaldb;database=Ourstudio.IdentityServer.EntityFramework;trusted_connection=yes;";

            var services = new ServiceCollection();
            services.AddLogging(b => b.AddConsole().SetMinimumLevel(LogLevel.Trace));
            services.AddOperationalDbContext(options =>
            {
                options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString);

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = false;
                options.TokenCleanupInterval = 5; // interval in seconds, short for testing
            });

            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var svc = scope.ServiceProvider.GetRequiredService<TokenCleanupService>();
                svc.RemoveExpiredGrantsAsync().GetAwaiter().GetResult();
            }
        }
    }
}
