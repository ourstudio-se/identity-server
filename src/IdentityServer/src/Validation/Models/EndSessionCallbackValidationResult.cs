﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;

namespace Ourstudio.IdentityServer.Validation
{
    /// <summary>
    /// Validation result for end session callback requests.
    /// </summary>
    /// <seealso cref="Ourstudio.IdentityServer.Validation.ValidationResult" />
    public class EndSessionCallbackValidationResult : ValidationResult
    {
        /// <summary>
        /// Gets the client front-channel logout urls.
        /// </summary>
        public IEnumerable<string> FrontChannelLogoutUrls { get; set; }
    }
}