﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ourstudio.IdentityServer.Models;
using Ourstudio.IdentityServer.Services;
using Ourstudio.IdentityServer.Validation;

namespace Ourstudio.IdentityServer.UnitTests.Common
{
    public class MockConsentService : IConsentService
    {
        public bool RequiresConsentResult { get; set; }

        public Task<bool> RequiresConsentAsync(ClaimsPrincipal subject, Client client, IEnumerable<ParsedScopeValue> parsedScopes)
        {
            return Task.FromResult(RequiresConsentResult);
        }

        public ClaimsPrincipal ConsentSubject { get; set; }
        public Client ConsentClient { get; set; }
        public IEnumerable<string> ConsentScopes { get; set; }

        public Task UpdateConsentAsync(ClaimsPrincipal subject, Client client, IEnumerable<ParsedScopeValue> parsedScopes)
        {
            ConsentSubject = subject;
            ConsentClient = client;
            ConsentScopes = parsedScopes?.Select(x => x.RawValue);

            return Task.CompletedTask;
        }
    }
}
