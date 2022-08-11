﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ourstudio.IdentityServer.Configuration;

#pragma warning disable 1591

namespace Ourstudio.IdentityServer.Hosting
{
    public static class CorsMiddlewareExtensions
    {
        public static void ConfigureCors(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetRequiredService<IdentityServerOptions>();
            app.UseCors(options.Cors.CorsPolicyName);
        }
    }
}
