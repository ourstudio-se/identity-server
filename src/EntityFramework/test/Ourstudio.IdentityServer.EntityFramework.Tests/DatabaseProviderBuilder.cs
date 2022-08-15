﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.EntityFrameworkCore;

namespace Ourstudio.IdentityServer.EntityFramework.IntegrationTests
{
    /// <summary>
    /// Helper methods to initialize DbContextOptions for the specified database provider and context.
    /// </summary>
    public class DatabaseProviderBuilder
    {
        public static DbContextOptions<T> BuildInMemory<T>(string name) where T : DbContext
        {
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseInMemoryDatabase(name);
            return builder.Options;
        }

        public static DbContextOptions<T> BuildSqlite<T>(string name) where T : DbContext
        {
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseSqlite($"Filename=./Test.Ourstudio.IdentityServer.EntityFramework-6.0.0.{name}.db");
            return builder.Options;
        }

        public static DbContextOptions<T> BuildLocalDb<T>(string name) where T : DbContext
        {
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseSqlServer(
                $@"Data Source=(LocalDb)\MSSQLLocalDB;database=Test.Ourstudio.IdentityServer.EntityFramework-6.0.0.{name};trusted_connection=yes;");
            return builder.Options;
        }

        public static DbContextOptions<T> BuildAppVeyorSqlServer2016<T>(string name) where T : DbContext
        {
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseSqlServer($@"Server=(local)\SQL2016;Database=Test.Ourstudio.IdentityServer.EntityFramework-6.0.0.{name};User ID=sa;Password=Password12!");
            return builder.Options;
        }
    }
}