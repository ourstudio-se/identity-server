﻿using Ourstudio.IdentityServer.Models;
using Ourstudio.IdentityServer.Validation;

namespace Ourstudio.IdentityServer.UnitTests.Validation.Setup
{ 
    public static class ValidationExtensions
    {
        public static ClientSecretValidationResult ToValidationResult(this Client client, ParsedSecret secret = null)
        {
            return new ClientSecretValidationResult
            {
                Client = client,
                Secret = secret
            };
        }
    }
}
